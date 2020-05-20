using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;


namespace backend.Models
{
    public class Status
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public int Id { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        public int RobotId { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        public Robot Robot { get; set; }
        
   
        [JsonPropertyName("battery_percentage")]
        public float BatteryPercentage { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("battery_time_remaining")]
        public float BatteryTimeRemaining { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("distance_to_next_target")]
        public float DistanceToNextTarget { get; set; }
        
        [NotMapped, System.Text.Json.Serialization.JsonIgnore]
        public Dictionary<string, Error> Error { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        public string Footprint { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("map_id")]
        public string MapId { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("mission_queue_id")]
        public string MissionQueueId { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("mission_queue_url")]
        public string MissionQueueUrl { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("mission_text")]
        public string MissionText { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("mode_id")]
        public int ModeId { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("mode_text")]
        public string ModeText { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        public float Moved { get; set; }
        
        [NotMapped]
        [System.Text.Json.Serialization.JsonIgnore]
        public Dictionary<string, Position> Position { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("robot_model")]
        public string RobotModel { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("robot_name")]
        public string RobotName { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("serial_number")]
        public long SerialNumber { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("session_id")]
        public string SessionId { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("state_id")]
        public int StateId { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("state_text")]
        public string StateText { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("unloaded_map_changes")]
        public bool UnloadedMapChanges { get; set; }
        
        [System.Text.Json.Serialization.JsonIgnore]
        public int Uptime { get; set; }
        
        [JsonIgnore]
        [NotMapped, JsonPropertyName("user_prompt")]
        public Dictionary<string, UserPrompt> UserPrompt { get; set; }
        
        [NotMapped]
        public Velocity Velocity { get; set; }
        
    }

    public abstract class Error
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Module { get; set; }
    }

    public abstract class Position
    {
        public float Orientation { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }

    public class Velocity
    {
        [JsonPropertyName("angular")]
        public string Angular { get; set; }
        [JsonPropertyName("linear")]
        public string Linear { get; set; }
    }

    public abstract class UserPrompt
    {
        [JsonIgnore]
        public string Guid { get; set; }
        public string Options { get; set; }
        public string Question { get; set; }
        public float Timeout { get; set; }
        
        [JsonPropertyName("user_group")]
        public string UserGroup { get; set; }
    }
}