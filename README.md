# Immutable X orders C# client
A c# client for [immutable](https://www.immutable.com/) exchange. It allows for communication with the exchange (creating and cancelling orders) - you can reuse this code to save time.

# Using the client
In class `OfferClient`:

1. Fill the `_ethPublicAddress` with your etherum public key. It can be seen in Metamask, at the top of the default window.
2. Fill the `_ethSecret` and `_starkSecret` with your private keys. Your etherum private key can be found with Metamask in your browser.
3. [This library](https://www.immutabletools.online/) (https://www.immutabletools.online/) is used as an implementation to `ISigner` to sign messages with the defined private keys. It is required by Immutable X exchange.
4. [Optional] Update the `ICommunicationClient` implementation with a custom one, if needed
