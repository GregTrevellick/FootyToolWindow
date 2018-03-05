using System;
using System.IO;

namespace FootieData.Gateway
{
    public class MapperHelper
    {
        private static void TemporaryWriteTeamNameToTextFile(string externalTeamName)
        {
            File.AppendAllText("VsixFootieTeams.txt", externalTeamName + Environment.NewLine);
        }

        public static string MapExternalTeamNameToInternalTeamName(string externalTeamName)
        {
            TemporaryWriteTeamNameToTextFile(externalTeamName);//gregt comment out

            switch (externalTeamName)
            {
                #region UK
                case "Accrington Stanley":
                    return "Accrington Stanley";
                case "Arsenal":  
                case "Arsenal FC":
                    return "Arsenal";
                case "Aston Villa":   
                case "Aston Villa FC":
                    return "Aston Villa";
                case "Barnet FC":
                    return "Barnet";
                case "Barnsley":  
                case "Barnsley FC":
                    return "Barnsley";
                case "Birmingham":  
                case "Birmingham City":
                    return "Birmingham City";
                case "Blackburn Rovers FC":
                    return "Blackburn Rovers";
                case "Blackpool":  
                case "Blackpool FC":
                    return "Blackpool";
                case "Bolton Wanderers FC":
                    return "Bolton Wanderers";
                case "AFC Bournemouth":  
                case "Bournemouth":
                    return "Bournemouth";
                case "Bradford":  
                case "Bradford City AFC":
                    return "Bradford City";
                case "Brentford FC":
                    return "Brentford";
                case "Brighton":     
                case "Brighton & Hove Albion":
                    return "Brighton & Hove Albion";
                case "Bristol":
                    return "";
                case "Bristol City":
                    return "Bristol City";
                case "Bristol Rovers":
                    return "Bristol Rovers";
                case "Burnley":  
                case "Burnley FC":
                    return "Burnley";
                case "Burton":   
                case "Burton Albion FC":
                    return "Burton Albion";
                case "Bury":  
                case "Bury FC":
                    return "Bury";
                case "Cambridge United":
                    return "Cambridge United";
                case "Cardiff":  
                case "Cardiff City FC":
                    return "Cardiff City";
                case "Carlisle United":
                    return "Carlisle United";
                case "Charlton":   
                case "Charlton Athletic":
                    return "Charlton Athletic";
                case "Chelsea":  
                case "Chelsea FC":
                    return "Chelsea";
                case "Cheltenham":  
                case "Cheltenham Town":
                    return "Cheltenham";
                case "Chesterfield":  
                case "Chesterfield FC":
                    return "Chesterfield";
                case "Colchester":   
                case "Colchester United FC":
                    return "Colchester United";
                case "Coventry":  
                case "Coventry City FC":
                    return "Coventry City";
                case "Crawley Town":
                    return "Crawley Town";
                case "Crewe":   
                case "Crewe Alexandra FC":
                    return "Crewe Alexandra";
                case "Crystal":   
                case "Crystal Palace FC":
                    return "Crystal Palace";
                case "Derby":   
                case "Derby County":
                    return "Derby County";
                case "Doncaster":   
                case "Doncaster Rovers FC":
                    return "Doncaster Rovers";
                case "Everton":  
                case "Everton FC":
                    return "Everton";
                case "Exeter":  
                case "Exeter City":
                    return "Exeter City";
                case "Fleetwood Town FC":
                    return "Fleetwood Town";
                case "Forest Green Rovers":
                    return "Forest Green Rovers";
                case "Fulham":  
                case "Fulham FC":
                    return "Fulham";
                case "Gillingham FC":
                    return "Gillingham";
                case "Grimsby Town":
                    return "Grimsby Town";
                case "Huddersfield":  
                case "Huddersfield Town":
                    return "Huddersfield Town";
                case "Hull":  
                case "Hull City FC":
                    return "Hull City";
                case "Ipswich":  
                case "Ipswich Town":
                    return "Ipswich Town";
                case "Leeds United":
                    return "Leeds United";
                case "Foxes":  
                case "Leicester City FC":
                    return "Leicester City";
                case "Lincoln":  
                case "Lincoln City":
                    return "Lincoln City";
                case "Liverpool":  
                case "Liverpool FC":
                    return "Liverpool";
                case "Luton":  
                case "Luton Town":
                    return "Luton Town";
                case "Manchester City FC":   
                case "ManCity":
                    return "Manchester City";
                case "Manchester United FC":   
                case "ManU":
                    return "Manchester United";
                case "Mansfield":  
                case "Mansfield Town":
                    return "Mansfield Town";
                case "Middlesbrough":  
                case "Middlesbrough FC":
                    return "Middlesbrough";
                case "Millwall":  
                case "Millwall FC":
                    return "Millwall";
                case "Milton Keynes Dons":
                    return "Milton Keynes Dons";
                case "Morecambe":  
                case "Morecambe FC":
                    return "Morecambe";
                case "Newcastle":  
                case "Newcastle United FC":
                    return "Newcastle United";
                case "Newport County":
                    return "Newport County AFC";
                case "Northampton":  
                case "Northampton Town":
                    return "Northampton Town";
                case "Norwich":  
                case "Norwich City FC":
                    return "Norwich City";
                case "Nottingham":   
                case "Nottingham Forest":
                    return "Nottingham Forest";
                case "Notts County":
                    return "Notts County";
                case "Oldham":   
                case "Oldham Athletic AFC":
                    return "Oldham Athletic";
                case "Oxford":  
                case "Oxford United":
                    return "Oxford United";
                case "Peterborough United FC":
                    return "Peterborough United";
                case "Plymouth Argyle":
                    return "Plymouth Argyle";
                case "Port Vale":   
                case "Port Vale FC":
                    return "Port Vale";
                case "Portsmouth":
                    return "Portsmouth";
                case "Preston":    
                case "Preston North End":
                    return "Preston North End";
                case "QPR":    
                case "Queens Park Rangers":
                    return "Queens Park Rangers";
                case "Reading":
                    return "Reading";
                case "Rochdale":  
                case "Rochdale AFC":
                    return "Rochdale";
                case "Rotherham":  
                case "Rotherham United":
                    return "Rotherham United";
                case "Scunthorpe United":  
                case "Scunthorpe United FC":
                    return "Scunthorpe United";
                case "Sheffield":
                case "Sheffield United FC":
                    return "Sheffield United";
                case "Sheffield Wednesday":
                    return "Sheffield Wednesday";
                case "Shrewsbury":  
                case "Shrewsbury Town FC":
                    return "Shrewsbury Town";
                case "Southampton":  
                case "Southampton FC":
                    return "Southampton";
                case "Southend United FC":  
                case "Southend Utd":
                    return "Southend United";
                case "Stevenage FC":
                    return "Stevenage";
                case "Stoke":  
                case "Stoke City FC":
                    return "Stoke City";
                case "Sunderland":  
                case "Sunderland AFC":
                    return "Sunderland";
                case "Swans":  
                case "Swansea City FC":
                    return "Swansea City";
                case "Swindon":  
                case "Swindon Town FC":
                    return "Swindon Town";
                case "Spurs":  
                case "Tottenham Hotspur FC":
                    return "Tottenham Hotspur";
                case "Walsall FC":
                    return "Walsall";
                case "Watford":  
                case "Watford FC":
                    return "Watford";
                case "West Bromwich":  
                case "West Bromwich Albion FC":
                    return "West Bromwich Albion";
                case "West Ham":  
                case "West Ham United FC":
                    return "West Ham United";
                case "Wigan":  
                case "Wigan Athletic FC":
                    return "Wigan Athletic";
                case "AFC Wimbledon":  
                case "Wimbledon":
                    return "AFC Wimbledon";
                case "Wolverhampton Wanderers FC":   
                case "Wolves":
                    return "Wolverhampton Wanderers";
                case "Wycombe":  
                case "Wycombe Wanderers":
                    return "Wycombe Wanderers";
                case "Yeovil":  
                case "Yeovil Town":
                    return "Yeovil Town";
                #endregion

                #region Brazil
                //http://www.correiobraziliense.com.br/
                #endregion

                #region France
                //https://www.lequipe.fr/Football/ligue-1-classement.html
                //https://www.lequipe.fr/Football/ligue-2-classement.html
                #endregion

                #region Italy
                //http://www.gazzetta.it/
                #endregion

                #region Germany
                //https://www.bild.de/sport/fussball/bundesliga/bundesliga-startseite-52368768.bild.html
                #endregion

                #region Netherlands
                //https://www.bndestem.nl/voetbalcenter/klassement/eredivisie/
                #endregion

                #region Portugal
                //https://www.dn.pt/desporto.html
                #endregion

                #region Spain
                //https://resultados.elpais.com/deportivos/futbol/primera/clasificacion/
                #endregion

                default:
                    return externalTeamName;
            }
        }
    }
}



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
