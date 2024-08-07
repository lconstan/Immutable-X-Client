namespace ImmutableXClient
{
    public class ImmutableOrderResponse<T>
    {
        public T? Value { get; }
        public string? ErrorMessage { get; }
        public OrderErrorCode ErrorCode { get; }
        public bool IsSuccess { get; }

        public ImmutableOrderResponse(T response)
        {
            Value = response;
            IsSuccess = true;
        }

        public ImmutableOrderResponse(OrderErrorCode errorCode, string message)
        {
            ErrorMessage = message;
            ErrorCode = errorCode;
        }
    }
}
