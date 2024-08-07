using Newtonsoft.Json;

namespace ImmutableXClient.ImmutableXClasses;

public class SignableOrderRequest
{
    [JsonProperty("user")]
    public string EthAddress { get; set; }

    [JsonProperty("amount_buy")]
    public string AmountBuy { get; set; }

    [JsonProperty("amount_sell")]
    public string AmountSell { get; set; }

    [JsonProperty("token_buy")]
    public BuySell BuyInformation { get; set; }

    [JsonProperty("token_sell")]
    public BuySell SellInformation { get; set; }
}