using System.Collections.Generic;
using HierarchicalDataTemplate.ReferenceData;

namespace HierarchicalDataTemplate.Options
{
    public class LeagueOption
    {
        public InternalLeagueCode InternalLeagueCode;
        public bool ShowLeague;
        public List<LeagueSubOption> LeagueSubOptions;
    }
}