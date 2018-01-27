using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FootballDataSDK.Common;
using FootballDataSDK.Results;
using Newtonsoft.Json;

namespace FootballDataSDK
{
    public class FootDataServices
    {
        private string url1 = "http://api.football-data.org/v1/competitions";
        private string AuthToken { get; set; }

        public FootDataServices(string token)
        {
            AuthToken = token + "b";
        }

        public CompetitionResult GetCompetitionResult()
        {
            using (var footballDataOrgApiHttpClient = new FootballDataOrgApiHttpClient(AuthToken))
            {
                var httpResponseMessage = footballDataOrgApiHttpClient.GetAsync(new Uri(url1)).Result;
                var responseString = httpResponseMessage.Content.ReadAsStringAsync().Result;

                if (string.IsNullOrEmpty(responseString) || httpResponseMessage.StatusCode != HttpStatusCode.OK)
                {
                    return new CompetitionResult
                    {
                        error = JsonConvert.DeserializeObject<ErrorResult>(responseString).error
                    };
                }
                else
                {
                    return new CompetitionResult
                    {
                        competitions = JsonConvert.DeserializeObject<IEnumerable<Competition>>(responseString)
                    };
                }
            }
        }

        public async Task<CompetitionResult> GetCompetitionResultAsync()
        {
            var url = $"{url1}";

            using (var footballDataOrgApiHttpClient = new FootballDataOrgApiHttpClient(AuthToken))
            {
                var httpResponseMessage = await footballDataOrgApiHttpClient.GetAsync(new Uri(url));
                var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseString) || httpResponseMessage.StatusCode != HttpStatusCode.OK)
                {
                    return new CompetitionResult
                    {
                        error = JsonConvert.DeserializeObject<ErrorResult>(responseString).error
                    };
                }
                else
                {
                    return new CompetitionResult
                    {
                        competitions = JsonConvert.DeserializeObject<IEnumerable<Competition>>(responseString)
                    };
                }
            }
        }

        public async Task<LeagueTableResult> GetLeagueTableResultAsync(int idSeason)
        {
            var url = $"{url1}/{idSeason}/leagueTable";

            using (var footballDataOrgApiHttpClient = new FootballDataOrgApiHttpClient(AuthToken))
            {
                var httpResponseMessage = await footballDataOrgApiHttpClient.GetAsync(new Uri(url));
                var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseString) || httpResponseMessage.StatusCode != HttpStatusCode.OK)
                {
                    return new LeagueTableResult { error = JsonConvert.DeserializeObject<ErrorResult>(responseString).error };
                }
                else
                {
                    return JsonConvert.DeserializeObject<LeagueTableResult>(responseString);
                }
            }
        }

        public async Task<FixturesResult> GetFixturesResultAsync(int idSeason, string timeFrame)
        {
            var url = $"{url1}/{idSeason}/fixtures?timeFrame={timeFrame}";

            using (var footballDataOrgApiHttpClient = new FootballDataOrgApiHttpClient(AuthToken))
            {
                var httpResponseMessage = await footballDataOrgApiHttpClient.GetAsync(new Uri(url));
                var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseString) || httpResponseMessage.StatusCode != HttpStatusCode.OK)
                {
                    return new FixturesResult
                    {
                        error = JsonConvert.DeserializeObject<ErrorResult>(responseString).error
                    };
                }
                else
                {
                    return JsonConvert.DeserializeObject<FixturesResult>(responseString);
                }
            }
        }
    }
}