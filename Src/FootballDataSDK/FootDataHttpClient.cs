using System;
using System.Net.Http;

namespace FootballDataSDK
{
    class FootDataHttpClient : HttpClient
    {
        public FootDataHttpClient()
        {
            this.Timeout = TimeSpan.FromSeconds(5);
        }
        public FootDataHttpClient(string token) : this()
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                this.DefaultRequestHeaders.Add("X-Auth-Token", token);
            }
        }
    }
}
