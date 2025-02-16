
namespace Dapper_VS_EFcore.Models
{
    public class Game
    {
        public int GameID { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime? ReleaseDate { get; set; }
        public int? RequiredAge { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public int? MetacriticScore { get; set; }
        public string? Website { get; set; }

        public List<Developer> Developers { get; set; } = new();
        public List<Publisher> Publishers { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<Genre> Genres { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
        public List<Language> Languages { get; set; } = new();
        public List<Screenshot> Screenshots { get; set; } = new();
        public List<Package> Packages { get; set; } = new();
    }
}
