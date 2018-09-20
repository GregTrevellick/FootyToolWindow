using FootballDataOrg.ResponseEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.FSharp;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using v2c = FootballDataOrg.ResponseEntities.DeserializationTargets.V2.Competition;
using v2f = FootballDataOrg.ResponseEntities.DeserializationTargets.V2.Fixtures;
using v2s = FootballDataOrg.ResponseEntities.DeserializationTargets.V2.Standings;

namespace FootballDataOrg
{
    public class FootballDataOrgApiGateway
    {
        private string baseUri = "http://api.football-data.org/v2/competitions";
        private string dateFrom = string.Empty;
        private string dateTo = string.Empty;
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
            var uri = new Uri($"{baseUri}/{idSeason}/standings");

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
                    var rootobject = JsonConvert.DeserializeObject<v2s.Rootobject>(responseString);
                    var standings = GetStandings(rootobject);
                    return new StandingsResponse
                    {
                        Standing = standings
                    };
                }
            }
        }

        public async Task<FixturesResponse> GetFixturesResultAsync(int idSeason, string timeFrame)
        {
            SetDateRange(timeFrame);

            var uri = new Uri($"{baseUri}/{idSeason}/matches?dateFrom={dateFrom}&dateTo={dateTo}");

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
                    var rootobject = JsonConvert.DeserializeObject<v2f.Rootobject>(responseString);

                    var fixtures = new List<Fixture>();
                    foreach (var match in rootobject.matches)
                    {
                        var fixture = GetFixture(match);
                        fixtures.Add(fixture);
                    }

                    return new FixturesResponse
                    {
                        Fixtures = fixtures
                    };
                }
            }
        }

        private void SetDateRange(string timeFrame)
        {
            var dateFormat = "yyyy-MM-dd";
            var now = DateTime.UtcNow;
            var formattedNow = now.ToString(dateFormat);

            switch (timeFrame)
            {
                case "p7":
                    dateFrom = now.AddDays(-7).ToString(dateFormat);
                    dateTo = formattedNow;
                    break;
                case "n7":
                    dateFrom = formattedNow;
                    dateTo = now.AddDays(7).ToString(dateFormat);
                    break;
            }
        }

        private FootballDataOrgApiHttpClient GetFootballDataOrgApiHttpClient()
        {
            return new FootballDataOrgApiHttpClient(Token);
        }

        private static IEnumerable<CompetitionResponse> DeserializeCompetitions(string responseString)
        {
            var rootobject = JsonConvert.DeserializeObject<v2c.Rootobject>(responseString);

            var competitionResponses = new List<CompetitionResponse>();

            foreach (var competition in rootobject.competitions)
            {
                var competitionResponse = GetCompetitionResponse(competition);
                competitionResponses.Add(competitionResponse);
            }

            return competitionResponses;
        }

        private static string GetError(string responseString)
        {
            return JsonConvert.DeserializeObject<ErrorResponse>(responseString).Error;
        }

        private static bool IsInvalidResponse(string responseString, HttpResponseMessage httpResponseMessage)
        {
            return string.IsNullOrEmpty(responseString) || httpResponseMessage.StatusCode != HttpStatusCode.OK;
        }

        private static CompetitionResponse GetCompetitionResponse(v2c.Competition competition)
        {
            return new CompetitionResponse
            {
                Id = competition.id,
                League = competition.name
            };
        }

        private static Fixture GetFixture(v2f.Match match)
        {
            return new Fixture
            {
                AwayTeamName = match.awayTeam.name,
                Date = match.utcDate,
                Status = match.status,
                HomeTeamName = match.homeTeam.name,
                Result = new ResponseEntities.HomeAway.HomeAwayGoals
                {
                    GoalsAwayTeam = match.score.fullTime.awayTeam,
                    GoalsHomeTeam = match.score.fullTime.homeTeam
                }
            };
        }

        private static List<Standing> GetStandings(v2s.Rootobject rootobject)
        {
            var triple = rootobject.standings.ToList();
            var total = triple.Single(x => x.type == "TOTAL").table;
            var home = triple.Single(x => x.type == "HOME").table;
            var away = triple.Single(x => x.type == "AWAY").table;

            var standings = new List<Standing>();
            for (int i = 0; i < total.Length; i++)
            {
                var standing = GetStanding(total, home, away, i);
                standings.Add(standing);
            }

            return standings;
        }

        private static Standing GetStanding(v2s.Table[] total, v2s.Table[] home, v2s.Table[] away, int i)
        {
            return new Standing
            {
                Position = total[i].position,
                TeamName = total[i].team.name,
                PlayedGames = total[i].playedGames,
                Points = total[i].points,
                Goals = total[i].goalsFor,
                GoalsAgainst = total[i].goalsAgainst,
                GoalDifference = total[i].goalDifference,
                Wins = total[i].won,
                Draws = total[i].draw,
                Losses = total[i].lost,
                Home = new ResponseEntities.HomeAway.Home
                {
                    Draws = home[i].draw,
                    Goals = home[i].goalsFor,
                    GoalsAgainst = home[i].goalsAgainst,
                    Losses = home[i].lost,
                    Wins = home[i].won
                },
                Away = new ResponseEntities.HomeAway.Away
                {
                    Draws = away[i].draw,
                    Goals = away[i].goalsFor,
                    GoalsAgainst = away[i].goalsAgainst,
                    Losses = away[i].lost,
                    Wins = away[i].won
                }
            };
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