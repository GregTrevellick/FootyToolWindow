using System.ComponentModel;

namespace HierarchicalDataTemplate
{

    public enum ExternalLeagueCode
    {
        [Description("Germany 1. Bundesliga")]
        BL1,
        [Description("Germany 2. Bundesliga")]
        BL2,
        [Description("Germany 3. Bundesliga")]
        BL3,
        [Description("Germany Dfb-Cup")]
        DFB,
        [Description("England Premiere League")]
        PL,
        [Description("England League One")]
        EL1,
        [Description("England Championship")]
        ELC,
        [Description("England FA-Cup")]
        FAC,
        [Description("Italy Serie A")]
        SA,
        [Description("Italy Serie B")]
        SB,
        [Description("Spain Primera Division")]
        PD,
        [Description("Spain Segunda Division")]
        SD,
        [Description("Spain Copa del Rey")]
        CDR,
        [Description("France Ligue 1")]
        FL1,
        [Description("France Ligue 2")]
        FL2,
        [Description("Netherlands Eredivisie")]
        DED,
        [Description("Portugal Primeira Liga")]
        PPL,
        [Description("Greece Super League")]
        GSL,
        [Description("Europe Champions-League")]
        CL,
        [Description("Europe UEFA-Cup")]
        EL,
        [Description("Europe European-Cup of Nations")]
        EC,
        [Description("World World-Cup")]
        WC,
    }
}