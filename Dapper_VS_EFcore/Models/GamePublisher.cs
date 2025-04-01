namespace Dapper_VS_EFcore.Models
{

public class GamePublisher
{
    public int GameID { get; set; }
    public Game Game { get; set; }

    public int PublisherID { get; set; }
    public Publisher Publisher { get; set; }
}
}