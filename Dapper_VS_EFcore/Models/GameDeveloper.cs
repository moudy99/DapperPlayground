namespace Dapper_VS_EFcore.Models
{
    public class GameDeveloper
    {
        public int GameID { get; set; }
        public Game Game { get; set; }

        public int DeveloperID { get; set; }
        public Developer Developer { get; set; }
    }
}