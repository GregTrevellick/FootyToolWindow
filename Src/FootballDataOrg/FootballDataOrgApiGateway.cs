using FootballDataOrg.ResponseEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data.FSharp;

namespace FootballDataOrg
{
    public class FootballDataOrgApiGateway
    {
        private string baseUri = "http://api.football-data.org/v1/competitions";
        private string Token { get; set; }

        public FootballDataOrgApiGateway()
        {
            Token = Frules.FootballDataOrgApiToken;
        }

//gregt revert to async ? convert others to sync ?
        public CompetitionResponseDto GetCompetitionResult()
        {
            var uri = new Uri(baseUri);
            using (var footballDataOrgApiHttpClient = GetFootballDataOrgApiHttpClient())
            {
                var httpResponseMessage = footballDataOrgApiHttpClient.GetAsync(uri).Result;
                var responseString = httpResponseMessage.Content.ReadAsStringAsync().Result;
                if (IsInvalidResponse(responseString, httpResponseMessage))
                {
                    return new CompetitionResponseDto { error = GetError(responseString) };
                }
                else
                {
                    return new CompetitionResponseDto { competitions = DeserializeCompetitions(responseString) };
                }
            }
        }

        //public async Task<CompetitionResponseDto> GetCompetitionResultAsync()
        //{
        //    var uri = new Uri(baseUri);
        //    using (var footballDataOrgApiHttpClient = GetFootballDataOrgApiHttpClient())
        //    {
        //        var httpResponseMessage = await footballDataOrgApiHttpClient.GetAsync(uri);
        //        var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
        //        if (IsInvalidResponse(responseString, httpResponseMessage))
        //        {
        //            return new CompetitionResponseDto { error = GetError(responseString) };
        //        }
        //        else
        //        {
        //            return new CompetitionResponseDto { competitions = DeserializeCompetitions(responseString) };
        //        }
        //    }
        //}

        public StandingsResponse GetLeagueTableResult(int idSeason)
        {
            var uri = new Uri($"{baseUri}/{idSeason}/leagueTable");

            using (var footballDataOrgApiHttpClient = GetFootballDataOrgApiHttpClient())
            {
                var httpResponseMessage = footballDataOrgApiHttpClient.GetAsync(uri).Result;
                var responseString = httpResponseMessage.Content.ReadAsStringAsync().Result;

                if (IsInvalidResponse(responseString, httpResponseMessage))
                {
                    return new StandingsResponse { Error = GetError(responseString) };
                }
                else
                {
                    return JsonConvert.DeserializeObject<StandingsResponse>(responseString);
                }
            }
        }

        //public async Task<StandingsResponse> GetLeagueTableResultAsync(int idSeason)
        //{
        //    var uri = new Uri($"{baseUri}/{idSeason}/leagueTable");
        //    using (var footballDataOrgApiHttpClient = GetFootballDataOrgApiHttpClient())
        //    {
        //        var httpResponseMessage = await footballDataOrgApiHttpClient.GetAsync(uri);                
        //        var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
        //        if (IsInvalidResponse(responseString, httpResponseMessage))
        //        {
        //            return new StandingsResponse { Error = GetError(responseString) };
        //        }
        //        else
        //        {
        //            return JsonConvert.DeserializeObject<StandingsResponse>(responseString);
        //        }
        //    }
        //}

        public async Task<FixturesResponse> GetFixturesResultAsync(int idSeason, string timeFrame)
        {
            var uri = new Uri($"{baseUri}/{idSeason}/fixtures?timeFrame={timeFrame}");

            using (var footballDataOrgApiHttpClient = GetFootballDataOrgApiHttpClient())
            {
                var httpResponseMessage = await footballDataOrgApiHttpClient.GetAsync(uri);        
                var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

                if (IsInvalidResponse(responseString, httpResponseMessage))
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
            return new FootballDataOrgApiHttpClient(Token);
        }

        private static IEnumerable<CompetitionResponse> DeserializeCompetitions(string responseString)
        {
            return JsonConvert.DeserializeObject<IEnumerable<CompetitionResponse>>(responseString);
        }

        private static string GetError(string responseString)
        {
            return JsonConvert.DeserializeObject<ErrorResponse>(responseString).Error;
        }

        private static bool IsInvalidResponse(string responseString, HttpResponseMessage httpResponseMessage)
        {
            return string.IsNullOrEmpty(responseString) || httpResponseMessage.StatusCode != HttpStatusCode.OK;
        }
    }
}


//private static void HandleResponseHeaders(HttpResponseMessage httpResponseMessage, string responseStringError)
//{
//    //You reached your request limit. Wait 52 seconds.
//    //You reached your request limit {X-RequestsAvailable}. Wait {X-RequestCounter-Reset} seconds.
//    IEnumerable<string> values;
//    if (httpResponseMessage.Headers.TryGetValues("X-RequestCounter-Reset", out values))
//    {
//        var secondsLeftToResetRequestCounter = values.First();
//        int.TryParse(secondsLeftToResetRequestCounter, out _secondsLeftToResetRequestCounter);
//    }
//    if (httpResponseMessage.Headers.TryGetValues("X-RequestsAvailable", out values))
//    {
//        //var remainingRequestsBeforeBeingBlocked = values.First();
//        var requestsAvailable = values.First();
//        int.TryParse(requestsAvailable, out _requestsAvailable);
//    }
//}







#region Alternative using RestSharp
//var client = new RestClient(baseUri);
//client.AddDefaultHeader("X-Auth-Token", Token);
//client.AddDefaultHeader("X-Response-Control", "minified");
//var request = new RestRequest($"/{idSeason}/leagueTable", Method.GET);
//var sr = await client.ExecuteTaskAsync<StandingsResponse>(request);
//return sr.Data;
#endregion