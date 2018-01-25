using System.ComponentModel;

namespace FootieData.Entities.ReferenceData
{
    public enum ExternalLeagueCode
    {
        //"league": "AAL",
        //"league": "BL1",
        //"league": "BL2",
        //"league": "BSA",
        //"league": "CL",
        //"league": "DED",
        //"league": "DFB",
        //"league": "EL1",
        //"league": "EL2",
        //"league": "ELC",
        //"league": "FL1",
        //"league": "FL2",
        //"league": "PD",
        //"league": "PL",
        //"league": "PPL",
        //"league": "SA",
        //"league": "SB",

        //gregt delete descriptions
        //gregt sort alpha

        [Description("Australian A-League")]
        AAL,

        [Description("Germany 1. Bundesliga")]
        BL1,

        [Description("Germany 2. Bundesliga")]
        BL2,

        [Description("Campeonato Brasileiro da Série A")]
        BSA,
        
        [Description("Europe Champions-League")]
        CL,

        [Description("Netherlands Eredivisie")]
        DED,

        [Description("DFB-Pokal 2017/18")]
        DFB,
        
        [Description("England League One")]
        EL1,

        [Description("League Two 2017/18")]
        EL2,      

        [Description("England Championship")]
        ELC,
      
        [Description("France Ligue 1")]
        FL1,

        [Description("France Ligue 2")]
        FL2,

        [Description("Spain Primera Division")]
        PD,

        [Description("England Premiere League")]
        PL,

        [Description("Portugal Primeira Liga")]
        PPL,

        [Description("Italy Serie A")]
        SA,

        [Description("Italy Serie B")]
        SB,        
    }
}