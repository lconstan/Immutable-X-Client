using Newtonsoft.Json;

namespace ImmutableXClient.ImmutableXClasses;

public class Data
{
    [JsonProperty("token_id")]
    public string TokenId { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("token_address")]
    public string TokenAddress { get; set; }

    [JsonProperty("quantity")]
    public string Quantity { get; set; }

    [JsonProperty("quantity_with_fees")]
    public string QuantityWithFees { get; set; }

    [JsonProperty("properties")]
    public Properties Properties { get; set; }

    [JsonProperty("decimals")]
    public int? Decimals { get; set; }

    [JsonProperty("contract_address")]
    public string ContractAddress { get; set; }
}