using FootballDataSDK.Models.Results;
using FootballDataSDK.Services;
using FootieData.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Standing = FootieData.Entities.Standing;

namespace FootieData.Gateway
{
    public class FootballDataSdkGateway
    {
        private readonly FootDataServices _footDataServices;
        private SoccerSeasonResult _soccerSeasonResult;

        public FootballDataSdkGateway(FootDataServices footDataServices)
        {
            _footDataServices = footDataServices;
        }

        //public async Task<LeagueStandings> GetLeagueResponse_Standings(string leagueIdentifier)
        //{
        //    var result = await GetLeagueResponseFromClient_Standings(leagueIdentifier);
        //    return result;
        //}

        //public async Task<LeagueMatchesResults> GetLeagueResponse_Results(string leagueIdentifier)
        //{
        //    var result = await GetLeagueResponseFromClient_MatchesResult(leagueIdentifier, "p7");
        //    return result;
        //}

        //public async Task<LeagueMatchesFixtures> GetLeagueResponse_Fixtures(string leagueIdentifier)
        //{
        //    var result = await GetLeagueResponseFromClient_MatchesFixture(leagueIdentifier, "n7");
        //    return result;
        //}

        public LeagueStandings GetLeagueResponse_Standings(string leagueIdentifier)
        {
            var result = GetLeagueResponseFromClient_Standings(leagueIdentifier);
            return result;
        }
        public LeagueMatchesResults GetLeagueResponse_Results(string leagueIdentifier)
        {
            var result =  GetLeagueResponseFromClient_MatchesResult(leagueIdentifier, "p7");
            return result;
        }

        public LeagueMatchesFixtures GetLeagueResponse_Fixtures(string leagueIdentifier)
        {
            var result = GetLeagueResponseFromClient_MatchesFixture(leagueIdentifier, "n7");
            return result;
        }


        //private async Task<LeagueStandings> GetLeagueResponseFromClient_Standings(string leagueIdentifier)
        private LeagueStandings GetLeagueResponseFromClient_Standings(string leagueIdentifier)
        {
            var result = new LeagueStandings
            {
                Standings = new List<Standing>(),
            };

            //var leagueId = await GetLeagueId1a(leagueIdentifier);
            var leagueId = GetLeagueId1a(leagueIdentifier);

            if (leagueId != int.MinValue)                       
            {
                //var tbl = await _footDataServices.LeagueTableAsync(leagueId);
                var tbl = _footDataServices.LeagueTable(leagueId);

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
            }

            return result;
        }

        //private async Task<LeagueMatchesResults> GetLeagueResponseFromClient_MatchesResult(string leagueIdentifier, string timeFrame)
        private LeagueMatchesResults GetLeagueResponseFromClient_MatchesResult(string leagueIdentifier, string timeFrame)
        {
            //var leagueId = await GetLeagueIdResult(leagueIdentifier);
            var leagueId =  GetLeagueIdResult(leagueIdentifier);

            //var tbl = await _footDataServices.FixturesAsync(leagueId, timeFrame);
            var tbl = _footDataServices.Fixtures(leagueId, timeFrame);

            var result = new LeagueMatchesResults()
            {
                MatchFixtures = new List<Fixture>(),
            };

            foreach (var item in tbl.fixtures)
            {
                result.MatchFixtures.Add(new Fixture()
                {
                    HomeName = item.homeTeamName,
                    AwayName = item.awayTeamName,
                    Date = item.date,
                    GoalsHome = item.result.goalsAwayTeam,
                    GoalsAway = item.result.goalsAwayTeam,
                });
            }

            return result;
        }

        //private async Task<LeagueMatchesFixtures> GetLeagueResponseFromClient_MatchesFixture(string leagueIdentifier, string timeFrame)
        private LeagueMatchesFixtures GetLeagueResponseFromClient_MatchesFixture(string leagueIdentifier, string timeFrame)
        {
            //var leagueId = await GetLeagueIdFixture(leagueIdentifier);
            var leagueId = GetLeagueIdFixture(leagueIdentifier);

            //var tbl = await _footDataServices.FixturesAsync(leagueId, timeFrame);
            var tbl = _footDataServices.Fixtures(leagueId, timeFrame);

            var result = new LeagueMatchesFixtures()
            {
                MatchFixtures = new List<Fixture>(),
            };

            foreach (var item in tbl.fixtures)
            {
                result.MatchFixtures.Add(new Fixture()
                {
                    HomeName = item.homeTeamName,
                    AwayName = item.awayTeamName,
                    Date = item.date,
                    GoalsHome = item.result.goalsHomeTeam,
                    GoalsAway = item.result.goalsAwayTeam,
                });
            }

            return result;
        }

        //private async Task<int> GetLeagueIdResult(string leagueIdentifier)
        //{
        //    var taskSeasons = await GetLeagueId2r(leagueIdentifier);
        //    return taskSeasons;
        //}
        private int GetLeagueIdResult(string leagueIdentifier)
        {
            var taskSeasons = GetLeagueId2r(leagueIdentifier);
            return taskSeasons;
        }


        //private async Task<int> GetLeagueIdFixture(string leagueIdentifier)
        //{
        //    var taskSeasons = await GetLeagueId2f(leagueIdentifier);
        //    return taskSeasons;
        //}
        private int GetLeagueIdFixture(string leagueIdentifier)
        {
            var taskSeasons = GetLeagueId2f(leagueIdentifier);
            return taskSeasons;
        }

        //private async Task<int> GetLeagueId1a(string leagueIdentifier)
        private int GetLeagueId1a(string leagueIdentifier)
        {
            if (_soccerSeasonResult == null)
            {
                //_soccerSeasonResult = await GetLeagueId1b();
                _soccerSeasonResult = GetLeagueId1b();
                //////////////////////////////////////_soccerSeasonResult = GetLeagueId1b();
            }

            if (_soccerSeasonResult == null ||
                _soccerSeasonResult.Seasons == null||
                _soccerSeasonResult.Seasons.Length == 0)
            {
                //throw new Exception("yep");
                return int.MinValue;
            }
            else
            {
                var league = _soccerSeasonResult.Seasons.First(x => x.league == leagueIdentifier);
                return league.id;
            }            
        }

        //private async Task<SoccerSeasonResult> GetLeagueId1b()
        //{
        //    var result = await _footDataServices.SoccerSeasonsAsync();
        //    return result;
        //}
        private SoccerSeasonResult GetLeagueId1b()
        {
            var result = _footDataServices.SoccerSeasons();
            return result;
        }

        //private async Task<int> GetLeagueId2r(string leagueIdentifier)
        //{
        //    var taskSeasons = await GetLeagueId2rb();
        //    var league = taskSeasons.Seasons.First(x => x.league == leagueIdentifier);
        //    return league.id;
        //}
        private int GetLeagueId2r(string leagueIdentifier)
        {
            var taskSeasons = GetLeagueId2rb();
            var league = taskSeasons.Seasons.First(x => x.league == leagueIdentifier);
            return league.id;
        }

        //private async Task<SoccerSeasonResult> GetLeagueId2rb()
        //{
        //    return await _footDataServices.SoccerSeasonsAsync();
        //}
        private SoccerSeasonResult GetLeagueId2rb()
        {
            return _footDataServices.SoccerSeasons();
        }

        //private async Task<int> GetLeagueId2f(string leagueIdentifier)
        //{
        //    var taskSeasons = await GetLeagueId2fb();
        //    var league = taskSeasons.Seasons.First(x => x.league == leagueIdentifier);
        //    return league.id;
        //}
        private int GetLeagueId2f(string leagueIdentifier)
        {
            var taskSeasons = GetLeagueId2fb();
            var league = taskSeasons.Seasons.First(x => x.league == leagueIdentifier);
            return league.id;
        }

        //private async Task<SoccerSeasonResult> GetLeagueId2fb()
        //{
        //    return await _footDataServices.SoccerSeasonsAsync();
        //}
        private SoccerSeasonResult GetLeagueId2fb()
        {
            return _footDataServices.SoccerSeasons();
        }
    }
}
