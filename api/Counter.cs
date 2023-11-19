using System.Text.Json.Serialization;

namespace Api.Function
{
    public class Counter
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
        public Counter(string id)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }
    }
}