namespace FootieData.Entities.ReferenceData
{
    public class LeagueDto
    {
        public ExternalLeagueCode ExternalLeagueCode { get; set; }
        public string ExternalLeagueCodeDescription { get; set; }
        public InternalLeagueCode InternalLeagueCode { get; set; }
    }
}