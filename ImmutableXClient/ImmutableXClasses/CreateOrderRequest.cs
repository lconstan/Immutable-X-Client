using Newtonsoft.Json;

namespace ImmutableXClient.ImmutableXClasses;

public class CreateOrderRequest
{
    [JsonProperty("amount_buy")]
    public string AmountBuy { get; set; }

    [JsonProperty("amount_sell")]
    public string AmountSell { get; set; }

    [JsonProperty("asset_id_buy")]
    public string AssetIdBuy { get; set; }

    [JsonProperty("asset_id_sell")]
    public string AssetIdSell { get; set; }

    [JsonProperty("expiration_timestamp")]
    public int ExpirationTimestamp { get; set; }

    [JsonProperty("nonce")]
    public int Nonce { get; set; }

    [JsonProperty("stark_key")]
    public string StarkKey { get; set; }

    [JsonProperty("stark_signature")]
    public string StarkSignature { get; set; }

    [JsonProperty("vault_id_buy")]
    public int VaultIdBuy { get; set; }

    [JsonProperty("vault_id_sell")]
    public int VaultIdSell { get; set; }
}