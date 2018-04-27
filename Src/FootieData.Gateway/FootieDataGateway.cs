using FootballDataOrg.ResponseEntities;
using FootieData.Entities;
using FootieData.Entities.ReferenceData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using Standing = FootieData.Entities.Standing;

namespace FootieData.Gateway
{
    public class FootieDataGateway
    {
        private readonly CompetitionResultSingleton _competitionResultSingleton;

        public FootieDataGateway(CompetitionResultSingleton competitionResultSingletonInstance)
        {
            _competitionResultSingleton = competitionResultSingletonInstance;
        }

        public IEnumerable<Standing> GetFromClientStandings(ExternalLeagueCode externalLeagueCode)
        {
            var leagueIdentifier = externalLeagueCode.ToString();

            IEnumerable<Standing> result = null;
            var idSeason = GetIdSeason(leagueIdentifier);
            if (idSeason > 0)
            {
                StandingsResponse leagueTableResult;
                try
                {
                    ////leagueTableResult = _competitionResultSingleton.FootballDataOrgApiGateway.GetLeagueTableResultAsync(idSeason).Result;
                    //leagueTableResult = await _competitionResultSingleton.FootballDataOrgApiGateway.GetLeagueTableResultAsync(idSeason);
                    leagueTableResult = _competitionResultSingleton.FootballDataOrgApiGateway.GetLeagueTableResult(idSeason);
                }
                catch (AggregateException ex)
                {
                    LogAggregateException(ex);
                    leagueTableResult = new StandingsResponse { Standing = new List<FootballDataOrg.ResponseEntities.Standing> { new FootballDataOrg.ResponseEntities.Standing { TeamName = EntityConstants.PotentialTimeout } } };
                }
                if (leagueTableResult != null)
                {
                    result = GetResultMatchStandings(leagueTableResult);
                }
            }
            return result;
        }

        ////////public async Task<IEnumerable<Standing>> GetFromClientStandingsAsync(string leagueIdentifier)
        ////////{
        ////////    IEnumerable<Standing> result = null;
        ////////    var idSeason = GetIdSeason(leagueIdentifier);
        ////////    if (idSeason > 0)
        ////////    {
        ////////        StandingsResponse leagueTableResult;
        ////////        try
        ////////        {
        ////////            //leagueTableResult = _competitionResultSingleton.FootballDataOrgApiGateway.GetLeagueTableResultAsync(idSeason).Result;
        ////////            leagueTableResult = await _competitionResultSingleton.FootballDataOrgApiGateway.GetLeagueTableResultAsync(idSeason);
        ////////        }
        ////////        catch (AggregateException ex)
        ////////        {
        ////////            LogAggregateException(ex);
        ////////            leagueTableResult = new StandingsResponse { Standing = new List<FootballDataOrg.ResponseEntities.Standing> { new FootballDataOrg.ResponseEntities.Standing { TeamName = EntityConstants.PotentialTimeout } }};
        ////////        }
        ////////        if (leagueTableResult != null)
        ////////        {
        ////////            result = GetResultMatchStandings(leagueTableResult);
        ////////        }
        ////////    }
        ////////    return result;
        ////////}

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
                    LogAggregateException(ex);
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
                    LogAggregateException(ex);
                    fixturesResult = new FixturesResponse { Fixtures = new List<Fixture> { new Fixture { HomeTeamName = EntityConstants.PotentialTimeout } } };
                }                
                if (fixturesResult != null)
                {
                    result = GetFixtureFutures(fixturesResult);//.OrderBy(x => new { x.Date, x.HomeName });
                }
            }
            return result;
        }

        private void LogAggregateException(AggregateException ex)
        {
            Logger.Log($"Exception in {nameof(GetFromClientStandings)}" + ex.Message);
        }

        private int GetIdSeason(string leagueIdentifier, bool getViaHttpRequest = true)
        {
            int result;

            //if (getViaHttpRequest)
            //{
                var league = _competitionResultSingleton?.CompetitionResult?.competitions?.SingleOrDefault(x => x.League == leagueIdentifier);
                result = league?.Id ?? 0;
            //}
            //else
            //{
            //    var leagueDto = LeagueMapping.LeagueDtos.FirstOrDefault(x => x.ExternalLeagueCode.ToString() == leagueIdentifier);
            //    result = leagueDto?.ClientLeagueId ?? 0;
            //}

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
                    Date = MapperHelper.GetDate(x.Date, Thread.CurrentThread.CurrentCulture),
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
                    Date = MapperHelper.GetDate(x.Date, Thread.CurrentThread.CurrentCulture),
                    HomeName = MapperHelper.MapExternalTeamNameToInternalTeamName(x.HomeTeamName),
                    Time = MapperHelper.GetTime(x.Date, Thread.CurrentThread.CurrentCulture),
                });
            }
        }
    }
}

