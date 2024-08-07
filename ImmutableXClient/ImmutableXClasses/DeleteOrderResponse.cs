using Newtonsoft.Json;

namespace ImmutableXClient.ImmutableXClasses
{
    public class DeleteOrderResponse
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
