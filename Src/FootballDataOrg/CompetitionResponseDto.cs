using System.Collections.Generic;

namespace FootballDataOrg.ResponseEntities
{
    public class CompetitionResponseDto
    {
        public IEnumerable<CompetitionResponse> competitions { get; set; }
        public string error { get; set; }
    }
}
