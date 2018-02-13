using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using FootieData.Entities.ReferenceData;

namespace FootieData.Common
{
    public class WpfHelper
    {
        public static void RightAlignDataGridColumns(ObservableCollection<DataGridColumn> dataGridColumns, IEnumerable<int> indexes, Style rightAlignStyle)
        {
            foreach (var index in indexes)
            {
                dataGridColumns[index].CellStyle = rightAlignStyle;
            }
        }

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

        public string GetGridTypeString(string expanderName)
        {
            var underscorePosition = expanderName.IndexOf('_');
            var gridTypeString = expanderName.Substring(underscorePosition, expanderName.Length - underscorePosition).Replace("_", "");
            return gridTypeString;
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


        public static GridType GetGridType(WpfHelper wpfHelper, string expanderName)
        {
            var gridTypeString = wpfHelper.GetGridTypeString(expanderName);
            return (GridType)Enum.Parse(typeof(GridType), gridTypeString);
        }
    }
}