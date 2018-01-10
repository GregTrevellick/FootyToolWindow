using System;
using FootballDataSDK.Services;
using System.Collections.Generic;
using System.Linq;
using FootieData.Entities;

namespace FootieData.Gateway
{
    public class FootballDataSdkGateway
    {
        private FootDataServices client;

        public FootballDataSdkGateway()
        {
            client = new FootDataServices
            {
                AuthToken = "52109775b1584a93854ca187690ed4bb"
            };
        }

        public LeagueResponse_Standings GetLeagueResponse_Standings(LeagueRequest leagueRequest)
        {
            var result = GetLeagueResponseFromClient_Standings(leagueRequest);
            return result;
        }

        public LeagueResponse_Matches GetLeagueResponse_Results(LeagueRequest leagueRequest)
        {
            var result = GetLeagueResponseFromClient_Matches(leagueRequest, "p7");
            return result;
        }

        public LeagueResponse_Matches GetLeagueResponse_Fixtures(LeagueRequest leagueRequest)
        {
            var result = GetLeagueResponseFromClient_Matches(leagueRequest,"n7");
            return result;
        }

        private LeagueResponse_Standings GetLeagueResponseFromClient_Standings(LeagueRequest leagueRequest)
        {
            var leagueId = GetLeagueId(leagueRequest);

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

            return new LeagueResponse_Standings { Standings = result };
        }

        private LeagueResponse_Matches GetLeagueResponseFromClient_Matches(LeagueRequest leagueRequest, string timeFrame)
        {
            var leagueId = GetLeagueId(leagueRequest);

            var tbl = client.Fixtures(leagueId, timeFrame);

            var result = new LeagueResponse_Matches();
            result.MatchFixtures = new List<Fixture>();

            foreach (var item in tbl.fixtures)
            {
                //var rslt = new Result
                //{
                //    goalsAwayTeam = item.result.goalsAwayTeam,
                //    goalsHomeTeam = item.result.goalsHomeTeam
                //};

                result.MatchFixtures.Add(new Fixture()
                {
                     HomeTeamName= item.homeTeamName,
                     AwayTeamName= item.awayTeamName,
                     Date=item.date,
                     //Result= rslt
                     goalsHomeTeam =item.result.goalsAwayTeam,
                    goalsAwayTeam =item.result.goalsAwayTeam,
                });
            }

            return result;
        }

        private int GetLeagueId(LeagueRequest leagueRequest)
        {
            var leagues = client.SoccerSeasons();
            var leagueId = leagues.Seasons.Where(x => x.league == leagueRequest.LeagueIdentifier).Select(x => x.id).First();
            return leagueId;
        }
    }
}
