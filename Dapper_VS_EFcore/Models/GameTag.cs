namespace Dapper_VS_EFcore.Models
{

public class GameTag
{
    public int GameID { get; set; }
    public Game Game { get; set; }

    public int TagID { get; set; }
    public Tag Tag { get; set; }
}
}