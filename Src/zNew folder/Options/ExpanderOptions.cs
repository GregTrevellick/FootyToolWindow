using System.Collections.Generic;

namespace HierarchicalDataTemplate
{
    public static class ExpanderOptions
    {
        public static List<GridToExpand> GridToExpands = new List<GridToExpand>
        {
            //new GridToExpand{internalLeagueCode = InternalLeagueCode.UK1, gridType = GridType.Standing},
            //new GridToExpand{internalLeagueCode = InternalLeagueCode.UK1, gridType = GridType.Result},
            //new GridToExpand{internalLeagueCode = InternalLeagueCode.UK1, gridType = GridType.Fixture},
            new GridToExpand{internalLeagueCode = InternalLeagueCode.DE1, gridType = GridType.Standing},
            new GridToExpand{internalLeagueCode = InternalLeagueCode.DE1, gridType = GridType.Result},
        };

    }
}