namespace Dapper_VS_EFcore.Models
{
    public class Genre
    {
        public int GenreID { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<GameGenre> GameGenres { get; set; } = new();
    }
}