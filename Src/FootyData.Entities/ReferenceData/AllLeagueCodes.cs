using System.Collections.Generic;

namespace HierarchicalDataTemplate.ReferenceData
{
    public static class AllLeagueCodes
    {
        public static readonly IDictionary<InternalLeagueCode, ExternalLeagueCode> AllMappings =
            new Dictionary<InternalLeagueCode, ExternalLeagueCode>
            {
                {InternalLeagueCode.AU1, ExternalLeagueCode.AAL},
                {InternalLeagueCode.DE1, ExternalLeagueCode.BL1},
                {InternalLeagueCode.DE2, ExternalLeagueCode.BL2},
                {InternalLeagueCode.BR1, ExternalLeagueCode.BSA},
                {InternalLeagueCode.UEFA1, ExternalLeagueCode.CL},
                {InternalLeagueCode.NL1, ExternalLeagueCode.DED},
                {InternalLeagueCode.DE3, ExternalLeagueCode.DFB},
                {InternalLeagueCode.UK2, ExternalLeagueCode.EL1},
                {InternalLeagueCode.UK4, ExternalLeagueCode.EL2},
                {InternalLeagueCode.UK3, ExternalLeagueCode.ELC},
                {InternalLeagueCode.FR1, ExternalLeagueCode.FL1},
                {InternalLeagueCode.FR2, ExternalLeagueCode.FL2},
                {InternalLeagueCode.ES1, ExternalLeagueCode.PD},
                {InternalLeagueCode.UK1, ExternalLeagueCode.PL},
                {InternalLeagueCode.PT1, ExternalLeagueCode.PPL},
                {InternalLeagueCode.IT1, ExternalLeagueCode.SA},
                {InternalLeagueCode.IT2, ExternalLeagueCode.SB},
            };
    }
}