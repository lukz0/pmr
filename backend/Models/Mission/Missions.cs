using System.Text.Json.Serialization;

namespace backend.Models.Robot
{
    public class Mission
    {
        [JsonPropertyName("guid")]
        public int Id { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}