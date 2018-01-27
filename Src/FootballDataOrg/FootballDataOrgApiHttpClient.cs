using System;
using System.Net.Http;

namespace FootballDataOrg
{
    class FootballDataOrgApiHttpClient : HttpClient
    {
        public FootballDataOrgApiHttpClient()
        {
            Timeout = TimeSpan.FromSeconds(5);//gregt ????
        }

        public FootballDataOrgApiHttpClient(string token) : this()
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                DefaultRequestHeaders.Add("X-Auth-Token", token);
            }
        }
    }
}