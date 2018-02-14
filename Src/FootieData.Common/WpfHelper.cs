using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using FootieData.Common.Options;
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

        private static string GetInternalLeagueCodeString(string expanderName)
        {
            var underscorePosition = expanderName.IndexOf('_');
            var internalLeagueCodeString = expanderName.Substring(0, underscorePosition).Replace("_", "");
            return internalLeagueCodeString;
        }

        private static string GetGridTypeString(string expanderName)
        {
            var underscorePosition = expanderName.IndexOf('_');
            var gridTypeString = expanderName.Substring(underscorePosition, expanderName.Length - underscorePosition).Replace("_", "");
            return gridTypeString;
        }

        public static InternalLeagueCode GetInternalLeagueCode(string expanderName)
        {
            var internalLeagueCodeString = GetInternalLeagueCodeString(expanderName);
            return (InternalLeagueCode)Enum.Parse(typeof(InternalLeagueCode), internalLeagueCodeString);
        }

        public static GridType GetGridType(string expanderName)
        {
            var gridTypeString = GetGridTypeString(expanderName);
            return (GridType)Enum.Parse(typeof(GridType), gridTypeString);
        }

        public static LeagueOption GetLeagueOption(bool interestedIn, InternalLeagueCode internalLeagueCode)
        {
            return new LeagueOption
            {
                InternalLeagueCode = internalLeagueCode,
                ShowLeague = interestedIn,
                LeagueSubOptions = WpfHelper.GetLeagueSubOptions()
            };
        }

        private static List<LeagueSubOption> GetLeagueSubOptions()
        {
            return new List<LeagueSubOption>
            {
                new LeagueSubOption
                {
                    GridType = GridType.Standing,
                    Expand = true
                },
                new LeagueSubOption
                {
                    GridType = GridType.Result,
                    Expand = false
                },
                new LeagueSubOption
                {
                    GridType = GridType.Fixture,
                    Expand = false
                }
            };
        }
    }
}