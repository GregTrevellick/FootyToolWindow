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
            //TemporaryWriteTeamNameToTextFile(externalTeamName);

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
                //https://www.df.superesportes.com.br/campeonatos/2017/brasileirao/serie-a/
                case "Atlético Goianiense": return "Atlético-GO";
                case "Atlético Mineiro": return "Atlético-MG";
                case "Atlético PR": return "Atlético-PR";
                case "Avaí SC": return "Avaí";
                case "Bahia": return " Bahia";
                case "Botafogo": return "Botafogo";
                case "Chapecoense": return "Chapecoense";
                case "Corinthians": return "Corinthians";
                case "Coritiba FC": return "Coritiba";
                case "Cruzeiro": return "Cruzeiro";
                case "EC Flamengo": return "Flamengo";
                case "EC Vitória": return "Vitória";
                case "Fluminense FC": return "Fluminense";
                case "Grémio": return "Grêmio";
                case "Palmeiras": return "Palmeiras";
                case "Ponte Preta": return "Ponte Preta";
                case "Santos FC": return "Santos";
                case "Sao Paulo": return "São Paulo";
                case "Sport Recife": return "Sport";
                case "Vasco da Gama": return "Vasco";
                #endregion

                #region France
                //https://www.lequipe.fr/Football/ligue-1-classement.html
                //https://www.lequipe.fr/Football/ligue-2-classement.html
                case "AJ Auxerre": return "Auxerre";
                case "Ajaccio AC":
                case "Gazélec Ajaccio":
                    return "AC Ajaccio";
                case "Amiens SC": return "Amiens";
                case "Angers SCO": return "Angers";
                case "AS Monaco FC": return "Monaco";
                case "AS Nancy": return "Nancy";
                case "AS Saint-Étienne": return "Saint-Étienne";
                case "Chamois Niortais FC": return "Niort";
                case "Clermont Foot Auvergne": return "Clermont";
                case "Dijon FCO": return "Dijon";
                case "EA Guingamp": return "Guingamp";
                case "ES Troyes AC": return "Troyes";
                case "FC Bourg - en - Bresse Péronnas": return "Bourg-en-Bresse";
                case "FC Girondins de Bordeaux": return "Bordeaux";
                case "FC Lorient": return "Lorient";
                case "FC Metz": return "Metz";
                case "FC Nantes": return "Nantes";
                case "FC Valenciennes": return "Valenciennes";
                case "LB Châteauroux": return "Châteauroux";
                case "Le Havre AC": return "Le Havre";
                case "Montpellier Hérault SC": return "Montpellier";
                case "Nîmes Olympique": return "Nîmes";
                case "OGC Nice": return "Nice";
                case "Olympique de Marseille": return "Marseille";
                case "Olympique Lyonnais": return "Lyon";
                case "OSC Lille": return "Lille";
                case "Paris FC": return "Paris FC";
                case "Paris Saint - Germain": 
                case "Paris Saint-Germain":
                    return "Paris-SG";
                case "Quevilly Rouen": return "Quevilly-Rouen";
                case "RC Lens": return "Lens";
                case "RC Strasbourg Alsace": return "Strasbourg";
                case "RC Tours": return "Tours";
                case "SM Caen": return "Caen";
                case "Sochaux FC": return "Sochaux";
                case "Stade Brestois": return "Brest";
                case "Stade de Reims": return "Reims";
                case "Stade Rennais FC": return "Rennes";
                case "Toulouse FC": return "Toulouse";
                case "US Orleans": return "Nancy";
                #endregion

                #region Italy
                //http://www.gazzetta.it/speciali/risultati_classifiche/2018/calcio/seriea/classifica.shtml
                case "AC Cesena": return "Cesena";
                case "AC Chievo Verona": return "Verona";
                case "AC Milan": return "Milan";
                case "ACF Fiorentina": return "Fiorentina";
                case "AS Avellino 1912": return "Avellino";
                case "AS Bari": return "Bari";
                case "AS Cittadella": return "Cittadella";
                case "AS Roma": return "Roma";
                case "Ascoli": return "Ascoli";
                case "Atalanta BC": return "Atalanta";
                case "Benevento Calcio": return "Benevento";
                case "Bologna FC": return "Bologna";
                case "Brescia Calcio": return "Brescia";
                case "Cagliari Calcio": return "Cagliari";
                case "Carpi FC": return "Carpi";
                case "Cremonese": return "Cremonese";
                case "Empoli FC": return "Empoli";
                case "FC Crotone": return "Crotone";
                case "FC Internazionale Milano": return "Inter";
                case "Foggia": return "Foggia";
                case "Frosinone Calcio": return "Frosinone";
                case "Genoa CFC": return "Genoa";
                case "Hellas Verona FC": return "Verona";
                case "Juventus Turin": return "Juventus";
                case "Novara Calcio": return "Novara";
                case "Parma FC": return "Parma";
                case "Perugia": return "Perugia";
                case "Pescara Calcio": return "Pescara";
                case "Pro Vercelli": return "Pro Vercelli";
                case "Salernitana Calcio": return "Salernitana";
                case "SPAL Ferrara": return "Spal";
                case "Spezia Calcio": return "Spezia";
                case "SS Lazio": return "Lazio";
                case "SSC Napoli": return "Napoli";
                case "Ternana Calcio": return "Ternana U.";
                case "Torino FC": return "Torino";
                case "UC Sampdoria": return "Sampdoria";
                case "Udinese Calcio": return "Udinese";
                case "US Cittá di Palermo": return "Palermo";
                case "US Sassuolo Calcio": return "Sassuolo";
                case "Venezia": return "Venezia";
                case "Virtus Entella": return "Entella";
                #endregion

                #region Germany 
                //https://www.bild.de/sport/fussball/bundesliga/bundesliga-startseite-52368768.bild.html
                //https://www.bild.de/sport/fussball/2-bundesliga/2-liga-startseite-52368772.bild.html
                case "1.FC Heidenheim 1846": return "Heidenheim";
                case "1.FC Kaiserslautern": return "Kaiserslautern";
                case "1.FC Köln": return "Köln";
                case "1.FC Nürnberg": return "Nürnberg";
                case "1.FC Union Berlin": return "Union Berlin";
                case "1.FSV Mainz 05": return "Mainz 05";
                case "Arminia Bielefeld": return "Bielefeld";
                case "Bayer Leverkusen": return "Leverkusen";
                case "Bor.Mönchengladbach": return "Mönchengladbach";
                case "Borussia Dortmund": return "Dortmund";
                case "Dynamo Dresden": return "Dresden";
                case "Eintracht Braunschweig": return "Braunschweig";
                case "Eintracht Frankfurt": return "Frankfurt";
                case "Erzgebirge Aue": return "Aue";
                case "FC Augsburg": return "Augsburg";
                case "FC Bayern München": return "Bayern";
                case "FC Ingolstadt 04": return "Ingolstadt";
                case "FC Schalke 04": return "Schalke";
                case "FC St. Pauli": return "St. Pauli";
                case "Fortuna Düsseldorf": return "Düsseldorf";
                case "Hamburger SV": return "Hamburg";
                case "Hannover 96": return "Hannover";
                case "Hertha BSC": return "Hertha";
                case "Holstein Kiel": return "Holstein Kiel";
                case "Jahn Regensburg": return "Regensburg";
                case "MSV Duisburg": return "Duisburg";
                case "Red Bull Leipzig": return "RB Leipzig";
                case "SC Freiburg": return "Freiburg";
                case "SpVgg Greuther Fürth": return "Gr. Fürth";
                case "SV Darmstadt 98": return "Darmstadt";
                case "SV Sandhausen": return "Sandhausen";
                case "TSG 1899 Hoffenheim": return "Hoffenheim";
                case "VfB Stuttgart": return "Stuttgart";
                case "VfL Bochum": return "Bochum";
                case "VfL Wolfsburg": return "Wolfsburg";
                case "Werder Bremen": return "Werder";
                #endregion

                #region Netherlands 
                //https://www.bndestem.nl/voetbalcenter/klassement/eredivisie/
                case "ADO Den Haag": return "ADO Den Haag";
                case "Ajax Amsterdam": return "Ajax";
                case "AZ Alkmaar": return "AZ";
                case "Excelsior": return "Excelsior";
                case "FC Groningen": return "FC Groningen";
                case "FC Twente Enschede": return "FC Twente";
                case "FC Utrecht": return "FC Utrecht";
                case "Feyenoord Rotterdam": return "Feyenoord";
                case "Heracles Almelo": return "Heracles Almelo";
                case "NAC Breda": return "NAC Breda";
                case "PEC Zwolle": return "PEC Zwolle";
                case "PSV Eindhoven": return "PSV";
                case "Roda JC Kerkrade": return "Roda JC Kerkrade";
                case "SC Heerenveen": return "sc Heerenveen";
                case "Sparta Rotterdam": return "Sparta Rotterdam";
                case "Vitesse Arnhem": return "Vitesse";
                case "VVV Venlo": return "VVV-Venlo";
                case "Willem II Tilburg": return "Willem II";
                #endregion

                #region Portugal 
                //https://www.dn.pt/desporto.html
                case "Boavista Porto FC": return "Boavista";
                case "C.F.Os Belenenses": return "Belenenses";
                case "CD Tondela": return "Tondela";
                case "Desportivo Aves": return "Aves";
                case "FC Paços de Ferreira": return "Paços de Ferreira";
                case "FC Porto": return "FC Porto";
                case "FC Rio Ave": return "Rio Ave";
                case "Feirense": return "Feirense";
                case "G.D.Chaves": return "Chaves";
                case "GD Estoril Praia": return "Estoril";
                case "Maritimo Funchal": return "Maritimo";
                case "Moreirense FC": return "Moreirense";
                case "Portimonense S.C.": return "Portimonense";
                case "SL Benfica": return "Benfica";
                case "Sporting Braga": return "Braga";
                case "Sporting CP": return "Sporting";
                case "Vitoria Guimaraes": return "V. Guimarães";
                case "Vitoria Setubal": return "V. Setúbal";
                #endregion

                #region Spain
                //https://resultados.elpais.com/deportivos/futbol/primera/clasificacion/
                case "Athletic Club": return "Athletic";
                case "CD Leganes": return "Leganés";
                case "Club Atlético de Madrid": return "Atlético";
                case "Deportivo Alavés": return "Alavés";
                case "FC Barcelona": return "Barcelona";
                case "Girona FC": return "Girona";
                case "Levante UD": return "Levante";
                case "Málaga CF": return "Málaga";
                case "RC Celta de Vigo": return "Celta";
                case "RC Deportivo La Coruna": return "Deportivo";
                case "RCD Espanyol": return "Espanyol";
                case "Real Betis": return "Betis";
                case "Real Madrid CF": return "Real Madrid";
                case "Real Sociedad de Fútbol": return "R. Sociedad";
                case "SD Eibar": return "Eibar";
                case "Sevilla FC": return "Sevilla";
                case "UD Las Palmas": return "Las Palmas";
                case "Valencia CF": return "Valencia";
                case "Villarreal CF": return "Villarreal";
                #endregion

                default:
                    return externalTeamName;
            }
        }
    }
}
