using Newtonsoft.Json;

namespace ImmutableXClient.ImmutableXClasses
{
    public class SignableCancelOrderRequest
    {
        [JsonProperty("order_id")]
        public int OrderId { get; set; }
    }
}
