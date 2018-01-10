using FootballDataSDK.Services;
using FootieData.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FootieData.Gateway
{
    public class FootballDataSdkGateway
    {
        private readonly FootDataServices _client;
        private string _leagueCaption;

        public FootballDataSdkGateway()
        {
            _client = new FootDataServices
            {
                AuthToken = "52109775b1584a93854ca187690ed4bb"
            };
        }

        public LeagueStandings GetLeagueResponse_Standings(string leagueIdentifier)
        {
            var result = GetLeagueResponseFromClient_Standings(leagueIdentifier);
            return result;
        }

        public LeagueMatches GetLeagueResponse_Results(string leagueIdentifier)
        {
            var result = GetLeagueResponseFromClient_Matches(leagueIdentifier, "p7");
            return result;
        }

        public LeagueMatches GetLeagueResponse_Fixtures(string leagueIdentifier)
        {
            var result = GetLeagueResponseFromClient_Matches(leagueIdentifier, "n7");
            return result;
        }

        private LeagueStandings GetLeagueResponseFromClient_Standings(string leagueIdentifier)
        {
            var leagueId = GetLeagueId(leagueIdentifier);

            var tbl = _client.LeagueTable(leagueId);

            var result = new LeagueStandings
            {
                Standings = new List<Standing>(),
                LeagueCaption = _leagueCaption
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

            return result;
        }

        private LeagueMatches GetLeagueResponseFromClient_Matches(string leagueIdentifier, string timeFrame)
        {
            var leagueId = GetLeagueId(leagueIdentifier);

            var tbl = _client.Fixtures(leagueId, timeFrame);

            var result = new LeagueMatches
            {
                MatchFixtures = new List<Fixture>(),
                LeagueCaption = _leagueCaption
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
            var leagues = _client.SoccerSeasons();
            var league = leagues.Seasons.First(x => x.league == leagueIdentifier);
            _leagueCaption = league.caption;
            return league.id;
        }
    }
}
