using System.Text.Json.Serialization;

namespace backend.Models
{
    public class PutStatus
    {
        [JsonPropertyName("status_id")]
        public int StateId { get; set; }
    }
}