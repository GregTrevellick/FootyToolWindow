using System.Collections.Generic;
using HierarchicalDataTemplate.ReferenceData;

namespace HierarchicalDataTemplate
{
    public class LeagueOption
    {
        public InternalLeagueCode InternalLeagueCode;
        public bool ShowLeague;
        public List<LeagueSubOption> LeagueSubOptions;
    }
}