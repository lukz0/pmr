using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class Status
    {
        public int Id { get; set; }
        
        public int RobotId { get; set; }
        public Robot Robot { get; set; }
        
        public string AllowedMethods { get; set; }
        public float BatteryPercentage { get; set; }
        public float BatteryTimeRemaining { get; set; }
        public float DistanceToNextTarget { get; set; }
        
        [NotMapped]
        public List<string> Error { get; set; }
        [NotMapped]
        public List<string> Footprint { get; set; }
        public string MapId { get; set; }
        public string MissionQueueId { get; set; }
        public string MissionQueueUrl { get; set; }
        public string MissionText { get; set; }
        public int ModeId { get; set; }
        public string ModeText { get; set; }
        public float Moved { get; set; }
        [NotMapped]
        public Position Position { get; set; }
        public string RobotModel { get; set; }
        public string RobotName { get; set; }
        public long SerialNumber { get; set; }
        public string SessionId { get; set; }
        public int StateId { get; set; }
        public string StateText { get; set; }
        public bool UnloadedMapChanges { get; set; }
        public int Uptime { get; set; }
        public string UserPrompt { get; set; }
        [NotMapped]
        public Velocity Velocity { get; set; }
    }

    public abstract class Position
    {
        public float Orientation { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }

    public abstract class Velocity
    {
        public int Angular { get; set; }
        public int Linear { get; set; }
    }
}