using System.Collections.Generic;
using FootieData.Entities.ReferenceData;

namespace FootieData.Wpf.Options
{
    public class LeagueOption
    {
        public InternalLeagueCode InternalLeagueCode;
        public bool ShowLeague;
        public List<LeagueSubOption> LeagueSubOptions;
    }
}