using System.Collections.Generic;

namespace FootieData.Entities
{
    public class LeagueResponse_Matches
    {
        public string LeagueCaption { get; set; }
        public IList<Fixture> MatchFixtures;
    }
}
