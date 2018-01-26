using System.Collections.Generic;

namespace FootieData.Entities.ReferenceData
{
    public class OneMap
    {
        public int ClientLeagueId { get; set; }
        public ExternalLeagueCode ExternalLeagueCode { get; set; }
        public string ExternalLeagueCodeDescription { get; set; }
        public InternalLeagueCode InternalLeagueCode { get; set; }
        public string InternalLeagueCodeDescription { get; set; }
    }

    public static class AllLeagueCodes
    {
        public static readonly IEnumerable<OneMap> AllMappings =
            new List<OneMap>
            {
                new OneMap
                {
                    ClientLeagueId = 466,
                    InternalLeagueCode = InternalLeagueCode.AU1,
                    InternalLeagueCodeDescription = "Australian A-League",
                    ExternalLeagueCode = ExternalLeagueCode.AAL,
                    ExternalLeagueCodeDescription = "Australian A-League"
                },
                new OneMap
                {
                    ClientLeagueId = 452,
                    InternalLeagueCode = InternalLeagueCode.DE1,
                    InternalLeagueCodeDescription = "Bundesliga 1",
                    ExternalLeagueCode = ExternalLeagueCode.BL1,
                    ExternalLeagueCodeDescription = "Germany 1. Bundesliga"
                },
                new OneMap
                {
                    ClientLeagueId = 453,
                    InternalLeagueCode = InternalLeagueCode.DE2,
                    InternalLeagueCodeDescription = "Bundesliga 2",
                    ExternalLeagueCode = ExternalLeagueCode.BL2,
                    ExternalLeagueCodeDescription = "Germany 2. Bundesliga"
                },
                new OneMap
                {
                    ClientLeagueId = 444,
                    InternalLeagueCode = InternalLeagueCode.BR1,
                    InternalLeagueCodeDescription = "Brasileiro da Série A",
                    ExternalLeagueCode = ExternalLeagueCode.BSA,
                    ExternalLeagueCodeDescription = "Campeonato Brasileiro da Série A"
                },
                new OneMap
                {
                    ClientLeagueId = 464,
                    InternalLeagueCode = InternalLeagueCode.UEFA1,
                    InternalLeagueCodeDescription = "UEFA Champions League",
                    ExternalLeagueCode = ExternalLeagueCode.CL,
                    ExternalLeagueCodeDescription = "Europe Champions-League"
                },
                new OneMap
                {
                    ClientLeagueId = 449,
                    InternalLeagueCode = InternalLeagueCode.NL1,
                    InternalLeagueCodeDescription = "Eredivisie",
                    ExternalLeagueCode = ExternalLeagueCode.DED,
                    ExternalLeagueCodeDescription = "Netherlands Eredivisie"
                },
                new OneMap
                {
                    ClientLeagueId = 447,
                    InternalLeagueCode = InternalLeagueCode.UK2,
                    InternalLeagueCodeDescription = "English League One",
                    ExternalLeagueCode = ExternalLeagueCode.EL1,
                    ExternalLeagueCodeDescription = "England League One"
                },
                new OneMap
                {
                    ClientLeagueId = 448,
                    InternalLeagueCode = InternalLeagueCode.UK4,
                    InternalLeagueCodeDescription = "English League Two",
                    ExternalLeagueCode = ExternalLeagueCode.EL2,
                    ExternalLeagueCodeDescription = "League Two 2017/18"
                },
                new OneMap
                {
                    ClientLeagueId = 446,
                    InternalLeagueCode = InternalLeagueCode.UK3,
                    InternalLeagueCodeDescription = "English Championship",
                    ExternalLeagueCode = ExternalLeagueCode.ELC,
                    ExternalLeagueCodeDescription = "England Championship"
                },
                new OneMap
                {
                    ClientLeagueId = 450,
                    InternalLeagueCode = InternalLeagueCode.FR1,
                    InternalLeagueCodeDescription = "Ligue 1",
                    ExternalLeagueCode = ExternalLeagueCode.FL1,
                    ExternalLeagueCodeDescription = "France Ligue 1"
                },
                new OneMap
                {
                    ClientLeagueId = 451,
                    InternalLeagueCode = InternalLeagueCode.FR2,
                    InternalLeagueCodeDescription = "Ligue 2",
                    ExternalLeagueCode = ExternalLeagueCode.FL2,
                    ExternalLeagueCodeDescription = "France Ligue 2"
                },
                new OneMap
                {
                    ClientLeagueId = 455,
                    InternalLeagueCode = InternalLeagueCode.ES1,
                    InternalLeagueCodeDescription = "La Liga Primera Division",
                    ExternalLeagueCode = ExternalLeagueCode.PD,
                    ExternalLeagueCodeDescription = "Spain Primera Division"
                },
                new OneMap
                {
                    ClientLeagueId = 445,
                    InternalLeagueCode = InternalLeagueCode.UK1,
                    InternalLeagueCodeDescription = "English Premiership",
                    ExternalLeagueCode = ExternalLeagueCode.PL,
                    ExternalLeagueCodeDescription = "England Premiere League"
                },
                new OneMap
                {
                    ClientLeagueId = 457,
                    InternalLeagueCode = InternalLeagueCode.PT1,
                    InternalLeagueCodeDescription = "Primeira Liga",
                    ExternalLeagueCode = ExternalLeagueCode.PPL,
                    ExternalLeagueCodeDescription = "Portugal Primeira Liga"
                },
                new OneMap
                {
                    ClientLeagueId = 456,
                    InternalLeagueCode = InternalLeagueCode.IT1,
                    InternalLeagueCodeDescription = "Serie A",
                    ExternalLeagueCode = ExternalLeagueCode.SA,
                    ExternalLeagueCodeDescription = "Italy Serie A"
                },
                new OneMap
                {
                    ClientLeagueId = 459,
                    InternalLeagueCode = InternalLeagueCode.IT2,
                    InternalLeagueCodeDescription = "Serie B",
                    ExternalLeagueCode = ExternalLeagueCode.SB,
                    ExternalLeagueCodeDescription = "Italy Serie B"
                },                
            };
    }


    //public static class AllLeagueCodes
    //{
    //    public static readonly IDictionary<InternalLeagueCode, ExternalLeagueCode> AllMappings =
    //        new Dictionary<InternalLeagueCode, ExternalLeagueCode>
    //        {
    //            {InternalLeagueCode.AU1, ExternalLeagueCode.AAL},
    //            {InternalLeagueCode.DE1, ExternalLeagueCode.BL1},
    //            {InternalLeagueCode, ExternalLeagueCode.BL2},
    //            {InternalLeagueCode.BR1, ExternalLeagueCode.BSA},
    //            {InternalLeagueCode.UEFA1, ExternalLeagueCode.CL},
    //            {InternalLeagueCode.NL1, ExternalLeagueCode.DED},
    //            {InternalLeagueCode.DE3, ExternalLeagueCode.DFB},
    //            {InternalLeagueCode.UK2, ExternalLeagueCode.EL1},
    //            {InternalLeagueCode.UK4, ExternalLeagueCode.EL2},
    //            {InternalLeagueCode.UK3, ExternalLeagueCode.ELC},
    //            {InternalLeagueCode.FR1, ExternalLeagueCode.FL1},
    //            {InternalLeagueCode.FR2, ExternalLeagueCode.FL2},
    //            {InternalLeagueCode.ES1, ExternalLeagueCode.PD},
    //            {InternalLeagueCode.UK1, ExternalLeagueCode.PL},
    //            {InternalLeagueCode.PT1, ExternalLeagueCode.PPL},
    //            {InternalLeagueCode.IT1, ExternalLeagueCode.SA},
    //            {InternalLeagueCode.IT2, ExternalLeagueCode.SB},
    //        };
    //}
}