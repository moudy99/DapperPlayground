namespace Dapper_VS_EFcore.Models
{
    public class Developer
    {
        public int DeveloperID { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<GameDeveloper> GameDevelopers { get; set; } = new();
    }
}