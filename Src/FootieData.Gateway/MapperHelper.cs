using System;
using System.IO;

namespace FootieData.Gateway
{
    public class MapperHelper
    {
        public static string MapExternalTeamNameToInternalTeamName(string externalTeamName)
        {
            TemporaryWriteTeamNameToTextFile(externalTeamName);

            switch (externalTeamName)
            {
                case "Manu":
                    return "Manchester United";
                //case "AS Monaco FC":
                //case "Boavista Porto FC":
                //case "Bologna FC":
                //case "Carpi FC":
                //case "Chamois Niortais FC":
                //case "Coritiba FC":
                //case "Empoli FC":
                //case "Fluminense FC":
                //case "Genoa CFC":
                //case "Girona FC":
                //case "Hellas Verona FC":
                //case "Moreirense FC":
                //case "Paris FC":
                //case "Parma FC":
                //case "Santos FC":
                //case "Sevilla FC":
                //case "Sochaux FC":
                //case "Stade Rennais FC":
                //case "Torino FC":
                //case "Toulouse FC":
                case "Arsenal FC":
                case "Barnet FC":
                case "Barnsley FC":
                case "Blackburn Rovers FC":
                case "Blackpool FC":
                case "Bolton Wanderers FC":
                case "Brentford FC":
                case "Burnley FC":
                case "Burton Albion FC":
                case "Bury FC":
                case "Cardiff City FC":
                case "Chelsea FC":
                case "Chesterfield FC":
                case "Colchester United FC":
                case "Coventry City FC":
                case "Crewe Alexandra FC":
                case "Crystal Palace FC":
                case "Doncaster Rovers FC":
                case "Everton FC":
                case "Fleetwood Town FC":                
                case "Fulham FC":
                case "Gillingham FC":
                case "Hull City FC":
                case "Leicester City FC":
                case "Liverpool FC":
                case "Manchester City FC":
                case "Manchester United FC":
                case "Middlesbrough FC":
                case "Millwall FC":
                case "Morecambe FC":
                case "Newcastle United FC":
                case "Norwich City FC":
                case "Peterborough United FC":
                case "Port Vale FC":                
                case "Scunthorpe United FC":
                case "Sheffield United FC":
                case "Shrewsbury Town FC":
                case "Southampton FC":
                case "Southend United FC":
                case "Stevenage FC":
                case "Stoke City FC":
                case "Swansea City FC":
                case "Swindon Town FC":
                case "Tottenham Hotspur FC":
                case "Walsall FC":
                case "Watford FC":
                case "West Bromwich Albion FC":
                case "West Ham United FC":
                case "Wigan Athletic FC":
                case "Wolverhampton Wanderers FC":
                    return externalTeamName.Replace(" FC", string.Empty);

                case "AFC Bournemouth":
                case "AFC Wimbledon":
                    return externalTeamName.Replace("AFC ", string.Empty);

                case "Bradford City AFC":
                case "Oldham Athletic AFC":
                case "Rochdale AFC":
                case "Sunderland AFC":
                    return externalTeamName.Replace(" AFC ", string.Empty);
















                //"1. FC Heidenheim 1846",
                //"1. FC Kaiserslautern",
                //"1. FC Köln",
                //"1. FC Nürnberg",
                //"1. FC Union Berlin",
                //"1. FSV Mainz 05",
                //"AC Cesena",
                //"AC Chievo Verona",
                //"AC Milan",
                //"Accrington Stanley",
                //"ACF Fiorentina",
                //"ADO Den Haag",
                //"AJ Auxerre",
                //"Ajaccio AC",
                //"Ajax Amsterdam",
                //"Amiens SC",
                //"Angers SCO",
                //"Arminia Bielefeld",
                //"Arsenal FC",
                //"AS Avellino 1912",
                //"AS Bari",
                //"AS Cittadella",
                //"AS Monaco FC",
                //"AS Nancy",
                //"AS Roma",
                //"AS Saint-Étienne",
                //"Ascoli",
                //"Aston Villa FC":
                //"Atalanta BC",
                //"Athletic Club",
                //"Atlético Goianiense",
                //"Atlético Mineiro",
                //"Atlético PR",
                //"Avaí SC",
                //"AZ Alkmaar",
                //"Bahia",
                //"Barnet FC",
                //"Barnsley FC",
                //"Bayer Leverkusen",
                //"Benevento Calcio",
                //"Birmingham City",
                //"Blackburn Rovers FC",
                //"Blackpool FC",
                //"Boavista Porto FC",
                //"Bologna FC",
                //"Bolton Wanderers FC",
                //"Bor. Mönchengladbach",
                //"Borussia Dortmund",
                //"Botafogo",
                //"Bradford City AFC",
                //"Brentford FC",
                //"Brescia Calcio",
                //"Brighton & Hove Albion",
                //"Bristol City",
                //"Bristol Rovers",
                //"Burnley FC",
                //"Burton Albion FC",
                //"Bury FC",
                //"C.F. Os Belenenses",
                //"Cagliari Calcio",
                //"Cambridge United",
                //"Cardiff City FC",
                //"Carlisle United",
                //"Carpi FC",
                //"CD Leganes",
                //"CD Tondela",
                //"Chamois Niortais FC",
                //"Chapecoense",
                //"Charlton Athletic",
                //"Chelsea FC",
                //"Cheltenham Town",
                //"Chesterfield FC",
                //"Clermont Foot Auvergne",
                //"Club Atlético de Madrid",
                //"Colchester United FC",
                //"Corinthians",
                //"Coritiba FC",
                //"Coventry City FC",
                //"Crawley Town",
                //"Cremonese",
                //"Crewe Alexandra FC",
                //"Cruzeiro",
                //"Crystal Palace FC",
                //"Deportivo Alavés",
                //"Derby County",
                //"Desportivo Aves",
                //"Dijon FCO",
                //"Doncaster Rovers FC",
                //"Dynamo Dresden",
                //"EA Guingamp",
                //"EC Flamengo",
                //"EC Vitória",
                //"Eintracht Braunschweig",
                //"Eintracht Frankfurt",
                //"Empoli FC",
                //"Erzgebirge Aue",
                //"ES Troyes AC",
                //"Everton FC",
                //"Excelsior",
                //"Exeter City",
                //"FC Augsburg",
                //"FC Barcelona",
                //"FC Bayern München",
                //"FC Bourg-en-Bresse Péronnas",
                //"FC Crotone",
                //"FC Girondins de Bordeaux",
                //"FC Groningen",
                //"FC Ingolstadt 04",
                //"FC Internazionale Milano",
                //"FC Lorient",
                //"FC Metz",
                //"FC Nantes",
                //"FC Paços de Ferreira",
                //"FC Porto",
                //"FC Rio Ave",
                //"FC Schalke 04",
                //"FC St. Pauli",
                //"FC Twente Enschede",
                //"FC Utrecht",
                //"FC Valenciennes",
                //"Feirense",
                //"Feyenoord Rotterdam",
                //"Fleetwood Town FC",
                //"Fluminense FC",
                //"Foggia",
                //"Forest Green Rovers",
                //"Fortuna Düsseldorf",
                //"Frosinone Calcio",
                //"Fulham FC",
                //"G.D. Chaves",
                //"Gazélec Ajaccio",
                //"GD Estoril Praia",
                //"Genoa CFC",
                //"Getafe CF",
                //"Gillingham FC",
                //"Girona FC",
                //"Grémio",
                //"Grimsby Town",
                //"Hamburger SV",
                //"Hannover 96",
                //"Hellas Verona FC",
                //"Heracles Almelo",
                //"Hertha BSC",
                //"Holstein Kiel",
                //"Huddersfield Town",
                //"Hull City FC",
                //"Ipswich Town",
                //"Jahn Regensburg",
                //"Juventus Turin",
                //"LB Châteauroux",
                //"Le Havre AC",
                //"Leeds United",
                //"Leicester City FC",
                //"Levante UD",
                //"Lincoln City",
                //"Liverpool FC",
                //"Luton Town",
                //"Málaga CF",
                //"Manchester City FC",
                //"Manchester United FC",
                //"Mansfield Town",
                //"Maritimo Funchal",
                //"Middlesbrough FC",
                //"Millwall FC",
                //"Milton Keynes Dons",
                //"Montpellier Hérault SC",
                //"Morecambe FC",
                //"Moreirense FC",
                //"MSV Duisburg",
                //"NAC Breda",
                //"Newcastle United FC",
                //"Newport County",
                //"Nîmes Olympique",
                //"Northampton Town",
                //"Norwich City FC",
                //"Nottingham Forest",
                //"Notts County",
                //"Novara Calcio",
                //"OGC Nice",
                //"Oldham Athletic AFC",
                //"Olympique de Marseille",
                //"Olympique Lyonnais",
                //"OSC Lille",
                //"Oxford United",
                //"Palmeiras",
                //"Paris FC",
                //"Paris Saint-Germain",
                //"Parma FC",
                //"PEC Zwolle",
                //"Perugia",
                //"Pescara Calcio",
                //"Peterborough United FC",
                //"Plymouth Argyle",
                //"Ponte Preta",
                //"Port Vale FC",
                //"Portimonense S.C.",
                //"Portsmouth",
                //"Preston North End",
                //"Pro Vercelli",
                //"PSV Eindhoven",
                //"Queens Park Rangers",
                //"Quevilly Rouen",
                //"RC Celta de Vigo",
                //"RC Deportivo La Coruna",
                //"RC Lens",
                //"RC Strasbourg Alsace",
                //"RC Tours",
                //"RCD Espanyol",
                //"Reading",
                //"Real Betis",
                //"Real Madrid CF",
                //"Real Sociedad de Fútbol",
                //"Red Bull Leipzig",
                //"Rochdale AFC",
                //"Roda JC Kerkrade",
                //"Rotherham United",
                //"Salernitana Calcio",
                //"Santos FC",
                //"Sao Paulo",
                //"SC Freiburg",
                //"SC Heerenveen",
                //"Scunthorpe United FC",
                //"SD Eibar",
                //"Sevilla FC",
                //"Sheffield United FC",
                //"Sheffield Wednesday",
                //"Shrewsbury Town FC",
                //"SL Benfica",
                //"SM Caen",
                //"Sochaux FC",
                //"Southampton FC",
                //"Southend United FC",
                //"SPAL Ferrara",
                //"Sparta Rotterdam",
                //"Spezia Calcio",
                //"Sport Recife",
                //"Sporting Braga",
                //"Sporting CP",
                //"SpVgg Greuther Fürth",
                //"SS Lazio",
                //"SSC Napoli",
                //"Stade Brestois",
                //"Stade de Reims",
                //"Stade Rennais FC",
                //"Stevenage FC",
                //"Stoke City FC",
                //"Sunderland AFC",
                //"SV Darmstadt 98",
                //"SV Sandhausen",
                //"Swansea City FC",
                //"Swindon Town FC",
                //"Ternana Calcio",
                //"Torino FC",
                //"Tottenham Hotspur FC",
                //"Toulouse FC",
                //"TSG 1899 Hoffenheim",
                //"UC Sampdoria",
                //"UD Las Palmas",
                //"Udinese Calcio",
                //"US Cittá di Palermo",
                //"US Orleans",
                //"US Sassuolo Calcio",
                //"Valencia CF",
                //"Vasco da Gama",
                //"Venezia",
                //"VfB Stuttgart",
                //"VfL Bochum",
                //"VfL Wolfsburg",
                //"Villarreal CF",
                //"Virtus Entella",
                //"Vitesse Arnhem",
                //"Vitoria Guimaraes",
                //"Vitoria Setubal",
                //"VVV Venlo",
                //"Walsall FC",
                //"Watford FC",
                //"Werder Bremen",
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

        private static void TemporaryWriteTeamNameToTextFile(string externalTeamName)
        {
            File.AppendAllText("VsixFootieTeams.txt", externalTeamName + Environment.NewLine);                        
        }
    }
}
