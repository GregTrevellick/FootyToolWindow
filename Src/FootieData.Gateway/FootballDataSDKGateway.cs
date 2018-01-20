using FootballDataSDK.Services;
using FootieData.Entities;
using System.Collections.Generic;
using System.Linq;
using FootballDataSDK.Models.Results;
using Standing = FootieData.Entities.Standing;

namespace FootieData.Gateway
{
    public class FootballDataSdkGateway
    {
        private readonly FootDataServices _footDataServices;
        private readonly SoccerSeasonResultSingleton _soccerSeasonResultSingleton;

        public FootballDataSdkGateway(FootDataServices footDataServices, SoccerSeasonResultSingleton soccerSeasonResultSingletonInstance)
        {
            _footDataServices = footDataServices;

            _soccerSeasonResultSingleton = soccerSeasonResultSingletonInstance;
        }

        public LeagueStandings GetFromClientStandings(string leagueIdentifier)
        {
            var result = new LeagueStandings
            {
                //Standings = new List<Standing>(),
            };

            var idSeason = GetIdSeason(leagueIdentifier);

            var leagueTableResult = _footDataServices.LeagueTable(idSeason);

            if (leagueTableResult != null)
            {
                result.Standings = GetResultMatchStandings(leagueTableResult);
            }

            return result;
        }

        public LeagueMatchesResults GetFromClientResults(string leagueIdentifier, string timeFrame)
        {
            var result = new LeagueMatchesResults
            {
                //MatchFixtures = new List<Fixture>(),
            };

            var idSeason = GetIdSeason(leagueIdentifier);

            var fixturesResult = _footDataServices.Fixtures(idSeason, timeFrame);

            if (fixturesResult != null)
            {
                result.MatchFixtures = GetResultMatchFixtures(fixturesResult);
            }

            return result;
        }

        public LeagueMatchesFixtures GetFromClientFixtures(string leagueIdentifier, string timeFrame)
        {
            var result = new LeagueMatchesFixtures
            {
               // MatchFixtures = new List<Fixture>(),
            };

            var idSeason = GetIdSeason(leagueIdentifier);

            var tbl = _footDataServices.Fixtures(idSeason, timeFrame);

            if (tbl != null)
            {
                result.MatchFixtures = GetResultMatchFixtures(tbl);
            }

            return result;
        }

        private int GetIdSeason(string leagueIdentifier)
        {
            var league = _soccerSeasonResultSingleton.SoccerSeasonResult.Seasons.Single(x => x.league == leagueIdentifier);
            return league.id;
        }

        private static IEnumerable<Standing> GetResultMatchStandings(LeagueTableResult leagueTableResult)
        {
            return leagueTableResult.standing.Select(x => new Standing
            {
                Rank = x.position,
                Team = x.teamName,
                Played = x.playedGames,
                //CrestURI = x.crestURI,
                Points = x.points,
                For = x.goals,
                Against = x.goalsAgainst,
                Diff = x.goalDifference
            });
        }

        private static IEnumerable<Fixture> GetResultMatchFixtures(FixturesResult fixturesResult)
        {
            return fixturesResult.fixtures.Select(x => new Fixture
            {
                HomeName = x.homeTeamName,
                AwayName = x.awayTeamName,
                Date = x.date,
                GoalsHome = x.result.goalsHomeTeam,
                GoalsAway = x.result.goalsAwayTeam,
            });
        }

    }
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

//private async Task<int> GetLeagueIdResult(string leagueIdentifier)
//{
//    var taskSeasons = await GetLeagueId2r(leagueIdentifier);
//    return taskSeasons;
//}
//private int GetLeagueIdResult(string leagueIdentifier)
//{
//    var taskSeasons = GetLeagueId4(leagueIdentifier);
//    return taskSeasons;
//}

//private async Task<int> GetLeagueIdFixture(string leagueIdentifier)
//{
//    var taskSeasons = await GetLeagueId2f(leagueIdentifier);
//    return taskSeasons;
//}
//private int GetLeagueIdFixture(string leagueIdentifier)
//{
//    var taskSeasons = GetLeagueId4(leagueIdentifier);
//    return taskSeasons;
//}

//private int GetLeagueId5(string leagueIdentifier)
//{
//    var taskSeasons = GetLeagueId4(leagueIdentifier);
//    return taskSeasons;
//}
//private async Task<SoccerSeasonResult> GetLeagueId1b()
//{
//    var result = await _footDataServices.SoccerSeasonsAsync();
//    return result;
//}
//private SoccerSeasonResult GetLeagueId1b()
//{
//    var result = GetLeagueId3();//_footDataServices.SoccerSeasons();
//    return result;
//}

//private async Task<int> GetLeagueId2r(string leagueIdentifier)
//{
//    var taskSeasons = await GetLeagueId2rb();
//    var league = taskSeasons.Seasons.First(x => x.league == leagueIdentifier);
//    return league.id;
//}
//private int GetLeagueId2r(string leagueIdentifier)
//{
//    var taskSeasons = GetLeagueId3();
//    var league = taskSeasons.Seasons.First(x => x.league == leagueIdentifier);
//    return league.id;
//}

//private async Task<SoccerSeasonResult> GetLeagueId2rb()
//{
//    return await _footDataServices.SoccerSeasonsAsync();
//}
//private SoccerSeasonResult GetLeagueId2rb()
//{
//    return GetLeagueId3();//_footDataServices.SoccerSeasons();
//}

//private async Task<int> GetLeagueId2f(string leagueIdentifier)
//{
//    var taskSeasons = await GetLeagueId2fb();
//    var league = taskSeasons.Seasons.First(x => x.league == leagueIdentifier);
//    return league.id;
//}
//private int GetLeagueId2f(string leagueIdentifier)
//{
//    var taskSeasons = GetLeagueId3();
//    var league = taskSeasons?.Seasons?.First(x => x.league == leagueIdentifier);
//    return league == null ? 0 : league.id;
//}

//private async Task<SoccerSeasonResult> GetLeagueId2fb()
//{
//    return await _footDataServices.SoccerSeasonsAsync();
//}
//private SoccerSeasonResult GetLeagueId2fb()
//{
//    return GetLeagueId3();//_footDataServices.SoccerSeasons();
//}


//public LeagueStandings GetLeagueResponse_Standings(string leagueIdentifier)
//{
//    var result = GetLeagueResponseFromClient_Standings(leagueIdentifier);
//    return result;
//}
//public LeagueMatchesResults GetLeagueResponse_Results(string leagueIdentifier)
//{
//    var result =  GetLeagueResponseFromClient_MatchesResult(leagueIdentifier, "p7");
//    return result;
//}

//public LeagueMatchesFixtures GetLeagueResponse_Fixtures(string leagueIdentifier)
//{
//    var result = GetLeagueResponseFromClient_MatchesFixture(leagueIdentifier, "n7");
//    return result;
//}





//private int GetLeagueId4(string leagueIdentifier)
//{
//    var soccerSeasonResult = GetLeagueId3();
//    var league = soccerSeasonResult?.Seasons?.First(x => x.league == leagueIdentifier);
//    return league == null ? 0 : league.id;
//}




//private int GetLeagueId1a(string leagueIdentifier)
//{
//    if (_soccerSeasonResult == null)
//    {
//        _soccerSeasonResult = GetLeagueId3();
//   }

//    if (_soccerSeasonResult == null ||
//        _soccerSeasonResult.Seasons == null||
//        _soccerSeasonResult.Seasons.Length == 0)
//    {
//        //throw new Exception("yep");
//        return int.MinValue;
//    }
//    else
//    {
//        var league = _soccerSeasonResult.Seasons.First(x => x.league == leagueIdentifier);
//        return league.id;
//    }            
//}

//private SoccerSeasonResult GetLeagueId3()
//{
//    return _footDataServices.SoccerSeasons();
//}
