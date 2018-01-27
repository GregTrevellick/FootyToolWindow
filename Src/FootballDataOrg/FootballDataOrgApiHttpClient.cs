using System;
using System.Net.Http;

namespace FootballDataOrg
{
    class FootballDataOrgApiHttpClient : HttpClient
    {
        public FootballDataOrgApiHttpClient()
        {
            Timeout = TimeSpan.FromSeconds(30);//gregt put into VS options
            //This value, if exceeded on the http call, gives rise to this error:
            //TaskCanceledException: A task was canceled.
            //https://stackoverflow.com/questions/29179848/httpclient-a-task-was-cancelled
        }

        public FootballDataOrgApiHttpClient(string token) : this()
        {
            if (!string.IsNullOrWhiteSpace(token))
            {
                DefaultRequestHeaders.Add("X-Auth-Token", token);
            }

            //Control the appearance of the response
            // "full" is default
            // "minified" will lack some (meta) information and thus be much smaller
            // "compressed" is currently only supported by the fixture resource
            DefaultRequestHeaders.Add("X-Response-Control", "minified");
        }
    }
}