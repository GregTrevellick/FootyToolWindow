using System.Collections.Generic;

namespace FootieData.Entities
{
    public class LeagueResponse
    {
        public string LeagueCaption { get; set; }
        public int MatchDay { get; set; }
        public IEnumerable<Standing> Standings;
        public IEnumerable<Fixture> PastFixtures;
        public IEnumerable<Fixture> NextFixtures;
    }

}
