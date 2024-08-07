using ImmutableXClient.ImmutableXClasses;
using Newtonsoft.Json;

namespace ImmutableXClient.HttpCommunication
{
    /// <summary>
    /// Quick & simple client to communicate with immutable X API, for demo purposes
    /// </summary>
    internal class CommunicationClient : ICommunicationClient
    {
        private readonly HttpClient _client;

        public CommunicationClient(Uri baseUri)
        {
            _client = new HttpClient()
            {
                BaseAddress = baseUri
            };
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string uri, TRequest request, Dictionary<string, string> headers = null)
        {
            return await ExecuteAsync<TResponse>(async () =>
            {
                var stringContent = JsonConvert.SerializeObject(request);
                var httpContent = new StringContent(stringContent);

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        httpContent.Headers.Add(header.Key, header.Value);
                    }
                }

                return await _client.PostAsync(uri, httpContent).ConfigureAwait(false);
            }, uri).ConfigureAwait(false);
        }

        public async Task<DeleteOrderResponse> SendDeleteAsync(string uri, CancelOrderRequest cancelOrderRequest, Dictionary<string, string> headers)
        {
            return await ExecuteAsync<DeleteOrderResponse>(async () =>
            {
                var stringContent = JsonConvert.SerializeObject(cancelOrderRequest);
                var httpContent = new StringContent(stringContent);

                if (headers != null)
                {
                    foreach (var header in headers)
                    {
                        httpContent.Headers.Add(header.Key, header.Value);
                    }
                }

                var message = new HttpRequestMessage
                {
                    Content = httpContent,
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(_client.BaseAddress + uri)
                };

                return await _client.SendAsync(message).ConfigureAwait(false);
            }, uri).ConfigureAwait(false);
        }

        private static async Task<TResponse> ExecuteAsync<TResponse>(Func<Task<HttpResponseMessage>> sendMessageAsync, string path)
        {
            HttpResponseMessage httpResponse = await sendMessageAsync().ConfigureAwait(false);
            string response = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode)
            {
                string errorString = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new InvalidOperationException($"Error while querying {path} - {errorString}");
            }

            return JsonConvert.DeserializeObject<TResponse>(response);
        }
    }
}
