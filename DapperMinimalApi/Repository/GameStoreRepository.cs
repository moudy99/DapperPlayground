using Dapper;
using DapperMinimalApi.Repository.Models;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace DapperMinimalApi.Repository
{
    public class GameStoreRepository : IGameStoreRepository
    {
        private readonly DapperContext _dapperContext;

        public GameStoreRepository(DapperContext dapperContext)
        {
            this._dapperContext = dapperContext;
        }

        public  int GetGamesCount()
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
                string sql = "SELECT * FROM GAMES";
                var result = connection.Query(sql);
                return result.Take(50) ;
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
            using ( var connection = _dapperContext.GetConnection())
            {
                string sql = @"
SELECT 
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
    }
}
