namespace Dapper_VS_EFcore.Models
{
    public class Screenshot
    {
        public int ScreenshotID { get; set; }
        public int GameID { get; set; }
        public string URL { get; set; } = string.Empty;
    }

}