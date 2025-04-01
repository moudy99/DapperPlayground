using Dapper;
using Dapper_VS_EFcore.Context;
using Dapper_VS_EFcore.DTOs;
using System.Linq;

namespace Dapper_VS_EFcore.Repository
{
    public class DapperRepository : IBaseRepository
    {
        private readonly DapperDBContext dapperDBContext;

        public DapperRepository(DapperDBContext dapperDBContext)
        {
            this.dapperDBContext = dapperDBContext;
        }

        public GlobalResponse<GameDto> GetById(int id)
        {
            string sql = @"
                CREATE TABLE #GameData (
                    GameID INT,
                    Name NVARCHAR(255),
                    ReleaseDate DATE,
                    RequiredAge INT,
                    Price DECIMAL(10, 2),
                    Description NVARCHAR(MAX),
                    MetacriticScore INT,
                    Website NVARCHAR(255),
                    DeveloperID INT,
                    DeveloperName NVARCHAR(255),
                    PublisherName NVARCHAR(255),
                    CategoryName NVARCHAR(255),
                    GenreName NVARCHAR(255),
                    TagName NVARCHAR(255),
                    LanguageName NVARCHAR(255),
                    ScreenshotURL NVARCHAR(500),
                    PackageID INT,
                    PackageTitle NVARCHAR(255),
                    PackagePrice DECIMAL(10, 2)
                );

                INSERT INTO #GameData
                -- Base game data
                SELECT 
                    g.GameID,
                    g.Name,
                    g.ReleaseDate,
                    g.RequiredAge,
                    g.Price,
                    g.Description,
                    g.MetacriticScore,
                    g.Website,
                    NULL AS DeveloperID,
                    NULL AS DeveloperName,
                    NULL AS PublisherName,
                    NULL AS CategoryName,
                    NULL AS GenreName,
                    NULL AS TagName,
                    NULL AS LanguageName,
                    NULL AS ScreenshotURL,
                    NULL AS PackageID,
                    NULL AS PackageTitle,
                    NULL AS PackagePrice
                FROM Games g
                WHERE g.GameID = @gameID

                UNION ALL

                -- Developers
                SELECT 
                    g.GameID,
                    NULL AS Name,
                    NULL AS ReleaseDate,
                    NULL AS RequiredAge,
                    NULL AS Price,
                    NULL AS Description,
                    NULL AS MetacriticScore,
                    NULL AS Website,
                    d.DeveloperID,
                    d.Name AS DeveloperName,
                    NULL AS PublisherName,
                    NULL AS CategoryName,
                    NULL AS GenreName,
                    NULL AS TagName,
                    NULL AS LanguageName,
                    NULL AS ScreenshotURL,
                    NULL AS PackageID,
                    NULL AS PackageTitle,
                    NULL AS PackagePrice
                FROM Games g
                INNER JOIN GameDevelopers gd ON g.GameID = gd.GameID
                INNER JOIN Developers d ON gd.DeveloperID = d.DeveloperID
                WHERE g.GameID = @gameID

                UNION ALL

                -- Publishers
                SELECT 
                    g.GameID,
                    NULL AS Name,
                    NULL AS ReleaseDate,
                    NULL AS RequiredAge,
                    NULL AS Price,
                    NULL AS Description,
                    NULL AS MetacriticScore,
                    NULL AS Website,
                    NULL AS DeveloperID,
                    NULL AS DeveloperName,
                    p.Name AS PublisherName,
                    NULL AS CategoryName,
                    NULL AS GenreName,
                    NULL AS TagName,
                    NULL AS LanguageName,
                    NULL AS ScreenshotURL,
                    NULL AS PackageID,
                    NULL AS PackageTitle,
                    NULL AS PackagePrice
                FROM Games g
                INNER JOIN GamePublishers gp ON g.GameID = gp.GameID
                INNER JOIN Publishers p ON gp.PublisherID = p.PublisherID
                WHERE g.GameID = @gameID

                UNION ALL

                -- Categories
                SELECT 
                    g.GameID,
                    NULL AS Name,
                    NULL AS ReleaseDate,
                    NULL AS RequiredAge,
                    NULL AS Price,
                    NULL AS Description,
                    NULL AS MetacriticScore,
                    NULL AS Website,
                    NULL AS DeveloperID,
                    NULL AS DeveloperName,
                    NULL AS PublisherName,
                    c.Name AS CategoryName,
                    NULL AS GenreName,
                    NULL AS TagName,
                    NULL AS LanguageName,
                    NULL AS ScreenshotURL,
                    NULL AS PackageID,
                    NULL AS PackageTitle,
                    NULL AS PackagePrice
                FROM Games g
                INNER JOIN GameCategories gc ON g.GameID = gc.GameID
                INNER JOIN Categories c ON gc.CategoryID = c.CategoryID
                WHERE g.GameID = @gameID

                UNION ALL

                -- Genres
                SELECT 
                    g.GameID,
                    NULL AS Name,
                    NULL AS ReleaseDate,
                    NULL AS RequiredAge,
                    NULL AS Price,
                    NULL AS Description,
                    NULL AS MetacriticScore,
                    NULL AS Website,
                    NULL AS DeveloperID,
                    NULL AS DeveloperName,
                    NULL AS PublisherName,
                    NULL AS CategoryName,
                    gen.Name AS GenreName,
                    NULL AS TagName,
                    NULL AS LanguageName,
                    NULL AS ScreenshotURL,
                    NULL AS PackageID,
                    NULL AS PackageTitle,
                    NULL AS PackagePrice
                FROM Games g
                INNER JOIN GameGenres ggen ON g.GameID = ggen.GameID
                INNER JOIN Genres gen ON ggen.GenreID = gen.GenreID
                WHERE g.GameID = @gameID

                UNION ALL

                -- Tags
                SELECT 
                    g.GameID,
                    NULL AS Name,
                    NULL AS ReleaseDate,
                    NULL AS RequiredAge,
                    NULL AS Price,
                    NULL AS Description,
                    NULL AS MetacriticScore,
                    NULL AS Website,
                    NULL AS DeveloperID,
                    NULL AS DeveloperName,
                    NULL AS PublisherName,
                    NULL AS CategoryName,
                    NULL AS GenreName,
                    t.Name AS TagName,
                    NULL AS LanguageName,
                    NULL AS ScreenshotURL,
                    NULL AS PackageID,
                    NULL AS PackageTitle,
                    NULL AS PackagePrice
                FROM Games g
                INNER JOIN GameTags gt ON g.GameID = gt.GameID
                INNER JOIN Tags t ON gt.TagID = t.TagID
                WHERE g.GameID = @gameID

                UNION ALL

                -- Languages
                SELECT 
                    g.GameID,
                    NULL AS Name,
                    NULL AS ReleaseDate,
                    NULL AS RequiredAge,
                    NULL AS Price,
                    NULL AS Description,
                    NULL AS MetacriticScore,
                    NULL AS Website,
                    NULL AS DeveloperID,
                    NULL AS DeveloperName,
                    NULL AS PublisherName,
                    NULL AS CategoryName,
                    NULL AS GenreName,
                    NULL AS TagName,
                    l.Name AS LanguageName,
                    NULL AS ScreenshotURL,
                    NULL AS PackageID,
                    NULL AS PackageTitle,
                    NULL AS PackagePrice
                FROM Games g
                INNER JOIN GameLanguages gl ON g.GameID = gl.GameID
                INNER JOIN Languages l ON gl.LanguageID = l.LanguageID
                WHERE g.GameID = @gameID

                UNION ALL

                -- Screenshots
                SELECT 
                    g.GameID,
                    NULL AS Name,
                    NULL AS ReleaseDate,
                    NULL AS RequiredAge,
                    NULL AS Price,
                    NULL AS Description,
                    NULL AS MetacriticScore,
                    NULL AS Website,
                    NULL AS DeveloperID,
                    NULL AS DeveloperName,
                    NULL AS PublisherName,
                    NULL AS CategoryName,
                    NULL AS GenreName,
                    NULL AS TagName,
                    NULL AS LanguageName,
                    s.URL AS ScreenshotURL,
                    NULL AS PackageID,
                    NULL AS PackageTitle,
                    NULL AS PackagePrice
                FROM Games g
                INNER JOIN Screenshots s ON g.GameID = s.GameID
                WHERE g.GameID = @gameID

                UNION ALL

                -- Packages
                SELECT 
                    g.GameID,
                    NULL AS Name,
                    NULL AS ReleaseDate,
                    NULL AS RequiredAge,
                    NULL AS Price,
                    NULL AS Description,
                    NULL AS MetacriticScore,
                    NULL AS Website,
                    NULL AS DeveloperID,
                    NULL AS DeveloperName,
                    NULL AS PublisherName,
                    NULL AS CategoryName,
                    NULL AS GenreName,
                    NULL AS TagName,
                    NULL AS LanguageName,
                    NULL AS ScreenshotURL,
                    pkg.PackageID,
                    pkg.Title AS PackageTitle,
                    pkg.Price AS PackagePrice
                FROM Games g
                INNER JOIN Packages pkg ON g.GameID = pkg.GameID
                WHERE g.GameID = @gameID;

                SELECT * FROM #GameData;

                DROP TABLE #GameData;
            ";

            using (var context = dapperDBContext.GetConnection())
            {
                context.Open();
                /*
                 usign the sp instead of wirte the sql query here 
                  var sp = "get_game_by_id";
                 DynamicParameters parameters = new DynamicParameters();
                 parameters.Add("gameId", id);
                 var results = context.Query<GameRawData>(sp, parameters).ToList();
                 */
                var results = context.Query<GameRawData>(sql, new { gameID = id }).ToList();

                // Aggregate into GameDto
                var baseGame = results.FirstOrDefault(r => r.Name != null);
                if (baseGame == null) return null;

                GameDto returnData= new GameDto
                {
                    GameID = baseGame.GameID,
                    Name = baseGame.Name,
                    ReleaseDate = baseGame.ReleaseDate,
                    RequiredAge = baseGame.RequiredAge,
                    Price = baseGame.Price,
                    Description = baseGame.Description,
                    MetacriticScore = baseGame.MetacriticScore,
                    Website = baseGame.Website,
                    Developers = results.Where(r => r.DeveloperID.HasValue)
                        .Select(r => new DeveloperDto { DeveloperID = r.DeveloperID.Value, Name = r.DeveloperName })
                        .DistinctBy(d => d.DeveloperID).ToList(),
                    Publishers = results.Where(r => r.PublisherName != null)
                        .Select(r => r.PublisherName).Distinct().ToList(),
                    Categories = results.Where(r => r.CategoryName != null)
                        .Select(r => r.CategoryName).Distinct().ToList(),
                    Genres = results.Where(r => r.GenreName != null)
                        .Select(r => r.GenreName).Distinct().ToList(),
                    Tags = results.Where(r => r.TagName != null)
                        .Select(r => r.TagName).Distinct().ToList(),
                    Languages = results.Where(r => r.LanguageName != null)
                        .Select(r => r.LanguageName).Distinct().ToList(),
                    ScreenshotUrls = results.Where(r => r.ScreenshotURL != null)
                        .Select(r => r.ScreenshotURL).Distinct().ToList(),
                    Packages = results.Where(r => r.PackageID.HasValue)
                        .Select(r => new PackageDto { PackageID = r.PackageID.Value, Title = r.PackageTitle, Price = r.PackagePrice })
                        .DistinctBy(p => p.PackageID).ToList()
                };

                return new GlobalResponse<GameDto> { Data = returnData ,Message="Return from Dapper repo",IsSuccess=true};
            }
        }
    }

   
}