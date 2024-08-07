using ImmutableXClient;
using ImmutableXClient.ImmutableXClasses;

public interface IOfferClient
{
    Task<ImmutableOrderResponse<CreateOrderResponse>> MakeOfferAsync(string collectionId, string tokenId, decimal price, string currency, bool isBuyOrder, int? orderId = null);

    Task<ImmutableOrderResponse<DeleteOrderResponse>> CancelOrdersAsync(int orderId);
}
