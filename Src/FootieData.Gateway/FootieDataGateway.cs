using FootballDataOrg.ResponseEntities;
using FootieData.Entities;
using FootieData.Entities.ReferenceData;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Standing = FootieData.Entities.Standing;

namespace FootieData.Gateway
{
    public class FootieDataGateway
    {
        //For for testing
        //private static CultureInfo enUS = new CultureInfo("en-US");
        private static CultureInfo enGB = new CultureInfo("en-GB");//gregt
        //private static CultureInfo frFR = new CultureInfo("fr-FR");
        //private static CultureInfo deDE = new CultureInfo("de-DE");

        private readonly CompetitionResultSingleton _competitionResultSingleton;

        public FootieDataGateway(CompetitionResultSingleton competitionResultSingletonInstance)
        {
            _competitionResultSingleton = competitionResultSingletonInstance;
        }

        public IEnumerable<Standing> GetFromClientStandings(string leagueIdentifier)
        {
            IEnumerable<Standing> result = null;
            var idSeason = GetIdSeason(leagueIdentifier);
            if (idSeason > 0)
            {
                var leagueTableResult = _competitionResultSingleton.FootballDataOrgApiGateway.GetLeagueTableResultAsync(idSeason).Result;
                if (leagueTableResult != null)
                {
                    result = GetResultMatchStandings(leagueTableResult);
                }
            }
            return result;
        }

        public IEnumerable<FixturePast> GetFromClientFixturePasts(string leagueIdentifier, string timeFrame)
        {
            IEnumerable<FixturePast> result = null;
            var idSeason = GetIdSeason(leagueIdentifier);
            if (idSeason > 0)
            {
                var fixturesResult = _competitionResultSingleton.FootballDataOrgApiGateway.GetFixturesResultAsync(idSeason, timeFrame).Result;
                if (fixturesResult != null)
                {
                    result = GetFixturePasts(fixturesResult);//.OrderBy(x => new { x.Date, x.HomeName }); ;
                }
            }
            return result;
        }

        public IEnumerable<FixtureFuture> GetFromClientFixtureFutures(string leagueIdentifier, string timeFrame)
        {
            IEnumerable<FixtureFuture> result = null;
            var idSeason = GetIdSeason(leagueIdentifier);
            if (idSeason > 0)
            {
                var fixturesResult = _competitionResultSingleton.FootballDataOrgApiGateway.GetFixturesResultAsync(idSeason, timeFrame).Result;
                if (fixturesResult != null)
                {
                    result = GetFixtureFutures(fixturesResult);//.OrderBy(x => new { x.Date, x.HomeName });
                }
            }
            return result;
        }

        private int GetIdSeason(string leagueIdentifier, bool getViaHttpRequest = false)
        {
            int result;

            if (getViaHttpRequest)
            {
                var league = _competitionResultSingleton?.CompetitionResult?.competitions?.SingleOrDefault(x => x.League == leagueIdentifier);
                result = league?.Id ?? 0;
            }
            else
            {
                var leagueDto = LeagueMapping.LeagueDtos.FirstOrDefault(x => x.ExternalLeagueCode.ToString() == leagueIdentifier);
                result = leagueDto?.ClientLeagueId ?? 0;
            }

            return result;
        }

        private static IEnumerable<Standing> GetResultMatchStandings(StandingsResponse leagueTableResult)
        {
            if (!string.IsNullOrEmpty(leagueTableResult?.Error))
            {
                return new List<Standing>
                {
                    new Standing
                    {
                        Team = leagueTableResult.Error
                    }
                };
            }
            else
            {                
                return leagueTableResult?.Standing?.Select(x => new Standing
                {
                    //gregt sort alpha

                    //CrestURI = x.CrestURI,
                    Against = x.GoalsAgainst,
                    Diff = x.GoalDifference,
                    For = x.Goals,
                    Played = x.PlayedGames,
                    Points = x.Points,
                    //Rank = x.Rank,//minified
                    Rank = x.Position,//full
                    //Team = MapperHelper.MapExternalTeamNameToInternalTeamName(x.Team),//minified
                    Team = MapperHelper.MapExternalTeamNameToInternalTeamName(x.TeamName),//full

                    HomeWins = x.Home.Wins,
                    HomeDraws = x.Home.Draws,
                    HomeLosses = x.Home.Losses,
                    HomeGoalsFor = x.Home.Goals,
                    HomeGoalsAgainst = x.Home.GoalsAgainst,

                    AwayWins = x.Away.Wins,
                    AwayDraws = x.Away.Draws,
                    AwayLosses = x.Away.Losses,
                    AwayGoalsFor = x.Away.Goals,
                    AwayGoalsAgainst = x.Away.GoalsAgainst,
                });
            }
        }

        private static IEnumerable<FixturePast> GetFixturePasts(FixturesResponse fixturesResult)
        {
            if (!string.IsNullOrEmpty(fixturesResult?.Error))
            {
                return new List<FixturePast>
                {
                    new FixturePast
                    {
                        HomeName = fixturesResult.Error
                    }
                };
            }
            else
            {
                return fixturesResult?.Fixtures?.Select(x => new FixturePast
                {
                    AwayName = MapperHelper.MapExternalTeamNameToInternalTeamName(x.AwayTeamName),
                    Date = x.Date.ToString("d", enGB),//gregt unit test & replace with current culture
                    HomeName = MapperHelper.MapExternalTeamNameToInternalTeamName(x.HomeTeamName),
                    GoalsAway = x.Result?.GoalsAwayTeam,
                    GoalsHome = x.Result?.GoalsHomeTeam,
                });
            }
        }

        private static IEnumerable<FixtureFuture> GetFixtureFutures(FixturesResponse fixturesResult)
        {
            if (!string.IsNullOrEmpty(fixturesResult?.Error))
            {
                return new List<FixtureFuture>
                {
                    new FixtureFuture
                    {
                        HomeName = fixturesResult.Error
                    }
                };
            }
            else
            {
                return fixturesResult?.Fixtures?.Select(x => new FixtureFuture
                {
                    AwayName = MapperHelper.MapExternalTeamNameToInternalTeamName(x.AwayTeamName),
                    Date = x.Date.ToString("d"),
                    HomeName = MapperHelper.MapExternalTeamNameToInternalTeamName(x.HomeTeamName),
                    Time = x.Date.ToString("t"),
                });
            }
        }
    }
}

//https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings
//2009-06-15T13:45:30 -> 1:45 PM(en-US)
//2009-06-15T13:45:30 -> 13:45 (hr-HR)
//2009-06-15T13:45:30 -> 01:45 م(ar-EG)
