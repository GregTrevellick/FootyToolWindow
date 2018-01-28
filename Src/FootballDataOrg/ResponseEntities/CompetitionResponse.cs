using System.Collections.Generic;

namespace FootballDataOrg.ResponseEntities
{
    public class CompetitionResponse
    {
        public IEnumerable<Competition> competitions { get; set; }
        public string error { get; set; }
    }
}
