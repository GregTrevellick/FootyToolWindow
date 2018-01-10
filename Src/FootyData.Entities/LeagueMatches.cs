using System.Collections.Generic;

namespace FootieData.Entities
{
    public class LeagueMatches
    {
        public string LeagueCaption { get; set; }
        public IList<Fixture> MatchFixtures;
    }
}
