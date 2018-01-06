using System.Collections.Generic;

namespace FootyData.Entities
{
    public class LeagueResponse
    {
        public string leagueCaption { get; set; }
        public int matchday { get; set; }
        public IEnumerable<Standing> Standings;
        public IEnumerable<Fixture> PastFixtures;
        public IEnumerable<Fixture> NextFixtures;
    }

}
