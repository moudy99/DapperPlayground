using Dapper_VS_EFcore.Context;
using Dapper_VS_EFcore.DTOs;
using Dapper_VS_EFcore.Models;
using Microsoft.EntityFrameworkCore;

namespace Dapper_VS_EFcore.Repository
{
    public class EfCoreRepository:IBaseRepository
    {
        private readonly EFcoreDbContext dbContext;

        public EfCoreRepository(EFcoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public GameDto GetById(int id)
        {
            var game = dbContext.Games
                .Where(g => g.GameID == id)
                .Include(g => g.GameDevelopers).ThenInclude(gd => gd.Developer)
                .Include(g => g.GamePublishers).ThenInclude(gp => gp.Publisher) // Ensure this is present
                .Include(g => g.GameCategories).ThenInclude(gc => gc.Category)
                .Include(g => g.GameGenres).ThenInclude(gg => gg.Genre)
                .Include(g => g.GameTags).ThenInclude(gt => gt.Tag)
                .Include(g => g.GameLanguages).ThenInclude(gl => gl.Language)
                .Include(g => g.Screenshots)
                .Include(g => g.Packages)
                .FirstOrDefault();

            if (game == null) return null;

            return new GameDto
            {
                GameID = game.GameID,
                Name = game.Name,
                ReleaseDate = game.ReleaseDate,
                RequiredAge = game.RequiredAge,
                Price = game.Price,
                Description = game.Description,
                MetacriticScore = game.MetacriticScore,
                Website = game.Website,
                Developers = game.GameDevelopers.Select(gd => new DeveloperDto
                {
                    DeveloperID = gd.Developer.DeveloperID,
                    Name = gd.Developer.Name
                }).ToList(),
                Publishers = game.GamePublishers.Select(gp => gp.Publisher.Name).ToList(), 
                Categories = game.GameCategories.Select(gc => gc.Category.Name).ToList(),
                Genres = game.GameGenres.Select(gg => gg.Genre.Name).ToList(),
                Tags = game.GameTags.Select(gt => gt.Tag.Name).ToList(),
                Languages = game.GameLanguages.Select(gl => gl.Language.Name).ToList(),
                ScreenshotUrls = game.Screenshots.Select(s => s.URL).ToList(),
                Packages = game.Packages.Select(p => new PackageDto
                {
                    PackageID = p.PackageID,
                    Title = p.Title,
                    Price = p.Price
                }).ToList()
            };
        }
    }
}
