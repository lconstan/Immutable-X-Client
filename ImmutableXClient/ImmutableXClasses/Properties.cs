using Newtonsoft.Json;

namespace ImmutableXClient.ImmutableXClasses;

public class Properties
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("image_url")]
    public string ImageUrl { get; set; }

    [JsonProperty("collection")]
    public Collection Collection { get; set; }
}