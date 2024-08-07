using Newtonsoft.Json;

namespace ImmutableXClient.ImmutableXClasses;

public class FeeInfo
{
    [JsonProperty("asset_id")]
    public string AssetId { get; set; }

    [JsonProperty("fee_limit")]
    public string FeeLimit { get; set; }

    [JsonProperty("source_vault_id")]
    public int SourceVaultId { get; set; }
}