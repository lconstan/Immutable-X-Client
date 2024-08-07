namespace ImmutableXClient
{
    public enum OrderErrorCode
    {
        Unknown,

        // Order was already cancelled / order does not exist
        NotInTheOrderBook,

        TooManyRequests,

        // We were not fast enough?
        SellOrderDoesNotExist,

        // Order placed by another service instance
        SameStarkKey,

        // Signable cancel order returns null
        NullResponse
    }
}
