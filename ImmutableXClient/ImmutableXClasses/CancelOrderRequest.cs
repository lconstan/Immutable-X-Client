using Newtonsoft.Json;

namespace ImmutableXClient.ImmutableXClasses
{
    public class CancelOrderRequest
    {
        [JsonProperty("order_id")]
        public int OrderId { get; set; }

        [JsonProperty("stark_signature")]
        public string StartkSignature { get; set; }
    }
}
