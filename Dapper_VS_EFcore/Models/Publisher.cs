namespace Dapper_VS_EFcore.Models
{
    public class Publisher
    {
        public int PublisherID { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<GamePublisher> GamePublishers { get; set; } = new();
    }
}