namespace Dapper_VS_EFcore.Models
{
    public class Language
    {
        public int LanguageID { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<GameLanguage> GameLanguages { get; set; } = new();
    }
}