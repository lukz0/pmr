using System.Text.Json.Serialization;

namespace backend.Models.Robot
{
    public class RobotModel
    {
        [JsonPropertyName("guid")]
        public string Guid { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }
    }
}