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
                    //CrestURI = x.CrestURI,
                    Against = x.GoalsAgainst,
                    Diff = x.GoalDifference,
                    For = x.Goals,
                    Played = x.PlayedGames,
                    Points = x.Points,
                    Rank = x.Rank,//x.Position,
                    Team = MapExternalTeamNameToInternalTeamName(x.Team)
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
                    AwayName = MapExternalTeamNameToInternalTeamName(x.AwayTeamName),
                    Date = x.Date.ToString("d", enGB),//gregt unit test & replace with current culture
                    HomeName = MapExternalTeamNameToInternalTeamName(x.HomeTeamName),
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
                    AwayName = MapExternalTeamNameToInternalTeamName(x.AwayTeamName),
                    Date = x.Date.ToString("d"),
                    HomeName = MapExternalTeamNameToInternalTeamName(x.HomeTeamName),
                    Time = x.Date.ToString("t"),
                });
            }
        }

        private static string MapExternalTeamNameToInternalTeamName(string externalTeamName)
        {
            switch (externalTeamName.ToLower())
            {
                case "manu":
                    return "Manchester United";
                //"Accrington Stanley",
                //"ADO Den Haag",
                //"AFC Bournemouth",
                //"AFC Wimbledon",
                //"Ajax Amsterdam",
                //"Arsenal FC",
                //"Aston Villa FC",
                //"Atlético Goianiense",
                //"Atlético Mineiro",
                //"Atlético PR",
                //"Avaí SC",
                //"AZ Alkmaar",
                //"Bahia",
                //"Barnet FC",
                //"Barnsley FC",
                //"Birmingham City",
                //"Blackburn Rovers FC",
                //"Blackpool FC",
                //"Bolton Wanderers FC",
                //"Botafogo",
                //"Bradford City AFC",
                //"Brentford FC",
                //"Brighton & Hove Albion",
                //"Bristol City",
                //"Bristol Rovers",
                //"Burnley FC",
                //"Burton Albion FC",
                //"Bury FC",
                //"Cambridge United",
                //"Cardiff City FC",
                //"Carlisle United",
                //"Chapecoense",
                //"Charlton Athletic",
                //"Chelsea FC",
                //"Cheltenham Town",
                //"Chesterfield FC",
                //"Colchester United FC",
                //"Corinthians",
                //"Coritiba FC",
                //"Coventry City FC",
                //"Crawley Town",
                //"Crewe Alexandra FC",
                //"Cruzeiro",
                //"Crystal Palace FC",
                //"Derby County",
                //"Doncaster Rovers FC",
                //"EC Flamengo",
                //"EC Vitória",
                //"Everton FC",
                //"Excelsior",
                //"Exeter City",
                //"FC Groningen",
                //"FC Twente Enschede",
                //"FC Utrecht",
                //"Feyenoord Rotterdam",
                //"Fleetwood Town FC",
                //"Fluminense FC",
                //"Forest Green Rovers",
                //"Fulham FC",
                //"Gillingham FC",
                //"Grémio",
                //"Grimsby Town",
                //"Heracles Almelo",
                //"Huddersfield Town",
                //"Hull City FC",
                //"Ipswich Town",
                //"Leeds United",
                //"Leicester City FC",
                //"Lincoln City",
                //"Liverpool FC",
                //"Luton Town",
                //"Manchester City FC",
                //"Manchester United FC",
                //"Mansfield Town",
                //"Middlesbrough FC",
                //"Millwall FC",
                //"Milton Keynes Dons",
                //"Morecambe FC",
                //"NAC Breda",
                //"Newcastle United FC",
                //"Newport County",
                //"Northampton Town",
                //"Norwich City FC",
                //"Nottingham Forest",
                //"Notts County",
                //"Oldham Athletic AFC",
                //"Oxford United",
                //"Palmeiras",
                //"PEC Zwolle",
                //"Peterborough United FC",
                //"Plymouth Argyle",
                //"Ponte Preta",
                //"Port Vale FC",
                //"Portsmouth",
                //"Preston North End",
                //"PSV Eindhoven",
                //"Queens Park Rangers",
                //"Reading",
                //"Rochdale AFC",
                //"Roda JC Kerkrade",
                //"Rotherham United",
                //"Santos FC",
                //"Sao Paulo",
                //"SC Heerenveen",
                //"Scunthorpe United FC",
                //"Sheffield United FC",
                //"Sheffield Wednesday",
                //"Shrewsbury Town FC",
                //"Southampton FC",
                //"Southend United FC",
                //"Sparta Rotterdam",
                //"Sport Recife",
                //"Stevenage FC",
                //"Stoke City FC",
                //"Sunderland AFC",
                //"Swansea City FC",
                //"Swindon Town FC",
                //"Tottenham Hotspur FC",
                //"Vasco da Gama",
                //"Vitesse Arnhem",
                //"VVV Venlo",
                //"Walsall FC",
                //"Watford FC",
                //"West Bromwich Albion FC",
                //"West Ham United FC",
                //"Wigan Athletic FC",
                //"Willem II Tilburg",
                //"Wolverhampton Wanderers FC",
                //"Wycombe Wanderers",
                //"Yeovil Town",
                default:
                    return externalTeamName;
            }
        }
    }
}

//https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings
//2009-06-15T13:45:30 -> 1:45 PM(en-US)
//2009-06-15T13:45:30 -> 13:45 (hr-HR)
//2009-06-15T13:45:30 -> 01:45 م(ar-EG)