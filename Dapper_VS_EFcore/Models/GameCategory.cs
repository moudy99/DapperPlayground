namespace Dapper_VS_EFcore.Models
{ 
public class GameCategory
{
    public int GameID { get; set; }
    public Game Game { get; set; }

    public int CategoryID { get; set; }
    public Category Category { get; set; }
}
}