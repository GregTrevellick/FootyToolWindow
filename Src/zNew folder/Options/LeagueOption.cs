using System.Collections.Generic;
using FootieData.Entities.ReferenceData;

namespace HierarchicalDataTemplate
{
    public class LeagueOption
    {
        public InternalLeagueCode InternalLeagueCode;
        public bool ShowLeague;
        public List<LeagueSubOption> LeagueSubOptions;
    }
}