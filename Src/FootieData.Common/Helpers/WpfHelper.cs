using HierarchicalDataTemplate;

namespace FootieData.Common.Helpers
{
    public class WpfHelper
    {
        public string GetInternalLeagueCodeString(string expanderName)
        {
            var underscorePosition = expanderName.IndexOf('_');
            var internalLeagueCodeString = expanderName.Substring(0, underscorePosition).Replace("_", "");
            return internalLeagueCodeString;
        }

        public GridType GetGridType(string gridName)
        {
            GridType gridType = 0;

            if (gridName.StartsWith("Standing"))
            {
                gridType = GridType.Standing;
            }
            else
            {
                if (gridName.StartsWith("Results1"))
                {
                    gridType = GridType.Result;
                }
                else
                {
                    if (gridName.StartsWith("Fixtures"))
                    {
                        gridType = GridType.Fixture;
                    }
                }
            }

            return gridType;
        }


    }
}
