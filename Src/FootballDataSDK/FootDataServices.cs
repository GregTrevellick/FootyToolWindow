using System;
using System.Collections.Generic;
using System.Net;
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
            using (var client = new FootDataHttpClient(AuthToken))
            {
                var res = client.GetAsync(new Uri(url1)).Result;
                var responseString = res.Content.ReadAsStringAsync().Result;

                // Sanity Check
                if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                {
                    var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);
                    return new CompetitionResult { error = err.error };
                }

                var response = JsonConvert.DeserializeObject<IEnumerable<Competition>>(responseString);
                return new CompetitionResult
                {
                    Competitions = response
                };
            }
        }

        public async Task<CompetitionResult> GetCompetitionResultAsync()
        {
            using (var client = new FootDataHttpClient(AuthToken))
            {
                var res = await client.GetAsync(new Uri(url1));
                var responseString = await res.Content.ReadAsStringAsync();

                // Sanity Check
                if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                {
                    var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);
                    return new CompetitionResult { error = err.error };
                }

                var response = JsonConvert.DeserializeObject<IEnumerable<Competition>>(responseString);
                return new CompetitionResult
                {
                    Competitions = response
                };
            }
        }

        public LeagueTableResult GetLeagueTableResult(int idSeason)
        {
            var url = $"{url1}/{idSeason}/leagueTable";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                var res = client.GetAsync(new Uri(url)).Result;
                var responseString = res.Content.ReadAsStringAsync().Result;

                // Sanity Check
                if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                {
                    var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);
                    return new LeagueTableResult { error = err.error };
                }

                var response = JsonConvert.DeserializeObject<LeagueTableResult>(responseString);
                return response;
            }
        }

        public async Task<LeagueTableResult> GetLeagueTableResultAsync(int idSeason)
        {
            var url = $"{url1}/{idSeason}/leagueTable";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                var res = await client.GetAsync(new Uri(url));
                var responseString = await res.Content.ReadAsStringAsync();

                // Sanity Check
                if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                {
                    var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);
                    return new LeagueTableResult { error = err.error };
                }

                var response = JsonConvert.DeserializeObject<LeagueTableResult>(responseString);
                return response;
            }
        }

        public FixturesResult GetFixturesResult(int idSeason, string timeFrame)
        {
            var url = $"{url1}/{idSeason}/fixtures?timeFrame={timeFrame}";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                var res = client.GetAsync(new Uri(url)).Result;
                var responseString = res.Content.ReadAsStringAsync().Result;

                // Sanity Check
                if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                {
                    var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);
                    return new FixturesResult { error = err.error };
                }

                var response = JsonConvert.DeserializeObject<FixturesResult>(responseString);
                return response;
            }
        }

        public async Task<FixturesResult> GetFixturesResultAsync(int idSeason, string timeFrame)
        {
            var url = $"{url1}/{idSeason}/fixtures?timeFrame={timeFrame}";

            using (var client = new FootDataHttpClient(AuthToken))
            {
                var res = await client.GetAsync(new Uri(url));
                var responseString = await res.Content.ReadAsStringAsync();

                // Sanity Check
                if (string.IsNullOrEmpty(responseString) || res.StatusCode != HttpStatusCode.OK)
                {
                    var err = JsonConvert.DeserializeObject<ErrorResult>(responseString);
                    return new FixturesResult { error = err.error };
                }

                var response = JsonConvert.DeserializeObject<FixturesResult>(responseString);
                return response;
            }
        }
    }
}