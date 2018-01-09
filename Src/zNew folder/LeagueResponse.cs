using System.Collections.Generic;

namespace HierarchicalDataTemplate
{
    public class LeagueResponse
    {
        public LeagueResponse(string name)
        {
            LeagueName = name;
            ActualDataStandings = new List<Standing>();
            ActualDataResults = new List<Fixture>();
            ActualDataFixtures = new List<Fixture>();
        }

        public string LeagueName { get; }
        public IEnumerable<Standing> ActualDataStandings { get; set; }
        public IEnumerable<Fixture> ActualDataResults { get; set; }
        public IEnumerable<Fixture> ActualDataFixtures { get; set; }

    }
}