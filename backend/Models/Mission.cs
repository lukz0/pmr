using System;
using System.Security.Policy;
using System.Text.Json.Serialization;

namespace backend.Models
{
    public class Mission
    {
        [JsonIgnore]
        public int Id { get; set; }
        
        public string Guid { get; set; }
        
        public string Name { get; set; }
        
        public string Url { get; set; }
        
        [JsonIgnore]
        public int RobotId { get; set; }
        [JsonIgnore]
        public Robot Robot { get; set; }
    }
}