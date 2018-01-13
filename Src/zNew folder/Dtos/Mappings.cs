using System.Collections.Generic;

namespace HierarchicalDataTemplate
{
    public static class Mappings
    {
        public static Dictionary<InternalLeagueCode, ExternalLeagueCode> IntExt =
            new Dictionary<InternalLeagueCode, ExternalLeagueCode>
            {
                {InternalLeagueCode.DE1, ExternalLeagueCode.BL1},
                {InternalLeagueCode.DE2, ExternalLeagueCode.BL2},
                {InternalLeagueCode.DE3, ExternalLeagueCode.BL3},
                {InternalLeagueCode.DE4, ExternalLeagueCode.DFB},
                {InternalLeagueCode.UK1, ExternalLeagueCode.PL},
                {InternalLeagueCode.UK2, ExternalLeagueCode.EL1},
                {InternalLeagueCode.UK3, ExternalLeagueCode.ELC},
                {InternalLeagueCode.UK4, ExternalLeagueCode.FAC},
                {InternalLeagueCode.IT1, ExternalLeagueCode.SA},
                {InternalLeagueCode.IT2, ExternalLeagueCode.SB},
                {InternalLeagueCode.ES1, ExternalLeagueCode.PD},
                {InternalLeagueCode.ES2, ExternalLeagueCode.SD},
                {InternalLeagueCode.ES3, ExternalLeagueCode.CDR},
                {InternalLeagueCode.FR1, ExternalLeagueCode.FL1},
                {InternalLeagueCode.FR2, ExternalLeagueCode.FL2},
                {InternalLeagueCode.NL1, ExternalLeagueCode.DED},
                {InternalLeagueCode.PT1, ExternalLeagueCode.PPL},
                {InternalLeagueCode.GR1, ExternalLeagueCode.GSL},
                {InternalLeagueCode.UEFA1, ExternalLeagueCode.CL},
                {InternalLeagueCode.UEFA2, ExternalLeagueCode.EL},
                {InternalLeagueCode.UEFA3, ExternalLeagueCode.EC},
                {InternalLeagueCode.FIFA1, ExternalLeagueCode.WC},
            };
    }
}