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
        private string leagueCaption;

        public FootballDataSdkGateway()
        {
            client = new FootDataServices
            {
                AuthToken = "52109775b1584a93854ca187690ed4bb"
            };
        }

        public LeagueResponse_Standings GetLeagueResponse_Standings(string leagueIdentifier)
        {
            var result = GetLeagueResponseFromClient_Standings(leagueIdentifier);
            return result;
        }

        public LeagueResponse_Matches GetLeagueResponse_Results(string leagueIdentifier)
        {
            var result = GetLeagueResponseFromClient_Matches(leagueIdentifier, "p7");
            return result;
        }

        public LeagueResponse_Matches GetLeagueResponse_Fixtures(string leagueIdentifier)
        {
            var result = GetLeagueResponseFromClient_Matches(leagueIdentifier, "n7");
            return result;
        }

        private LeagueResponse_Standings GetLeagueResponseFromClient_Standings(string leagueIdentifier)
        {
            var leagueId = GetLeagueId(leagueIdentifier);

            var tbl = client.LeagueTable(leagueId);

            var result = new LeagueResponse_Standings
            {
                Standings = new List<Standing>(),
                LeagueCaption = leagueCaption
            };

            foreach (var sta in tbl.standing)
            {
                result.Standings.Add(new Standing
                {
                    Rank = sta.position,
                    Team = sta.teamName,
                    Played = sta.playedGames,
                    //CrestURI = sta.crestURI,
                    Points = sta.points,
                    For = sta.goals,
                    Against = sta.goalsAgainst,
                    Diff = sta.goalDifference
                });
            }

            return result;// new LeagueResponse_Standings { Standings = result };
        }

        private LeagueResponse_Matches GetLeagueResponseFromClient_Matches(string leagueIdentifier, string timeFrame)
        {
            var leagueId = GetLeagueId(leagueIdentifier);

            var tbl = client.Fixtures(leagueId, timeFrame);

            var result = new LeagueResponse_Matches
            {
                MatchFixtures = new List<Fixture>(),
                LeagueCaption = leagueCaption
            };

            foreach (var item in tbl.fixtures)
            {
                result.MatchFixtures.Add(new Fixture()
                {
                     HomeName= item.homeTeamName,
                     AwayName= item.awayTeamName,
                     Date=item.date,
                     GoalsHome =item.result.goalsAwayTeam,
                     GoalsAway =item.result.goalsAwayTeam,
                });
            }

            return result;
        }

        private int GetLeagueId(string leagueIdentifier)
        {
            var leagues = client.SoccerSeasons();
            var league = leagues.Seasons.First(x => x.league == leagueIdentifier);
            leagueCaption = league.caption;
            return league.id;
        }
    }
}
