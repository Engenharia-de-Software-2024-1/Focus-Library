using System.Net;
using System.Net.Http;

namespace Networking
{
    public class NetworkingClient
    {
        public HttpClient Client { get; private set; }

        public NetworkingClient()
        {
            Client = new HttpClient();
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = 
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            
            Client = new HttpClient(handler);
            Client.DefaultRequestHeaders.Add("Accept", "application/json");
            Client.DefaultRequestHeaders.Add("User-Agent", "UnityApp");
            Client.DefaultRequestHeaders.Add("ngrok-skip-browser-warning", "true");
        }
    }
}