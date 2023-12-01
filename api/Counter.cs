using System.Text.Json.Serialization;

namespace Api.Function
{
    public class Counter(string id, int count)
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = id ?? throw new ArgumentNullException(nameof(id));

        [JsonPropertyName("count")]
        public int Count { get; set; } = count;
    }
}