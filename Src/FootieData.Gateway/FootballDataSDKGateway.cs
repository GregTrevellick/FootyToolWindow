using FootballDataSDK.Services;
using FootyData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootieData.Gateway
{
    public class FootballDataSDKGateway
    {
        public LeagueResponse GetLeagueTable(LeagueRequest leagueRequest)
        {
            var client = new FootDataServices();
            //This is Optional (Can be used without Token, but it's limited [request number])
            client.AuthToken = "52109775b1584a93854ca187690ed4bb";

            var leagues = client.SoccerSeasons();
            var premLgeId = leagues.Seasons.Where(x => x.league == "PL").Select(x => x.id).First();
            var tbl = client.LeagueTable(premLgeId);

            var result = new List<Standing>();

            foreach (var sta in tbl.standing)
            {
                result.Add(new Standing
                {
                    Rank = sta.position,
                    Team = sta.teamName,
                    PlayedGames = sta.playedGames,
                    CrestURI = sta.crestURI,
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
