using System.Collections.Generic;
using FootieData.Entities.ReferenceData;

namespace FootieData.Common.Options
{
    public class LeagueOption
    {
        public InternalLeagueCode InternalLeagueCode;
        public bool ShowLeague;
        public List<LeagueSubOption> LeagueSubOptions;
    }
}