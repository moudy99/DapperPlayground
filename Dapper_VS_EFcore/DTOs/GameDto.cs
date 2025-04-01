namespace Dapper_VS_EFcore.DTOs
{
    public class GameDto
    {
        public int GameID { get; set; }
        public string Name { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? RequiredAge { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public int? MetacriticScore { get; set; }
        public string? Website { get; set; }

        public List<DeveloperDto> Developers { get; set; } = new();
        public List<string> Publishers { get; set; } = new();
        public List<string> Categories { get; set; } = new();
        public List<string> Genres { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public List<string> Languages { get; set; } = new();
        public List<string> ScreenshotUrls { get; set; } = new();
        public List<PackageDto> Packages { get; set; } = new();
    }

    public class DeveloperDto
    {
        public int DeveloperID { get; set; }
        public string Name { get; set; }
    }

    public class PackageDto
    {
        public int PackageID { get; set; }
        public string? Title { get; set; }
        public decimal? Price { get; set; }
    }
}
