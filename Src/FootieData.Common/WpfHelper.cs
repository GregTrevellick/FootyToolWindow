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
            if (dataGridColumns.Count > 0)
            {
                foreach (var index in indexes)
                {
                    dataGridColumns[index].CellStyle = rightAlignStyle;
                }
            }
        }

        public static string GetHeaderPrefix(InternalLeagueCode internalLeagueCode)
        {
            switch (internalLeagueCode)
            {
                case InternalLeagueCode.BR1:
                    return CommonConstants.InternalLeagueCodeDescriptionBr1;
                case InternalLeagueCode.DE1:
                    return CommonConstants.InternalLeagueCodeDescriptionDe1;
                case InternalLeagueCode.DE2:
                    return CommonConstants.InternalLeagueCodeDescriptionDe2;
                case InternalLeagueCode.ES1:
                    return CommonConstants.InternalLeagueCodeDescriptionEs1;
                case InternalLeagueCode.FR1:
                    return CommonConstants.InternalLeagueCodeDescriptionFr1;
                case InternalLeagueCode.FR2:
                    return CommonConstants.InternalLeagueCodeDescriptionFr2;
                case InternalLeagueCode.IT1:
                    return CommonConstants.InternalLeagueCodeDescriptionIt1;
                case InternalLeagueCode.IT2:
                    return CommonConstants.InternalLeagueCodeDescriptionIt2;
                case InternalLeagueCode.NL1:
                    return CommonConstants.InternalLeagueCodeDescriptionNl1;
                case InternalLeagueCode.PT1:
                    return CommonConstants.InternalLeagueCodeDescriptionPt1;
                case InternalLeagueCode.UEFA1:
                    return CommonConstants.InternalLeagueCodeDescriptionUefa1;
                case InternalLeagueCode.UK1:
                    return CommonConstants.InternalLeagueCodeDescriptionUk1;
                case InternalLeagueCode.UK2:
                    return CommonConstants.InternalLeagueCodeDescriptionUk2;
                case InternalLeagueCode.UK3:
                    return CommonConstants.InternalLeagueCodeDescriptionUk3;
                case InternalLeagueCode.UK4:
                    return CommonConstants.InternalLeagueCodeDescriptionUk4;
                default:
                    return $"League {internalLeagueCode}";
            }
        }

        public static string GetHeaderSuffix(GridType gridType)
        {
            string headerSuffix;
            switch (gridType)
            {
                case GridType.Result:
                    headerSuffix = $"Results (last {CommonConstants.DaysCount} days)";
                    break;
                case GridType.Fixture:
                    headerSuffix = $"Fixtures (next {CommonConstants.DaysCount} days)";
                    break;
                default:
                    headerSuffix = string.Empty;
                    break;
            }

            return headerSuffix;
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
                LeagueSubOptions = GetLeagueSubOptions(internalLeagueCode)
            };
        }

        private static List<LeagueSubOption> GetLeagueSubOptions(InternalLeagueCode internalLeagueCode)
        {
            switch (internalLeagueCode)
            {
                ////////////////////////case InternalLeagueCode.BR1:
                ////////////////////////    //Brazil - league table only seems to work
                ////////////////////////    return new List<LeagueSubOption>
                ////////////////////////    {
                ////////////////////////        new LeagueSubOption
                ////////////////////////        {
                ////////////////////////            GridType = GridType.Standing,
                ////////////////////////            Expand = true
                ////////////////////////        }
                ////////////////////////    };
                case InternalLeagueCode.UEFA1:
                    //Champions league - league table unsurprisingly doesn't work, but results/fixtures do (in knock-out rounds at least)
                    return new List<LeagueSubOption>
                    {
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
                default:
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
}