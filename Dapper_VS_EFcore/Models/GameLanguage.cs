namespace Dapper_VS_EFcore.Models
{

public class GameLanguage
{
    public int GameID { get; set; }
    public Game Game { get; set; }

    public int LanguageID { get; set; }
    public Language Language { get; set; }
}
}