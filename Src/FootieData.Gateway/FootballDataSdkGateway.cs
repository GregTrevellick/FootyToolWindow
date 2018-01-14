using FootballDataSDK.Services;
using FootieData.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballDataSDK.Models.Results;
using Standing = FootieData.Entities.Standing;

namespace FootieData.Gateway
{
    public class FootballDataSdkGateway
    {
        private readonly FootDataServices _client;

        public FootballDataSdkGateway()
        {
            _client = new FootDataServices
            {
                AuthToken = "52109775b1584a93854ca187690ed4bb"
            };
        }

        public async Task<LeagueStandings> GetLeagueResponse_Standings(string leagueIdentifier)
        {
            var result = await GetLeagueResponseFromClient_Standings(leagueIdentifier);
            return result;
        }

        public async Task<LeagueMatches> GetLeagueResponse_Results(string leagueIdentifier)
        {
            var result = await GetLeagueResponseFromClient_Matches(leagueIdentifier, "p30");
            return result;
        }

        public async Task<LeagueMatches> GetLeagueResponse_Fixtures(string leagueIdentifier)
        {
            var result = await GetLeagueResponseFromClient_Matches(leagueIdentifier, "n30");
            return result;
        }

        private async Task<LeagueStandings> GetLeagueResponseFromClient_Standings(string leagueIdentifier)
        {
            var leagueId = await GetLeagueId(leagueIdentifier);

            var tbl = await _client.LeagueTableAsync(leagueId);

            var result = new LeagueStandings
            {
                Standings = new List<Standing>(),
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

        private async Task<LeagueMatches> GetLeagueResponseFromClient_Matches(string leagueIdentifier, string timeFrame)
        {
            var leagueId = await GetLeagueId(leagueIdentifier);

            var tbl = await _client.FixturesAsync(leagueId, timeFrame);

            var result = new LeagueMatches
            {
                MatchFixtures = new List<Fixture>(),
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

        private async Task<int> GetLeagueId(string leagueIdentifier)
        {
            var taskSeasons = await GetLeagueId2(leagueIdentifier);
            var league = taskSeasons.Seasons.First(x => x.league == leagueIdentifier);
            return league.id;
        }

        private async Task<SoccerSeasonResult> GetLeagueId2(string leagueIdentifier)
        {
            return await _client.SoccerSeasonsAsync();
        }
    }
}
