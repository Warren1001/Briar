using System.Net.WebSockets;

using BlossomiShymae.Briar.GameClient;
using BlossomiShymae.Briar.Rest;
using BlossomiShymae.Briar.Utils;
using BlossomiShymae.Briar.WebSocket;

using Microsoft.Extensions.Logging;

using Websocket.Client;

namespace BlossomiShymae.Briar
{
    /// <summary>
    /// Connector to exchange requests with the League Client.
    /// </summary>
    public static class Connector
    {
        /// <summary>
        /// Create a Websocket client with logger for the League Client.
        /// </summary>
        /// <returns></returns>
        public static LcuWebsocketClient CreateLcuWebsocketClient(ILogger<WebsocketClient>? logger)
        {
            var processInfo = ProcessFinder.GetProcessInfo();
            var riotAuthentication = new RiotAuthentication(processInfo.RemotingAuthToken);
            var uri = new Uri($"wss://127.0.0.1:{processInfo.AppPort}/");
            ClientWebSocket factory() => new()
            {
                Options =
                {
                    Credentials = riotAuthentication.ToNetworkCredential(),
                    RemoteCertificateValidationCallback = (a, b, c, d) => true,
                },
            };

            var client = new LcuWebsocketClient(uri, logger, factory);

            return client;
        }

        /// <summary>
        /// Create a Websocket client for the League client.
        /// </summary>
        /// <returns></returns>
        public static LcuWebsocketClient CreateLcuWebsocketClient() =>
            CreateLcuWebsocketClient(null);

        /// <summary>
        /// Get a shared instance of the HTTP client for the League Client.
        /// </summary>
        /// <returns></returns>
        public static LcuHttpClient GetLcuHttpClientInstance()
        {
            return LcuHttpClient.GetInstance();
        }

        /// <summary>
        /// Get a shared instance of the HTTP client for the Game Client.
        /// </summary>
        /// <returns></returns>
        public static GameHttpClient GetGameHttpClientInstance()
        {
            return GameHttpClient.Instance;
        }
    }
}