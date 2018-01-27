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

                //{ "leagueCaption":"Premier League 2017/18","matchday":25,"standing":[{"rank":1,"team":"ManCity","teamId":65,"playedGames":24,"crestURI":"https://upload.wikimedia.org/wikipedia/en/e/eb/Manchester_City_FC_badge.svg","points":65,"goals":70,"goalsAgainst":18,"goalDifference":52},{"rank":2,"team":"ManU","teamId":66,"playedGames":24,"crestURI":"http://upload.wikimedia.org/wikipedia/de/d/da/Manchester_United_FC.svg","points":53,"goals":49,"goalsAgainst":16,"goalDifference":33},{"rank":3,"team":"Chelsea","teamId":61,"playedGames":24,"crestURI":"http://upload.wikimedia.org/wikipedia/de/5/5c/Chelsea_crest.svg","points":50,"goals":45,"goalsAgainst":16,"goalDifference":29},{"rank":4,"team":"Liverpool","teamId":64,"playedGames":24,"crestURI":"http://upload.wikimedia.org/wikipedia/de/0/0a/FC_Liverpool.svg","points":47,"goals":54,"goalsAgainst":29,"goalDifference":25},{"rank":5,"team":"Spurs","teamId":73,"playedGames":24,"crestURI":"http://upload.wikimedia.org/wikipedia/de/b/b4/Tottenham_Hotspur.svg","points":45,"goals":47,"goalsAgainst":22,"goalDifference":25},{"rank":6,"team":"Arsenal","teamId":57,"playedGames":24,"crestURI":"http://upload.wikimedia.org/wikipedia/en/5/53/Arsenal_FC.svg","points":42,"goals":45,"goalsAgainst":31,"goalDifference":14},{"rank":7,"team":"Foxes","teamId":338,"playedGames":24,"crestURI":"http://upload.wikimedia.org/wikipedia/en/6/63/Leicester02.png","points":34,"goals":36,"goalsAgainst":32,"goalDifference":4},{"rank":8,"team":"Burnley","teamId":328,"playedGames":24,"crestURI":"https://upload.wikimedia.org/wikipedia/en/0/02/Burnley_FC_badge.png","points":34,"goals":19,"goalsAgainst":21,"goalDifference":-2},{"rank":9,"team":"Everton","teamId":62,"playedGames":24,"crestURI":"http://upload.wikimedia.org/wikipedia/de/f/f9/Everton_FC.svg","points":28,"goals":26,"goalsAgainst":39,"goalDifference":-13},{"rank":10,"team":"Watford","teamId":346,"playedGames":24,"crestURI":"https://upload.wikimedia.org/wikipedia/en/e/e2/Watford.svg","points":26,"goals":33,"goalsAgainst":44,"goalDifference":-11},{"rank":11,"team":"West Ham","teamId":563,"playedGames":24,"crestURI":"http://upload.wikimedia.org/wikipedia/de/e/e0/West_Ham_United_FC.svg","points":26,"goals":30,"goalsAgainst":42,"goalDifference":-12},{"rank":12,"team":"Bournemouth","teamId":1044,"playedGames":24,"crestURI":"https://upload.wikimedia.org/wikipedia/de/4/41/Afc_bournemouth.svg","points":25,"goals":25,"goalsAgainst":36,"goalDifference":-11},{"rank":13,"team":"Crystal","teamId":354,"playedGames":24,"crestURI":"http://upload.wikimedia.org/wikipedia/de/b/bf/Crystal_Palace_F.C._logo_%282013%29.png","points":25,"goals":22,"goalsAgainst":37,"goalDifference":-15},{"rank":14,"team":"Huddersfield","teamId":394,"playedGames":24,"crestURI":"https://upload.wikimedia.org/wikipedia/en/5/5a/Huddersfield_Town_A.F.C._logo.svg","points":24,"goals":19,"goalsAgainst":41,"goalDifference":-22},{"rank":15,"team":"Newcastle","teamId":67,"playedGames":24,"crestURI":"http://upload.wikimedia.org/wikipedia/de/5/56/Newcastle_United_Logo.svg","points":23,"goals":22,"goalsAgainst":34,"goalDifference":-12},{"rank":16,"team":"Brighton","teamId":397,"playedGames":24,"crestURI":"https://upload.wikimedia.org/wikipedia/en/f/fd/Brighton_%26_Hove_Albion_logo.svg","points":23,"goals":17,"goalsAgainst":33,"goalDifference":-16},{"rank":17,"team":"Stoke","teamId":70,"playedGames":24,"crestURI":"http://upload.wikimedia.org/wikipedia/de/a/a3/Stoke_City.svg","points":23,"goals":25,"goalsAgainst":50,"goalDifference":-25},{"rank":18,"team":"Southampton","teamId":340,"playedGames":24,"crestURI":"http://upload.wikimedia.org/wikipedia/de/c/c9/FC_Southampton.svg","points":22,"goals":24,"goalsAgainst":35,"goalDifference":-11},{"rank":19,"team":"West Bromwich","teamId":74,"playedGames":24,"crestURI":"http://upload.wikimedia.org/wikipedia/de/8/8b/West_Bromwich_Albion.svg","points":20,"goals":19,"goalsAgainst":31,"goalDifference":-12},{"rank":20,"team":"Swans","teamId":72,"playedGames":24,"crestURI":"http://upload.wikimedia.org/wikipedia/de/a/ab/Swansea_City_Logo.svg","points":20,"goals":15,"goalsAgainst":35,"goalDifference":-20}]}

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