using System.Collections.Generic;

namespace FootieData.Entities.ReferenceData
{
    public static class LeagueMapping
    {
        public static readonly IEnumerable<LeagueDto> LeagueDtos =
            new List<LeagueDto>
            {
                GetLeagueDto(InternalLeagueCode.BR1,"Brasileiro da Série A",ExternalLeagueCode.BSA,"Campeonato Brasileiro da Série A",444),
                GetLeagueDto(InternalLeagueCode.DE1,"Bundesliga 1",ExternalLeagueCode.BL1,"Germany 1. Bundesliga",452),
                GetLeagueDto(InternalLeagueCode.DE2,"Bundesliga 2",ExternalLeagueCode.BL2,"Germany 2. Bundesliga",453),
                GetLeagueDto(InternalLeagueCode.ES1,"La Liga Primera Division",ExternalLeagueCode.PD,"Spain Primera Division",455),
                GetLeagueDto(InternalLeagueCode.FR1,"Ligue 1",ExternalLeagueCode.FL1,"France Ligue 1",450),
                GetLeagueDto(InternalLeagueCode.FR2,"Ligue 2",ExternalLeagueCode.FL2,"France Ligue 2",451),
                GetLeagueDto(InternalLeagueCode.IT1,"Serie A",ExternalLeagueCode.SA,"Italy Serie A",456),
                GetLeagueDto(InternalLeagueCode.IT2,"Serie B",ExternalLeagueCode.SB,"Italy Serie B",459),                
                GetLeagueDto(InternalLeagueCode.NL1,"Eredivisie",ExternalLeagueCode.DED,"Netherlands Eredivisie",449),
                GetLeagueDto(InternalLeagueCode.PT1,"Primeira Liga",ExternalLeagueCode.PPL,"Portugal Primeira Liga",457),
                GetLeagueDto(InternalLeagueCode.UEFA1,"UEFA Champions League",ExternalLeagueCode.CL,"Europe Champions-League",464),
                GetLeagueDto(InternalLeagueCode.UK1,"English Premiership",ExternalLeagueCode.PL,"England Premiere League",445),
                GetLeagueDto(InternalLeagueCode.UK2,"English Championship",ExternalLeagueCode.ELC,"England Championship",446),
                GetLeagueDto(InternalLeagueCode.UK3,"English League One Vanarama",ExternalLeagueCode.EL1,"England League One",447),
                GetLeagueDto(InternalLeagueCode.UK4,"English League Two",ExternalLeagueCode.EL2,"League Two 2017/18",448),
            };

        private static LeagueDto GetLeagueDto(
            InternalLeagueCode internalLeagueCode,
            string internalLeagueCodeDescription,
            ExternalLeagueCode externalLeagueCode,
            string externalLeagueCodeDescription,
            int clientLeagueId)
        {
            return new LeagueDto
            {
                ClientLeagueId = clientLeagueId,
                InternalLeagueCode   = internalLeagueCode,
                InternalLeagueCodeDescription = internalLeagueCodeDescription,
                ExternalLeagueCode = externalLeagueCode,
                ExternalLeagueCodeDescription = externalLeagueCodeDescription
            };
        }
    }   
}