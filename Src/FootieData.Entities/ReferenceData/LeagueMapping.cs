using System.Collections.Generic;

namespace FootieData.Entities.ReferenceData
{
    public static class LeagueMapping
    {
        public const string InternalLeagueCodeDescriptionUk1 = "English Premiership";
        public const string InternalLeagueCodeDescriptionBr1 = "Brasileiro da Série A";
        public const string InternalLeagueCodeDescriptionDe1 = "Bundesliga 1";
        public const string InternalLeagueCodeDescriptionDe2 = "Bundesliga 2";
        public const string InternalLeagueCodeDescriptionEs1 = "La Liga Primera Division";
        public const string InternalLeagueCodeDescriptionFr1 = "Ligue 1";
        public const string InternalLeagueCodeDescriptionFr2 = "Ligue 2";
        public const string InternalLeagueCodeDescriptionIt1= "Serie A";
        public const string InternalLeagueCodeDescriptionIt2 = "Serie B";
        public const string InternalLeagueCodeDescriptionNl1 = "Eredivisie";
        public const string InternalLeagueCodeDescriptionPt1 = "Primeira Liga";
        public const string InternalLeagueCodeDescriptionUefa1 = "UEFA Champions League";
        public const string InternalLeagueCodeDescriptionUk2 = "English Championship";
        public const string InternalLeagueCodeDescriptionUk3 = "English League One Vanarama";
        public const string InternalLeagueCodeDescriptionUk4 = "English League Two";

        public static readonly IEnumerable<LeagueDto> LeagueDtos =
            new List<LeagueDto>
            {
                GetLeagueDto(InternalLeagueCode.BR1,InternalLeagueCodeDescriptionUk1,ExternalLeagueCode.BSA,"Campeonato Brasileiro da Série A",444),
                GetLeagueDto(InternalLeagueCode.DE1,InternalLeagueCodeDescriptionDe1,ExternalLeagueCode.BL1,"Germany 1. Bundesliga",452),
                GetLeagueDto(InternalLeagueCode.DE2,InternalLeagueCodeDescriptionDe2,ExternalLeagueCode.BL2,"Germany 2. Bundesliga",453),
                GetLeagueDto(InternalLeagueCode.ES1,InternalLeagueCodeDescriptionEs1,ExternalLeagueCode.PD,"Spain Primera Division",455),
                GetLeagueDto(InternalLeagueCode.FR1,InternalLeagueCodeDescriptionFr1,ExternalLeagueCode.FL1,"France Ligue 1",450),
                GetLeagueDto(InternalLeagueCode.FR2,InternalLeagueCodeDescriptionFr2,ExternalLeagueCode.FL2,"France Ligue 2",451),
                GetLeagueDto(InternalLeagueCode.IT1,InternalLeagueCodeDescriptionIt1,ExternalLeagueCode.SA,"Italy Serie A",456),
                GetLeagueDto(InternalLeagueCode.IT2,InternalLeagueCodeDescriptionIt2,ExternalLeagueCode.SB,"Italy Serie B",459),                
                GetLeagueDto(InternalLeagueCode.NL1,InternalLeagueCodeDescriptionNl1,ExternalLeagueCode.DED,"Netherlands Eredivisie",449),
                GetLeagueDto(InternalLeagueCode.PT1,InternalLeagueCodeDescriptionPt1,ExternalLeagueCode.PPL,"Portugal Primeira Liga",457),
                GetLeagueDto(InternalLeagueCode.UEFA1,InternalLeagueCodeDescriptionUefa1,ExternalLeagueCode.CL,"Europe Champions-League",464),
                GetLeagueDto(InternalLeagueCode.UK1,InternalLeagueCodeDescriptionUk1,ExternalLeagueCode.PL,"England Premiere League",445),
                GetLeagueDto(InternalLeagueCode.UK2,InternalLeagueCodeDescriptionUk2,ExternalLeagueCode.ELC,"England Championship",446),
                GetLeagueDto(InternalLeagueCode.UK3,InternalLeagueCodeDescriptionUk3,ExternalLeagueCode.EL1,"England League One",447),
                GetLeagueDto(InternalLeagueCode.UK4,InternalLeagueCodeDescriptionUk4,ExternalLeagueCode.EL2,"League Two 2017/18",448),
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