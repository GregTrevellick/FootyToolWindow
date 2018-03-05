using System;
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
                StandingsResponse leagueTableResult;
                try
                {
                    leagueTableResult = _competitionResultSingleton.FootballDataOrgApiGateway.GetLeagueTableResultAsync(idSeason).Result;
                }
                catch (AggregateException ex)
                {
                    //gregt log exception
                    leagueTableResult = new StandingsResponse { Standing = new List<FootballDataOrg.ResponseEntities.Standing> { new FootballDataOrg.ResponseEntities.Standing { TeamName = EntityConstants.PotentialTimeout } }};
                }                
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
                FixturesResponse fixturesResult;
                try
                {
                    fixturesResult = _competitionResultSingleton.FootballDataOrgApiGateway.GetFixturesResultAsync(idSeason, timeFrame).Result;
                }
                catch (AggregateException ex)
                {
                    //gregt log exception
                    fixturesResult = new FixturesResponse {  Fixtures = new List<Fixture> { new Fixture { HomeTeamName = EntityConstants.PotentialTimeout } } };
                }
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
                FixturesResponse fixturesResult;
                try
                {
                    fixturesResult = _competitionResultSingleton.FootballDataOrgApiGateway.GetFixturesResultAsync(idSeason, timeFrame).Result;
                }
                catch (AggregateException ex)
                {
                    //gregt log exception
                    fixturesResult = new FixturesResponse { Fixtures = new List<Fixture> { new Fixture { HomeTeamName = EntityConstants.PotentialTimeout } } };
                }                
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
                    Against = x.GoalsAgainst,
                    AwayDraws = x.Away?.Draws ?? -1,
                    AwayGoalsAgainst = x.Away?.GoalsAgainst ?? -1,
                    AwayGoalsFor = x.Away?.Goals ?? -1,
                    AwayLosses = x.Away?.Losses ?? -1,
                    AwayWins = x.Away?.Wins ?? -1,
                    //CrestURI = x.CrestURI,
                    Diff = x.GoalDifference,
                    For = x.Goals,
                    HomeDraws = x.Home?.Draws ?? -1,
                    HomeGoalsAgainst = x.Home?.GoalsAgainst ?? -1,
                    HomeGoalsFor = x.Home?.Goals ?? -1,
                    HomeLosses = x.Home?.Losses ?? -1,
                    HomeWins = x.Home?.Wins ?? -1,
                    Played = x.PlayedGames,
                    Points = x.Points,
                    Rank = x.Position,//full
                    //Rank = x.Rank,//minified
                    //Team = MapperHelper.MapExternalTeamNameToInternalTeamName(x.Team),//minified
                    Team = MapperHelper.MapExternalTeamNameToInternalTeamName(x.TeamName),//full
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
