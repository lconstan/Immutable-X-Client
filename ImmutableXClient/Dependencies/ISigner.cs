namespace ImmutableXClient.Dependencies
{
    // See https://www.immutabletools.online/
    internal interface ISigner
    {
        string EthSign(string ethPrivateKey, string signableMessage);
        string StartkSign(string starkPrivateKey, string signableMessage);
    }
}
