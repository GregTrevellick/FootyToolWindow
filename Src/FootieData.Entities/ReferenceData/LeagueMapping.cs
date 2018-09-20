using System.Collections.Generic;

namespace FootieData.Entities.ReferenceData
{
    public static class LeagueMapping
    {
        public static readonly IEnumerable<LeagueDto> LeagueDtos =
            new List<LeagueDto>
            {
                //GetLeagueDto(InternalLeagueCode.DE2, ExternalLeagueCode.BL2, "Germany 2. Bundesliga"),//, 453),
                //GetLeagueDto(InternalLeagueCode.FR2, ExternalLeagueCode.FL2, "France Ligue 2"),//, 451),
                //GetLeagueDto(InternalLeagueCode.IT2, ExternalLeagueCode.SB, "Italy Serie B"),//, 459),
                //GetLeagueDto(InternalLeagueCode.UK3, ExternalLeagueCode.EL1, "England League One"),//, 447),
                //GetLeagueDto(InternalLeagueCode.UK4, ExternalLeagueCode.EL2, "League Two 2017/18"),//, 448),
                GetLeagueDto(InternalLeagueCode.BR1, ExternalLeagueCode.BSA, "Campeonato Brasileiro da Série A"),//, 444),
                GetLeagueDto(InternalLeagueCode.DE1, ExternalLeagueCode.BL1, "Germany 1. Bundesliga"),//, 452),
                GetLeagueDto(InternalLeagueCode.ES1, ExternalLeagueCode.PD, "Spain Primera Division"),//, 455),
                GetLeagueDto(InternalLeagueCode.FR1, ExternalLeagueCode.FL1, "France Ligue 1"),//, 450),
                GetLeagueDto(InternalLeagueCode.IT1, ExternalLeagueCode.SA, "Italy Serie A"),//, 456),
                GetLeagueDto(InternalLeagueCode.NL1, ExternalLeagueCode.DED, "Netherlands Eredivisie"),//, 449),
                GetLeagueDto(InternalLeagueCode.PT1, ExternalLeagueCode.PPL, "Portugal Primeira Liga"),//, 457),
                GetLeagueDto(InternalLeagueCode.UEFA1, ExternalLeagueCode.CL, "Europe Champions-League"),//, 464),
                GetLeagueDto(InternalLeagueCode.UK1, ExternalLeagueCode.PL, "England Premiere League"),//, 445),
                GetLeagueDto(InternalLeagueCode.UK2, ExternalLeagueCode.ELC, "England Championship"),//, 446),
            };

        private static LeagueDto GetLeagueDto(
            InternalLeagueCode internalLeagueCode,
            ExternalLeagueCode externalLeagueCode,
            string externalLeagueCodeDescription)
        {
            return new LeagueDto
            {
                InternalLeagueCode = internalLeagueCode,
                ExternalLeagueCode = externalLeagueCode,
                ExternalLeagueCodeDescription = externalLeagueCodeDescription
            };
        }
    }
}