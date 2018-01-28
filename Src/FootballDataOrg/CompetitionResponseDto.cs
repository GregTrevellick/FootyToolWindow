using System.Collections.Generic;
using FootballDataOrg.ResponseEntities;

namespace FootballDataOrg
{
    public class CompetitionResponseDto
    {
        public IEnumerable<CompetitionResponse> competitions { get; set; }
        public string error { get; set; }
    }
}
