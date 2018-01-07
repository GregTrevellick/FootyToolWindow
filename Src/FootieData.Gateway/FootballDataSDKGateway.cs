using FootballDataSDK.Services;
using System.Collections.Generic;
using System.Linq;
using FootieData.Entities;

namespace FootieData.Gateway
{
    public class FootballDataSdkGateway
    {
        public LeagueResponse GetLeagueResponse(LeagueRequest leagueRequest)
        {
            var leagueRequests = new List<LeagueRequest>{leagueRequest};

            var leagueResponses = GetLeagueResponses(leagueRequests);

            return leagueResponses.First();
        }

        public IEnumerable<LeagueResponse> GetLeagueResponses(IEnumerable<LeagueRequest> leagueRequests)
        {
            var leagueResponses = new List<LeagueResponse>();

            foreach (var leagueRequest in leagueRequests)
            {
                leagueResponses.Add(GetLeagueResponseFromClient(leagueRequest));
            }

            return leagueResponses;
        }

        private LeagueResponse GetLeagueResponseFromClient(LeagueRequest leagueRequest)
        {
            var client = new FootDataServices
            {
                AuthToken = "52109775b1584a93854ca187690ed4bb"
            };

            var leagues = client.SoccerSeasons();
            var leagueId = leagues.Seasons.Where(x => x.league == leagueRequest.LeagueIdentifier).Select(x => x.id).First();
            var tbl = client.LeagueTable(leagueId);

            var result = new List<Standing>();

            foreach (var sta in tbl.standing)
            {
                result.Add(new Standing
                {
                    Rank = sta.position,
                    Team = sta.teamName,
                    PlayedGames = sta.playedGames,
                    //CrestURI = sta.crestURI,
                    Points = sta.points,
                    Goals = sta.goals,
                    GoalsAgainst = sta.goalsAgainst,
                    GoalDifference = sta.goalDifference
                });
            }

            return new LeagueResponse { Standings = result };
        }

    }
}
