namespace Dapper_VS_EFcore.Models
{
    public class Package
    {
        public int PackageID { get; set; }
        public int GameID { get; set; }
        public string? Title { get; set; }
        public decimal? Price { get; set; }

        public Game Game { get; set; }
    }
}