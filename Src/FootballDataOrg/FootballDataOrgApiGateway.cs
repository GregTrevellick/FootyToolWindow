using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FootballDataOrg.Results;
using Newtonsoft.Json;

namespace FootballDataOrg
{
    public class FootballDataOrgApiGateway
    {
        private string baseUri = "http://api.football-data.org/v1/competitions";
        private string AuthToken { get; set; }

        public FootballDataOrgApiGateway(string token)
        {
            AuthToken = token + "b";
        }

        public CompetitionResult GetCompetitionResult()
        {
            var uri = new Uri(baseUri);

            using (var footballDataOrgApiHttpClient = GetFootballDataOrgApiHttpClient())
            {
                var httpResponseMessage = footballDataOrgApiHttpClient.GetAsync(uri).Result;
                var responseString = httpResponseMessage.Content.ReadAsStringAsync().Result;

                if (BadResponse(responseString, httpResponseMessage))
                {
                    return new CompetitionResult { error = GetError(responseString) };
                }
                else
                {
                    return new CompetitionResult { competitions = DeserializeCompetitions(responseString) };
                }
            }
        }

        public async Task<CompetitionResult> GetCompetitionResultAsync()
        {
            var uri = new Uri(baseUri);

            using (var footballDataOrgApiHttpClient = GetFootballDataOrgApiHttpClient())
            {
                var httpResponseMessage = await footballDataOrgApiHttpClient.GetAsync(uri);
                var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                if (BadResponse(responseString, httpResponseMessage))
                {
                    return new CompetitionResult { error = GetError(responseString) };
                }
                else
                {
                    return new CompetitionResult { competitions = DeserializeCompetitions(responseString) };
                }
            }
        }

        public async Task<LeagueTableResult> GetLeagueTableResultAsync(int idSeason)
        {
            var uri = new Uri($"{baseUri}/{idSeason}/leagueTable");

            using (var footballDataOrgApiHttpClient = GetFootballDataOrgApiHttpClient())
            {
                var httpResponseMessage = await footballDataOrgApiHttpClient.GetAsync(uri);
                var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                //{ "rank":1,"team":"ManCity","teamId":65,"playedGames":24,"crestURI":"https://upload.wikimedia.org/wikipedia/en/e/eb/Manchester_City_FC_badge.svg","points":65,"goals":70,"goalsAgainst":18,"goalDifference":52},
                //{ "rank":2,"team":"ManU","teamId":66,"playedGames":24,"crestURI":"http://upload.wikimedia.org/wikipedia/de/d/da/Manchester_United_FC.svg","points":53,"goals":49,"goalsAgainst":16,"goalDifference":33},

                if (BadResponse(responseString, httpResponseMessage))
                {
                    return new LeagueTableResult { error = GetError(responseString) };
                }
                else
                {
                    return JsonConvert.DeserializeObject<LeagueTableResult>(responseString);
                }
            }
        }

        public async Task<FixturesResult> GetFixturesResultAsync(int idSeason, string timeFrame)
        {
            var uri = new Uri($"{baseUri}/{idSeason}/fixtures?timeFrame={timeFrame}");

            using (var footballDataOrgApiHttpClient = GetFootballDataOrgApiHttpClient())
            {
                var httpResponseMessage = await footballDataOrgApiHttpClient.GetAsync(uri);
                var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                if (BadResponse(responseString, httpResponseMessage))
                {
                    return new FixturesResult { error = GetError(responseString) };
                }
                else
                {
                    return JsonConvert.DeserializeObject<FixturesResult>(responseString);
                }
            }
        }
        







        private FootballDataOrgApiHttpClient GetFootballDataOrgApiHttpClient()
        {
            return new FootballDataOrgApiHttpClient(AuthToken);
        }

        private static IEnumerable<Competition> DeserializeCompetitions(string responseString)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Competition>>(responseString);
        }

        private static string GetError(string responseString)
        {
            return JsonConvert.DeserializeObject<ErrorResult>(responseString).error;
        }

        private static bool BadResponse(string responseString, HttpResponseMessage httpResponseMessage)
        {
            return string.IsNullOrEmpty(responseString) || httpResponseMessage.StatusCode != HttpStatusCode.OK;
        }
    }
}