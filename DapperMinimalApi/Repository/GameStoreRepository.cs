using Dapper;
using DapperMinimalApi.Repository.Models;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Runtime.CompilerServices;
using System.Transactions;

namespace DapperMinimalApi.Repository
{
    public class GameStoreRepository : IGameStoreRepository
    {
        private readonly DapperContext _dapperContext;

        public GameStoreRepository(DapperContext dapperContext)
        {
            this._dapperContext = dapperContext;
        }

        public int GetGamesCount()
        {
            using (var connection = _dapperContext.GetConnection())
            {
                string sql = "SELECT COUNT(*) FROM GAMES";
                var count = connection.Query<int>(sql).FirstOrDefault();
                return count;
            }
        }

        public dynamic GetAllGames()
        {
            using (var connection = _dapperContext.GetConnection())
            {
                string sql = "SELECT * FROM GAMES where GameID in @ids";
                int[] Ids = [1, 2, 3, 4, 5, 6, 7];
                var result = connection.Query<Game>(sql, new { ids = Ids });

                return result;
            }

        }

        public dynamic getById(int GameId)
        {
            using (var connection = _dapperContext.GetConnection())
            {
                // here i will run store procedual called  sp_show_full_table that take @id
                var result = connection.ExecuteScalar("sp_show_full_table", new { id = GameId }, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public dynamic GetSummary()
        {
            using (var connection = _dapperContext.GetConnection())
            {
                string sql = @"
SELECT TOP 5
    g.GameID,
    g.Name AS GameName,
    g.ReleaseDate,
    g.Price AS BasePrice,
    g.MetacriticScore,

    -- Developer(s)
    (SELECT STRING_AGG(d.Name, ', ') 
     FROM GameDevelopers gd 
     JOIN Developers d ON gd.DeveloperID = d.DeveloperID
     WHERE gd.GameID = g.GameID) AS Developers,

    -- Publisher(s)
    (SELECT STRING_AGG(p.Name, ', ') 
     FROM GamePublishers gp 
     JOIN Publishers p ON gp.PublisherID = p.PublisherID
     WHERE gp.GameID = g.GameID) AS Publishers,

    -- Number of Available Packages
    (SELECT COUNT(*) FROM Packages pk WHERE pk.GameID = g.GameID) AS PackageCount,

    -- Minimum Package Price
    (SELECT MIN(pk.Price) FROM Packages pk WHERE pk.GameID = g.GameID) AS MinPackagePrice

FROM Games g

ORDER BY g.ReleaseDate DESC;";

                var result = connection.Query(sql);
                return result;
            }
        }


        // query multiple sql statements
        public dynamic testMultiple(int ID)
        {
            string sql = $"select * from Games where GameID = @id; select * from Screenshots where GameID = @id;select * from GameDevelopers where GameID =@id ";

            using (var connection = _dapperContext.GetConnection())
            {
                using (var multi = connection.QueryMultiple(sql, new { id = ID }))
                {
                    var games = multi.Read<Game>().ToList();
                    var screenshots = multi.Read<Screenshot>().ToList();
                    var gameDeveloper = multi.Read<Developer>().ToList();
                    foreach (var game in games)
                    {

                        game.Developers = gameDeveloper;
                        game.Screenshots = screenshots;
                    }
                    return games;
                }
            }
        }


        // Test dapper Transactions ->

        public dynamic testTransaction()
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    using (var connection = _dapperContext.GetConnection())
                    {
                        connection.Open();

                        string sql = "INSERT INTO Games (Name, ReleaseDate, Price, MetacriticScore) VALUES (@Name, @ReleaseDate, @Price, @MetacriticScore);";
                        var rowsAffected1 = connection.Execute(sql, new { Name = "Test Game", ReleaseDate = "2021-01-01", Price = 50.00, MetacriticScore = 80 });

                        sql = "INSERT INTO Publishers(Name) VALUES (@Name);";
                        connection.Execute(sql, new { Name = "Moudy" });

                        sql = "SELECT PublisherID, Name FROM Publishers WHERE Name = @Name;";
                        var insertedPublisher = connection.QuerySingleOrDefault(sql, new { Name = "Moudy" });

                        sql = "INSERT INTO GamePublishers (GameID, PublisherID) VALUES (@GameID, @PublisherID);";
                        var rowsAffected4 = connection.Execute(sql, new { GameID = 1, PublisherID = insertedPublisher?.PublisherID });

                        if (rowsAffected1 > 0 && rowsAffected4 > 0 && insertedPublisher != null)
                        {
                            transaction.Complete();
                            return new { Success = true, Message = "Transaction completed successfully." };
                        }
                        else
                        {
                            return new { Success = false, Message = "Transaction failed: Some inserts were not successful." };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new { Success = false, Message = "Transaction failed", Error = ex.Message };
            }
        }

    }
}
