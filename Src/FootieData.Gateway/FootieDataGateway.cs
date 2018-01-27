using FootieData.Entities;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using FootballDataOrg;
using FootballDataOrg.Results;
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

        private readonly FootballDataOrgApiGateway _footballDataOrgApiGateway;
        private readonly CompetitionResultSingleton _competitionResultSingleton;

        public FootieDataGateway(FootballDataOrgApiGateway footballDataOrgApiGateway, CompetitionResultSingleton competitionResultSingletonInstance)
        {
            _footballDataOrgApiGateway = footballDataOrgApiGateway;
            _competitionResultSingleton = competitionResultSingletonInstance;
        }

        public IEnumerable<Standing> GetFromClientStandings(string leagueIdentifier)
        {
            IEnumerable<Standing> result = null;
            var idSeason = GetIdSeason(leagueIdentifier);
            if (idSeason > 0)
            {
                var leagueTableResult = _footballDataOrgApiGateway.GetLeagueTableResultAsync(idSeason).Result;
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
                var fixturesResult = _footballDataOrgApiGateway.GetFixturesResultAsync(idSeason, timeFrame).Result;
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
                var fixturesResult = _footballDataOrgApiGateway.GetFixturesResultAsync(idSeason, timeFrame).Result;
                if (fixturesResult != null)
                {
                    result = GetFixtureFutures(fixturesResult);
                }
            }
            return result;
        }

        private int GetIdSeason(string leagueIdentifier)
        {
            //gregt get from new entity here =========================================================================================
            var league = _competitionResultSingleton?.CompetitionResult?.competitions?.SingleOrDefault(x => x.league == leagueIdentifier);
            return league?.id ?? 0;
        }

        private static IEnumerable<Standing> GetResultMatchStandings(LeagueTableResult leagueTableResult)
        {
            if (!string.IsNullOrEmpty(leagueTableResult?.error))
            {
                return new List<Standing>
                {
                    new Standing
                    {
                        Team = leagueTableResult.error
                    }
                };
            }
            else
            {                
                return leagueTableResult?.standing?.Select(x => new Standing
                {
                    //CrestURI = x.crestURI,
                    Against = x.goalsAgainst,
                    Diff = x.goalDifference,
                    For = x.goals,
                    Played = x.playedGames,
                    Points = x.points,
                    Rank = x.rank,//x.position,
                    Team = x.team,//x.teamName,
                });
            }
        }

        private static IEnumerable<FixturePast> GetFixturePasts(FixturesResult fixturesResult)
        {
            if (!string.IsNullOrEmpty(fixturesResult?.error))
            {
                return new List<FixturePast>
                {
                    new FixturePast
                    {
                        HomeName = fixturesResult.error
                    }
                };
            }
            else
            {
                return fixturesResult?.fixtures?.Select(x => new FixturePast
                {
                    AwayName = x.awayTeamName,
                    Date = x.date.ToString("d", enGB),//gregt unit test & remove culture
                    HomeName = x.homeTeamName,
                    GoalsAway = x.result?.goalsAwayTeam,
                    GoalsHome = x.result?.goalsHomeTeam,
                });
            }
        }

        private static IEnumerable<FixtureFuture> GetFixtureFutures(FixturesResult fixturesResult)
        {
            if (!string.IsNullOrEmpty(fixturesResult?.error))
            {
                return new List<FixtureFuture>
                {
                    new FixtureFuture
                    {
                        HomeName = fixturesResult.error
                    }
                };
            }
            else
            {
                return fixturesResult?.fixtures?.Select(x => new FixtureFuture
                {
                    AwayName = x.awayTeamName,
                    Date = x.date.ToString("d", enGB),//gregt unit test & remove culture
                    HomeName = x.homeTeamName,
                    Time = x.date.ToString("t", enUS),//gregt unit test & remove culture
                });
            }
        }
    }
}

//https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings
//2009-06-15T13:45:30 -> 1:45 PM(en-US)
//2009-06-15T13:45:30 -> 13:45 (hr-HR)
//2009-06-15T13:45:30 -> 01:45 م(ar-EG)