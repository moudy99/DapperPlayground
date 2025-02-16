namespace DapperMinimalApi.Models
{
    public class Screenshot
    {
        public int ScreenshotID { get; set; }
        public int GameID { get; set; }
        public string URL { get; set; } = string.Empty;
    }

}