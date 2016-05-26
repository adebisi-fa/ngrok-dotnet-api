using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace NgrokApi
{
    public static class Ngrok
    {
        static NgrokInstance _instance = new NgrokInstance();

        public static NgrokTunnel RandomHttps
        {
            get { return _instance.GetTunnelByName("command_line"); }
        }

        public static NgrokInstance DefaultInstance
        {
            get { return _instance; }
        }

        public static NgrokTunnel RandomHttp
        {
            get { return _instance.GetTunnelByName("command_line (http)"); }
        }

        public static NgrokTunnel[] GetTunnels() => _instance.GetTunnels();

        internal static string ExtractResponseBody(this HttpResponseMessage message) =>
            message?.Content.ReadAsStringAsync().Result;
    }

    class DtoNgrokApiTunnels
    {
        public NgrokTunnel[] Tunnels { get; set; }
        public string Uri { get; set; }
    }
}
