namespace Dapper_VS_EFcore.DTOs
{
    public class GameRawData
    {
        public int GameID { get; set; }
        public string Name { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? RequiredAge { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public int? MetacriticScore { get; set; }
        public string Website { get; set; }
        public int? DeveloperID { get; set; }
        public string DeveloperName { get; set; }
        public string PublisherName { get; set; }
        public string CategoryName { get; set; }
        public string GenreName { get; set; }
        public string TagName { get; set; }
        public string LanguageName { get; set; }
        public string ScreenshotURL { get; set; }
        public int? PackageID { get; set; }
        public string PackageTitle { get; set; }
        public decimal? PackagePrice { get; set; }
    }
}
