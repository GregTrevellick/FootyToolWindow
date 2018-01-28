﻿using FootballDataOrg;
using FootballDataOrg.ResponseEntities;
using FootieData.Entities;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using FootieData.Entities.ReferenceData;
using Standing = FootieData.Entities.Standing;

namespace FootieData.Gateway
{
    public class FootieDataGateway
    {
        //gregt for testing
        private static CultureInfo enUS = new CultureInfo("en-US");
        private static CultureInfo enGB = new CultureInfo("en-GB");
        private static CultureInfo frFR = new CultureInfo("fr-FR");
        private static CultureInfo deDE = new CultureInfo("de-DE");

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
                    result = GetFixturePasts(fixturesResult);
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
                    result = GetFixtureFutures(fixturesResult);
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
                    //CrestURI = x.CrestURI,
                    Against = x.GoalsAgainst,
                    Diff = x.GoalDifference,
                    For = x.Goals,
                    Played = x.PlayedGames,
                    Points = x.Points,
                    Rank = x.Rank,//x.Position,
                    Team = x.Team,//x.TeamName,
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
                    AwayName = x.AwayTeamName,
                    Date = x.Date.ToString("d", enGB),//gregt unit test & remove culture
                    HomeName = x.HomeTeamName,
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
                    AwayName = x.AwayTeamName,
                    Date = x.Date.ToString("d", enGB),//gregt unit test & remove culture
                    HomeName = x.HomeTeamName,
                    Time = x.Date.ToString("t", enUS),//gregt unit test & remove culture
                });
            }
        }
    }
}

//https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings
//2009-06-15T13:45:30 -> 1:45 PM(en-US)
//2009-06-15T13:45:30 -> 13:45 (hr-HR)
//2009-06-15T13:45:30 -> 01:45 م(ar-EG)