using FootieData.Entities.ReferenceData;
using HierarchicalDataTemplate;
using System;

namespace FootieData.Common.Helpers
{
    public class WpfHelper
    {
        public static string GetDescription(GridType gridType)
        {
            switch (gridType)
            {
                case GridType.Unknown:
                    return "Error000";
                case GridType.Standing:
                    return "";
                case GridType.Result:
                    return "results";
                case GridType.Fixture:
                    return "fixtures";
            }
            return "Error001";
        }

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

        public static InternalLeagueCode GetInternalLeagueCode(WpfHelper wpfHelper, string expanderName)
        {
            var internalLeagueCodeString = wpfHelper.GetInternalLeagueCodeString(expanderName);
            return (InternalLeagueCode)Enum.Parse(typeof(InternalLeagueCode), internalLeagueCodeString);
        }        
    }
}