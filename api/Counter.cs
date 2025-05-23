using System.Text.Json.Serialization;

namespace Api.Function
{
    public class Counter
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("pk")]
        public string PartitionKey { get; set; } = string.Empty;

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = "counter";

        [JsonPropertyName("lastUpdated")]
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("_ts")]
        public long Timestamp { get; set; }

        [JsonPropertyName("_etag")]
        public string? ETag { get; set; }

        public Counter(string id, int count)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            PartitionKey = id; 
            Count = count;
            LastUpdated = DateTime.UtcNow;
        }

        public Counter() 
        {
            Id = "visitor-count";
            PartitionKey = "visitor-count";
            Count = 0;
            LastUpdated = DateTime.UtcNow;
        }
    }
}