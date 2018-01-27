using System.Collections.Generic;

namespace FootieData.Entities.ReferenceData
{
    public static class AllLeagueCodes
    {
        public static readonly IEnumerable<OneMap> AllMappings =
            new List<OneMap>
            {
                GetOneMap(InternalLeagueCode.BR1,"Brasileiro da Série A",ExternalLeagueCode.BSA,"Campeonato Brasileiro da Série A",444),
                GetOneMap(InternalLeagueCode.DE1,"Bundesliga 1",ExternalLeagueCode.BL1,"Germany 1. Bundesliga",452),
                GetOneMap(InternalLeagueCode.DE2,"Bundesliga 2",ExternalLeagueCode.BL2,"Germany 2. Bundesliga",453),
                GetOneMap(InternalLeagueCode.ES1,"La Liga Primera Division",ExternalLeagueCode.PD,"Spain Primera Division",455),
                GetOneMap(InternalLeagueCode.FR1,"Ligue 1",ExternalLeagueCode.FL1,"France Ligue 1",450),
                GetOneMap(InternalLeagueCode.FR2,"Ligue 2",ExternalLeagueCode.FL2,"France Ligue 2",451),
                GetOneMap(InternalLeagueCode.IT1,"Serie A",ExternalLeagueCode.SA,"Italy Serie A",456),
                GetOneMap(InternalLeagueCode.IT2,"Serie B",ExternalLeagueCode.SB,"Italy Serie B",459),                
                GetOneMap(InternalLeagueCode.NL1,"Eredivisie",ExternalLeagueCode.DED,"Netherlands Eredivisie",449),
                GetOneMap(InternalLeagueCode.PT1,"Primeira Liga",ExternalLeagueCode.PPL,"Portugal Primeira Liga",457),
                GetOneMap(InternalLeagueCode.UEFA1,"UEFA Champions League",ExternalLeagueCode.CL,"Europe Champions-League",464),
                GetOneMap(InternalLeagueCode.UK1,"English Premiership",ExternalLeagueCode.PL,"England Premiere League",445),
                GetOneMap(InternalLeagueCode.UK2,"English Championship",ExternalLeagueCode.ELC,"England Championship",446),
                GetOneMap(InternalLeagueCode.UK3,"English League One Vanarama",ExternalLeagueCode.EL1,"England League One",447),
                GetOneMap(InternalLeagueCode.UK4,"English League Two",ExternalLeagueCode.EL2,"League Two 2017/18",448),
            };

        private static OneMap GetOneMap(
            InternalLeagueCode internalLeagueCode,
            string internalLeagueCodeDescription,
            ExternalLeagueCode externalLeagueCode,
            string externalLeagueCodeDescription,
            int clientLeagueId)
        {
            return new OneMap
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