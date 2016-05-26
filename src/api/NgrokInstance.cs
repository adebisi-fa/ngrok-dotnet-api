using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace NgrokApi
{
    public class NgrokInstance
    {
        HttpClient _client;
        public NgrokInstance(string baseAddress)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddress);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public NgrokInstance() :
            this("http://127.0.0.1:4040")
        { }

        public NgrokTunnel[] GetTunnels() =>
            JsonConvert.DeserializeObject<DtoNgrokApiTunnels>(
                _client.GetAsync("api/tunnels").Result.ExtractResponseBody()
            ).Tunnels;

        public string StartTunnel(string addressOrPort = "80", string protocol = "http", NgrokTunnelConfig config = null)
        {
            if (config == null)
                config = new NgrokTunnelConfig();

            config.Address = addressOrPort;
            config.Protocol = protocol;

            if (string.IsNullOrEmpty(config.Name))
                config.Name = "command_line";

            return _client.PostAsync(
                "api/tunnels",
                new StringContent(
                    JsonConvert.SerializeObject(config),
                    Encoding.UTF8, "application/json"
                )).Result.ExtractResponseBody();
        }


        public NgrokTunnel GetTunnelByName(string tunnelName) =>
            JsonConvert.DeserializeObject<NgrokTunnel>(
                _client.GetAsync(Uri.EscapeUriString($"api/tunnels/{tunnelName}")).Result.ExtractResponseBody()
            );

        public void StopTunnel(string tunnelName) =>
            _client.DeleteAsync(Uri.EscapeUriString($"api/tunnels/{tunnelName}")).Wait();
    }
}
