//using System.Collections.Generic;

//namespace FootieData.Entities
//{
//    public class LeagueMatches
//    {
//        public string LeagueCaption { get; set; }
//        public IList<Fixture> MatchFixtures;
//    }
//}

using System.Collections.Generic;

namespace FootieData.Entities
{
    public class LeagueMatchesResults
    {
        public string LeagueCaption { get; set; }
        public IList<Fixture> MatchFixtures;
    }

    public class LeagueMatchesFixtures
    {
        public string LeagueCaption { get; set; }
        public IList<Fixture> MatchFixtures;
    }
}
