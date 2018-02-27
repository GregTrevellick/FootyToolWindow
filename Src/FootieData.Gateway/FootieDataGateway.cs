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
"1. FC Heidenheim 1846",
"1. FC Kaiserslautern",
"1. FC Köln",
"1. FC Nürnberg",
"1. FC Union Berlin",
"1. FSV Mainz 05",
"AC Cesena",
"AC Chievo Verona",
"AC Milan",
"Accrington Stanley",
"ACF Fiorentina",
"ADO Den Haag",
"AFC Bournemouth",
"AFC Wimbledon",
"AJ Auxerre",
"Ajaccio AC",
"Ajax Amsterdam",
"Amiens SC",
"Angers SCO",
"Arminia Bielefeld",
"Arsenal FC",
"AS Avellino 1912",
"AS Bari",
"AS Cittadella",
"AS Monaco FC",
"AS Nancy",
"AS Roma",
"AS Saint-Étienne",
"Ascoli",
"Aston Villa FC",
"Atalanta BC",
"Athletic Club",
"Atlético Goianiense",
"Atlético Mineiro",
"Atlético PR",
"Avaí SC",
"AZ Alkmaar",
"Bahia",
"Barnet FC",
"Barnsley FC",
"Bayer Leverkusen",
"Benevento Calcio",
"Birmingham City",
"Blackburn Rovers FC",
"Blackpool FC",
"Boavista Porto FC",
"Bologna FC",
"Bolton Wanderers FC",
"Bor. Mönchengladbach",
"Borussia Dortmund",
"Botafogo",
"Bradford City AFC",
"Brentford FC",
"Brescia Calcio",
"Brighton & Hove Albion",
"Bristol City",
"Bristol Rovers",
"Burnley FC",
"Burton Albion FC",
"Bury FC",
"C.F. Os Belenenses",
"Cagliari Calcio",
"Cambridge United",
"Cardiff City FC",
"Carlisle United",
"Carpi FC",
"CD Leganes",
"CD Tondela",
"Chamois Niortais FC",
"Chapecoense",
"Charlton Athletic",
"Chelsea FC",
"Cheltenham Town",
"Chesterfield FC",
"Clermont Foot Auvergne",
"Club Atlético de Madrid",
"Colchester United FC",
"Corinthians",
"Coritiba FC",
"Coventry City FC",
"Crawley Town",
"Cremonese",
"Crewe Alexandra FC",
"Cruzeiro",
"Crystal Palace FC",
"Deportivo Alavés",
"Derby County",
"Desportivo Aves",
"Dijon FCO",
"Doncaster Rovers FC",
"Dynamo Dresden",
"EA Guingamp",
"EC Flamengo",
"EC Vitória",
"Eintracht Braunschweig",
"Eintracht Frankfurt",
"Empoli FC",
"Erzgebirge Aue",
"ES Troyes AC",
"Everton FC",
"Excelsior",
"Exeter City",
"FC Augsburg",
"FC Barcelona",
"FC Bayern München",
"FC Bourg-en-Bresse Péronnas",
"FC Crotone",
"FC Girondins de Bordeaux",
"FC Groningen",
"FC Ingolstadt 04",
"FC Internazionale Milano",
"FC Lorient",
"FC Metz",
"FC Nantes",
"FC Paços de Ferreira",
"FC Porto",
"FC Rio Ave",
"FC Schalke 04",
"FC St. Pauli",
"FC Twente Enschede",
"FC Utrecht",
"FC Valenciennes",
"Feirense",
"Feyenoord Rotterdam",
"Fleetwood Town FC",
"Fluminense FC",
"Foggia",
"Forest Green Rovers",
"Fortuna Düsseldorf",
"Frosinone Calcio",
"Fulham FC",
"G.D. Chaves",
"Gazélec Ajaccio",
"GD Estoril Praia",
"Genoa CFC",
"Getafe CF",
"Gillingham FC",
"Girona FC",
"Grémio",
"Grimsby Town",
"Hamburger SV",
"Hannover 96",
"Hellas Verona FC",
"Heracles Almelo",
"Hertha BSC",
"Holstein Kiel",
"Huddersfield Town",
"Hull City FC",
"Ipswich Town",
"Jahn Regensburg",
"Juventus Turin",
"LB Châteauroux",
"Le Havre AC",
"Leeds United",
"Leicester City FC",
"Levante UD",
"Lincoln City",
"Liverpool FC",
"Luton Town",
"Málaga CF",
"Manchester City FC",
"Manchester United FC",
"Mansfield Town",
"Maritimo Funchal",
"Middlesbrough FC",
"Millwall FC",
"Milton Keynes Dons",
"Montpellier Hérault SC",
"Morecambe FC",
"Moreirense FC",
"MSV Duisburg",
"NAC Breda",
"Newcastle United FC",
"Newport County",
"Nîmes Olympique",
"Northampton Town",
"Norwich City FC",
"Nottingham Forest",
"Notts County",
"Novara Calcio",
"OGC Nice",
"Oldham Athletic AFC",
"Olympique de Marseille",
"Olympique Lyonnais",
"OSC Lille",
"Oxford United",
"Palmeiras",
"Paris FC",
"Paris Saint-Germain",
"Parma FC",
"PEC Zwolle",
"Perugia",
"Pescara Calcio",
"Peterborough United FC",
"Plymouth Argyle",
"Ponte Preta",
"Port Vale FC",
"Portimonense S.C.",
"Portsmouth",
"Preston North End",
"Pro Vercelli",
"PSV Eindhoven",
"Queens Park Rangers",
"Quevilly Rouen",
"RC Celta de Vigo",
"RC Deportivo La Coruna",
"RC Lens",
"RC Strasbourg Alsace",
"RC Tours",
"RCD Espanyol",
"Reading",
"Real Betis",
"Real Madrid CF",
"Real Sociedad de Fútbol",
"Red Bull Leipzig",
"Rochdale AFC",
"Roda JC Kerkrade",
"Rotherham United",
"Salernitana Calcio",
"Santos FC",
"Sao Paulo",
"SC Freiburg",
"SC Heerenveen",
"Scunthorpe United FC",
"SD Eibar",
"Sevilla FC",
"Sheffield United FC",
"Sheffield Wednesday",
"Shrewsbury Town FC",
"SL Benfica",
"SM Caen",
"Sochaux FC",
"Southampton FC",
"Southend United FC",
"SPAL Ferrara",
"Sparta Rotterdam",
"Spezia Calcio",
"Sport Recife",
"Sporting Braga",
"Sporting CP",
"SpVgg Greuther Fürth",
"SS Lazio",
"SSC Napoli",
"Stade Brestois",
"Stade de Reims",
"Stade Rennais FC",
"Stevenage FC",
"Stoke City FC",
"Sunderland AFC",
"SV Darmstadt 98",
"SV Sandhausen",
"Swansea City FC",
"Swindon Town FC",
"Ternana Calcio",
"Torino FC",
"Tottenham Hotspur FC",
"Toulouse FC",
"TSG 1899 Hoffenheim",
"UC Sampdoria",
"UD Las Palmas",
"Udinese Calcio",
"US Cittá di Palermo",
"US Orleans",
"US Sassuolo Calcio",
"Valencia CF",
"Vasco da Gama",
"Venezia",
"VfB Stuttgart",
"VfL Bochum",
"VfL Wolfsburg",
"Villarreal CF",
"Virtus Entella",
"Vitesse Arnhem",
"Vitoria Guimaraes",
"Vitoria Setubal",
"VVV Venlo",
"Walsall FC",
"Watford FC",
"Werder Bremen",
"West Bromwich Albion FC",
"West Ham United FC",
"Wigan Athletic FC",
"Willem II Tilburg",
"Wolverhampton Wanderers FC",
"Wycombe Wanderers",
"Yeovil Town",
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
