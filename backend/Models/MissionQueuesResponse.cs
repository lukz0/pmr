using System.Text.Json.Serialization;

namespace backend.Models
{
    public class MissionQueuesResponse
    {
        public int Id { get; set; }
        
        public string State { get; set; }
        
        public string Url { get; set; }
        
        [JsonIgnore]
        public int RobotId { get; set; }
        [JsonIgnore]
        public Robot Robot { get; set; }
    }
}