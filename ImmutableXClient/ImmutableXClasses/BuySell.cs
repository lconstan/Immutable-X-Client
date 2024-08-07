using Newtonsoft.Json;

namespace ImmutableXClient.ImmutableXClasses;

public class BuySell
{
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("data")]
    public Data Data { get; set; }
}