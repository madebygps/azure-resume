using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Api.Function
{
    public class Counter
    {
        // Use both System.Text.Json and Newtonsoft.Json attributes to ensure proper serialization
        [JsonPropertyName("id")]
        [Newtonsoft.Json.JsonProperty("id")]
        public string Id { get; set; }

        [JsonPropertyName("count")]
        [Newtonsoft.Json.JsonProperty("count")]
        public int Count { get; set; }

        public Counter(string id, int count)
        {
            Id = id ?? throw new System.ArgumentNullException(nameof(id));
            Count = count;
        }
        
        // Empty constructor for serialization
        public Counter() 
        {
            Id = "index";  // Match your actual document ID
            Count = 0;
        }
    }
}