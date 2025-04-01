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

        public List<GameDeveloper> GameDevelopers { get; set; } = new();
        public List<GamePublisher> GamePublishers { get; set; } = new();
        public List<GameCategory> GameCategories { get; set; } = new();
        public List<GameGenre> GameGenres { get; set; } = new();
        public List<GameTag> GameTags { get; set; } = new();
        public List<GameLanguage> GameLanguages { get; set; } = new();

        public List<Screenshot> Screenshots { get; set; } = new();
        public List<Package> Packages { get; set; } = new();
    }
}