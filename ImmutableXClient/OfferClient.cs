using ImmutableXClient.Constants;
using ImmutableXClient.Dependencies;
using ImmutableXClient.HttpCommunication;
using ImmutableXClient.ImmutableXClasses;
using System.Globalization;

namespace ImmutableXClient;

internal class OfferClient : IOfferClient
{
    // See https://www.immutabletools.online/
    private readonly ISigner _signer;

    private const string _ethSecret = "Fill with your eth private key";
    private const string _starkSecret = " Fill with your stark private key";

    private const string _ethPublicAddress = "[fill_with_eth_public_address]";

    // To be filled with your ownn client if needed, through Dependency Injection for instance
    private readonly ICommunicationClient _communicationClient = new CommunicationClient(new Uri("https://api.x.immutable.com/"));

    private static readonly Dictionary<string, string> AddressesByTokenName = new()
    {
        [TokenConstants.Eth] = "",
        [TokenConstants.Gods] = TokenConstants.GodsTokenAddress,
        [TokenConstants.Imx] = TokenConstants.ImxTokenAddress,
        [TokenConstants.Usdc] = TokenConstants.UsdcTokenAddress,
        [TokenConstants.Gog] = TokenConstants.GogTokenAddress,
        [TokenConstants.Cta] = TokenConstants.CtaTokenAddress,
    };

    public async Task<ImmutableOrderResponse<CreateOrderResponse>> MakeOfferAsync(string collectionId,
                                                                              string tokenId,
                                                                              decimal price,
                                                                              string currency,
                                                                              bool isBuyOrder,
                                                                              int? orderId = null)
    {

        SignableOrderRequest signableOrderRequest = GetSignableTradeRequest(collectionId, tokenId, price, currency, isBuyOrder);

        SignableOrderResponse response =
            await _communicationClient.PostAsync<SignableOrderRequest, SignableOrderResponse>("v3/signable-order-details", signableOrderRequest);

        if (response == null)
        {
            return new ImmutableOrderResponse<CreateOrderResponse>(OrderErrorCode.NullResponse, "Null response while trying to create order");
        }

        string ethSignature = _signer.EthSign(_ethSecret, response.SignableMessage);
        string starkSignature = _signer.StartkSign(_starkSecret, response.PayloadHash);

        var createOrderRequest = new CreateOrderRequest()
        {
            AmountBuy = response.AmountBuy,
            AmountSell = response.AmountSell,
            AssetIdBuy = response.AssetIdBuy,
            AssetIdSell = response.AssetIdSell,
            ExpirationTimestamp = response.ExpirationTimestamp,
            Nonce = response.Nonce,
            StarkKey = response.StarkKey,
            StarkSignature = starkSignature,
            VaultIdBuy = response.VaultIdBuy,
            VaultIdSell = response.VaultIdSell
        };
        var headers = new Dictionary<string, string>
        {
            ["x-imx-eth-address"] = _ethPublicAddress,
            ["x-imx-eth-signature"] = ethSignature
        };

        CreateOrderResponse orderResponse =
            await _communicationClient.PostAsync<CreateOrderRequest, CreateOrderResponse>("v3/orders", createOrderRequest, headers);

        return new(orderResponse);
    }

    public async Task<ImmutableOrderResponse<DeleteOrderResponse>> CancelOrdersAsync(int orderId)
    {
        SignableCancelOrderRequest signableOrderRequest = GetSignableCancelRequest(orderId);

        SignableOrderResponse response =
            await _communicationClient.PostAsync<SignableCancelOrderRequest, SignableOrderResponse>("v3/signable-cancel-order-details", signableOrderRequest);

        if (response == null)
        {
            return new ImmutableOrderResponse<DeleteOrderResponse>(OrderErrorCode.NullResponse, "Null response while trying to cancel"); ;
        }

        string ethSignature = _signer.EthSign(_ethSecret, response.SignableMessage);
        string starkSignature = _signer.StartkSign(_starkSecret, response.PayloadHash);

        var cancelOrderRequest = new CancelOrderRequest()
        {
            StartkSignature = starkSignature,
            OrderId = orderId
        };
        var headers = new Dictionary<string, string>
        {
            ["x-imx-eth-address"] = _ethPublicAddress,
            ["x-imx-eth-signature"] = ethSignature
        };

        DeleteOrderResponse orderResponse =
            await _communicationClient.SendDeleteAsync($"v3/orders/{orderId}", cancelOrderRequest, headers);

        return new(orderResponse);
    }

    private static SignableCancelOrderRequest GetSignableCancelRequest(int orderId)
    {
        return new SignableCancelOrderRequest
        {
            OrderId = orderId
        };
    }

    private static SignableOrderRequest GetSignableTradeRequest(string collectionId, string tokenId, decimal price, string currency, bool isBuyOrder)
    {
        string ethPriceString = (price * (decimal)Math.Pow(10, 18)).ToString(CultureInfo.InvariantCulture);
        ethPriceString = ethPriceString.Substring(0, ethPriceString.IndexOf('.'));

        var erc721data = new BuySell()
        {
            Type = "ERC721",
            Data = new Data()
            {
                TokenAddress= collectionId,
                TokenId = tokenId
            }
        };
        var data = new BuySell()
        {
            Type = currency == TokenConstants.Eth ? TokenConstants.Eth : "ERC20",
            Data = new Data()
            {
                Decimals = 18,
                TokenAddress = currency == TokenConstants.Eth ? null : AddressesByTokenName[currency]
            }
        };

        return new SignableOrderRequest()
        {
            AmountBuy = isBuyOrder ? "1" : ethPriceString,
            AmountSell = isBuyOrder ? ethPriceString : "1",
            EthAddress = _ethPublicAddress,
            BuyInformation = isBuyOrder ? erc721data : data,
            SellInformation = isBuyOrder ? data : erc721data
        };
    }
}