using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FootballDataOrg.ResponseEntities;
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

        public CompetitionResponseDto GetCompetitionResult()
        {
            var uri = new Uri(baseUri);

            using (var footballDataOrgApiHttpClient = GetFootballDataOrgApiHttpClient())
            {
                var httpResponseMessage = footballDataOrgApiHttpClient.GetAsync(uri).Result;
                var responseString = httpResponseMessage.Content.ReadAsStringAsync().Result;

                if (BadResponse(responseString, httpResponseMessage))
                {
                    return new CompetitionResponseDto { error = GetError(responseString) };
                }
                else
                {
                    return new CompetitionResponseDto { competitions = DeserializeCompetitions(responseString) };
                }
            }
        }

        //gregt implement this !!!
        //public async Task<CompetitionResult> GetCompetitionResultAsync()
        //{
        //    var uri = new Uri(baseUri);
        //    using (var footballDataOrgApiHttpClient = GetFootballDataOrgApiHttpClient())
        //    {
        //        var httpResponseMessage = await footballDataOrgApiHttpClient.GetAsync(uri);
        //        var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
        //        if (BadResponse(responseString, httpResponseMessage))
        //        {
        //            return new CompetitionResult { error = GetError(responseString) };
        //        }
        //        else
        //        {
        //            return new CompetitionResult { competitions = DeserializeCompetitions(responseString) };
        //        }
        //    }
        //}

        public async Task<StandingsResponse> GetLeagueTableResultAsync(int idSeason)
        {
            var uri = new Uri($"{baseUri}/{idSeason}/leagueTable");

            using (var footballDataOrgApiHttpClient = GetFootballDataOrgApiHttpClient())
            {
                var httpResponseMessage = await footballDataOrgApiHttpClient.GetAsync(uri);
                var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                if (BadResponse(responseString, httpResponseMessage))
                {
                    return new StandingsResponse { Error = GetError(responseString) };
                }
                else
                {
                    return JsonConvert.DeserializeObject<StandingsResponse>(responseString);
                }
            }
        }

        public async Task<FixturesResponse> GetFixturesResultAsync(int idSeason, string timeFrame)
        {
            var uri = new Uri($"{baseUri}/{idSeason}/fixtures?timeFrame={timeFrame}");

            using (var footballDataOrgApiHttpClient = GetFootballDataOrgApiHttpClient())
            {
                var httpResponseMessage = await footballDataOrgApiHttpClient.GetAsync(uri);
                var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                if (BadResponse(responseString, httpResponseMessage))
                {
                    return new FixturesResponse { Error = GetError(responseString) };
                }
                else
                {
                    return JsonConvert.DeserializeObject<FixturesResponse>(responseString);
                }
            }
        }
        
        private FootballDataOrgApiHttpClient GetFootballDataOrgApiHttpClient()
        {
            return new FootballDataOrgApiHttpClient(AuthToken);
        }

        private static IEnumerable<CompetitionResponse> DeserializeCompetitions(string responseString)
        {
            return JsonConvert.DeserializeObject<IEnumerable<CompetitionResponse>>(responseString);
        }

        private static string GetError(string responseString)
        {
            return JsonConvert.DeserializeObject<ErrorResponse>(responseString).Error;
        }

        private static bool BadResponse(string responseString, HttpResponseMessage httpResponseMessage)
        {
            return string.IsNullOrEmpty(responseString) || httpResponseMessage.StatusCode != HttpStatusCode.OK;
        }
    }
}