using System.Collections.Generic;

namespace FootieData.Entities
{
    public class LeagueMatchesResults
    {
        public string LeagueCaption { get; set; }
        public IList<Fixture> MatchFixtures;
    }
}
