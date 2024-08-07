using Newtonsoft.Json;

namespace ImmutableXClient.ImmutableXClasses;

public class CreateOrderResponse
{
    [JsonProperty("order_id")]
    public int OrderId { get; set; }

    [JsonProperty("request_id")]
    public string RequestId { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("time")]
    public int Time { get; set; }
}