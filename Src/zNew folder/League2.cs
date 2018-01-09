using System.Collections.Generic;

namespace HierarchicalDataTemplate
{
    public class League2
    {
        public League2(string name)
        {
            LeagueName = name;
            ActualDataStandings = new List<Stringer>();
            ActualDataResults = new List<Stringer>();
            ActualDataFixtures = new List<Stringer>();
        }

        public string LeagueName { get; }
        public IEnumerable<Stringer> ActualDataStandings { get; set; }
        public IEnumerable<Stringer> ActualDataResults { get; set; }
        public IEnumerable<Stringer> ActualDataFixtures { get; set; }

    }
}