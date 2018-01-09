using System.Collections.Generic;

namespace HierarchicalDataTemplate
{
    public class LeagueResponse
    {
        public LeagueResponse()
        {
               
        }

        public LeagueResponse(string name)
        {
            LeagueName = name;
            Standings = new List<Standing>();
            Results = new List<Fixture>();
            Fixtures = new List<Fixture>();
        }

        public string LeagueName { get; }
        public IEnumerable<Standing> Standings { get; set; }
        public IEnumerable<Fixture> Results { get; set; }
        public IEnumerable<Fixture> Fixtures { get; set; }

    }
}