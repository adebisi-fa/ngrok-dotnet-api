using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NgrokApi
{
    public class NgrokTunnel
    {
        public string Name { get; set; }

        public string Uri { get; set; }

        [JsonProperty("public_url")]
        public string PublicUrl { get; set; }

        public string Proto { get; set; }

        public NgrokTunnelConfig Config { get; set; }

        public NgrokMetrics Metrics { get; set; }
    }

    #region Config

    public class NgrokTunnelConfig
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "command_line";

        /// <summary>
        /// Tunnel protocol name, one of 'http', 'tcp', 'tls'
        /// </summary>
        [JsonProperty("proto")]
        public string Protocol { get; set; }


        /// <summary>
        /// Forward traffic to this local port number or network address
        /// </summary>
        [JsonProperty("addr")]
        public string Address { get; set; }

        /// <summary>
        /// Enable http request inspection
        /// </summary>
        public bool Inspect { get; set; }


        /// <summary>
        /// HTTP basic authentication credentials to enforce on tunneled requests
        /// </summary>
        [JsonProperty("auth")]
        public string HttpBasicAuthenticationCredentials { get; set; }

        /// <summary>
        /// Rewrite the HTTP Host header to this value, or 'preserve' to leave it unchanged
        /// </summary>
        [JsonProperty("host_header")]
        public string HttpHostHeader { get; set; }

        /// <summary>
        /// Bind an HTTPS or HTTP endpoint or both 'true', 'false', or 'both'
        /// </summary>
        [JsonProperty("bind_tls")]
        public string HttpBindTls { get; set; }

        /// <summary>
        /// Subdomain name to request. If unspecified, uses the tunnel name
        /// </summary>
        [JsonProperty("subdomain")]
        public string HttpTlsSubdomain { get; set; }

        /// <summary>
        /// Hostname to request (requires reserved name and DNS CNAME)
        /// </summary>
        [JsonProperty("hostname")]
        public string HttpTlsHostname { get; set; }

        /// <summary>
        /// PEM TLS certificate at this path to terminate TLS traffic before forwarding locally
        /// </summary>
        [JsonProperty("crt")]
        public string TlsCertificate { get; set; }

        /// <summary>
        /// PEM TLS private key at this path to terminate TLS traffic before forwarding locally
        /// </summary>
        [JsonProperty("key")]
        public string TlsKey { get; set; }

        /// <summary>
        /// PEM TLS certificate authority at this path will verify incoming TLS client connection certificates.
        /// </summary>
        [JsonProperty("client_cas")]
        public string TlsClientCertificateAuthority { get; set; }

        /// <summary>
        /// Bind the remote TCP port on the given address
        /// </summary>
        [JsonProperty("remote_addr")]
        public string TcpRemoteAddress { get; set; }
    }

    #endregion

    #region Metrics 

    public class NgrokMetrics
    {
        public NgrokHttpMetrics Http { get; set; }

        [JsonProperty("conns")]
        public NgrokConnectionMetrics Connection { get; set; }
    }

    public abstract class NgrokBaseMetrics
    {
        public int Count { get; set; }
        public double Rate1 { get; set; }
        public double Rate5 { get; set; }
        public double Rate15 { get; set; }

        [JsonProperty("p50")]
        public double Percentile50 { get; set; }

        [JsonProperty("p90")]
        public double Percentile90 { get; set; }

        [JsonProperty("p95")]
        public double Percentile95 { get; set; }

        [JsonProperty("p99")]
        public double Percentile99 { get; set; }
    }

    public class NgrokHttpMetrics : NgrokBaseMetrics { }

    public class NgrokConnectionMetrics : NgrokBaseMetrics
    {
        public int Gauge { get; set; }
    }

    #endregion
}
