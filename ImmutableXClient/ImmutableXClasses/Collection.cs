using Newtonsoft.Json;

namespace ImmutableXClient.ImmutableXClasses;

public class Collection
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("icon_url")]
    public string IconUrl { get; set; }
}