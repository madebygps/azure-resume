using System.Text.Json.Serialization;

namespace Company.Function;

public class Counter 
{
    
    [JsonPropertyName("id")]
    public string? Id {get; set;}
    
    [JsonPropertyName("count")]
    public int Count {get;set;}
}