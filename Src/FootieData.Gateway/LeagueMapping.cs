/////////////////////////////using FootieData.Gateway;
////using FootieData.Gateway;
//using System.Collections.Generic;
////using System.Linq;

//namespace FootieData.Entities.ReferenceData
//{
//    public static class LeagueMapping
//    {
//        public static IEnumerable<LeagueDto> GetLeagueDtos()//CompetitionResultSingleton competitionResultSingleton)
//        {
//            return new List<LeagueDto>
//            {
//                GetLeagueDto(InternalLeagueCode.BR1, ExternalLeagueCode.BSA, "Campeonato Brasileiro da Série A"),//, competitionResultSingleton),//, 444),
//                GetLeagueDto(InternalLeagueCode.DE1, ExternalLeagueCode.BL1, "Germany 1. Bundesliga"),//, competitionResultSingleton),//, 452),
//                GetLeagueDto(InternalLeagueCode.DE2, ExternalLeagueCode.BL2, "Germany 2. Bundesliga"),//, competitionResultSingleton),//, 453),
//                GetLeagueDto(InternalLeagueCode.ES1, ExternalLeagueCode.PD, "Spain Primera Division"),//, competitionResultSingleton),//, 455),
//                GetLeagueDto(InternalLeagueCode.FR1, ExternalLeagueCode.FL1, "France Ligue 1"),//, competitionResultSingleton),//, 450),
//                GetLeagueDto(InternalLeagueCode.FR2, ExternalLeagueCode.FL2, "France Ligue 2"),//, competitionResultSingleton),//, 451),
//                GetLeagueDto(InternalLeagueCode.IT1, ExternalLeagueCode.SA, "Italy Serie A"),//, competitionResultSingleton),//, 456),
//                GetLeagueDto(InternalLeagueCode.IT2, ExternalLeagueCode.SB, "Italy Serie B"),//, competitionResultSingleton),//, 459),
//                GetLeagueDto(InternalLeagueCode.NL1, ExternalLeagueCode.DED, "Netherlands Eredivisie"),//, competitionResultSingleton),//, 449),
//                GetLeagueDto(InternalLeagueCode.PT1, ExternalLeagueCode.PPL, "Portugal Primeira Liga"),//, competitionResultSingleton),//, 457),
//                GetLeagueDto(InternalLeagueCode.UEFA1, ExternalLeagueCode.CL, "Europe Champions-League"),//, competitionResultSingleton),//, 464),
//                GetLeagueDto(InternalLeagueCode.UK1, ExternalLeagueCode.PL, "England Premiere League"),//, competitionResultSingleton),//, 445),
//                GetLeagueDto(InternalLeagueCode.UK2, ExternalLeagueCode.ELC, "England Championship"),//, competitionResultSingleton),//, 446),
//                GetLeagueDto(InternalLeagueCode.UK3, ExternalLeagueCode.EL1, "England League One"),//, competitionResultSingleton),//, 447),
//                GetLeagueDto(InternalLeagueCode.UK4, ExternalLeagueCode.EL2, "League Two 2017/18"),//, competitionResultSingleton),//, 448),
//            };
//        }

//        private static LeagueDto GetLeagueDto(InternalLeagueCode internalLeagueCode, ExternalLeagueCode externalLeagueCode, string externalLeagueCodeDescription)//, CompetitionResultSingleton competitionResultSingleton)
//        {
//            ////////////var gateway = new FootieDataGateway(_competitionResultSingletonInstance);
//            ////////////var clientLeagueId = gateway.GetIdSeason(externalLeagueCode);
//            //var clientLeagueId = competitionResultSingleton?.CompetitionResult?.competitions?.SingleOrDefault(x => x.League == externalLeagueCode.ToString()).Id;

//            return new LeagueDto
//            {
//                //ClientLeagueId = clientLeagueId.HasValue ? clientLeagueId.Value : int.MinValue,//gregt need to test int.MinValue here
//                InternalLeagueCode   = internalLeagueCode,
//                ExternalLeagueCode = externalLeagueCode,
//                ExternalLeagueCodeDescription = externalLeagueCodeDescription
//            };
//        }
//    }   
//}