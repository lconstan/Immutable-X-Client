
using ImmutableXClient.ImmutableXClasses;

namespace ImmutableXClient.HttpCommunication
{
    internal interface ICommunicationClient
    {
        Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest request, Dictionary<string, string> headers = null);
        Task<DeleteOrderResponse> SendDeleteAsync(string uri, CancelOrderRequest cancelOrderRequest, Dictionary<string, string> headers);
    }
}
