using System;
using System.Collections.Generic;
using System.Linq;
using FootieData.Entities;
using FootieData.Gateway;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HierarchicalDataTemplate
{
    public partial class MainWindow : Window
    {
        private readonly FootballDataSdkGateway _gateway;

        //public List<InternalLeagueCode> leaguesToShow = new List<InternalLeagueCode>
        //{
        //    InternalLeagueCode.UK1,
        //    InternalLeagueCode.DE1,
        //    InternalLeagueCode.DE2,
        //};

        //public List<GridToExpand> gridToExpands = new List<GridToExpand>
        //{
        //    new GridToExpand{internalLeagueCode = InternalLeagueCode.UK1, gridType = GridType.Standing},
        //    new GridToExpand{internalLeagueCode = InternalLeagueCode.UK1, gridType = GridType.Result},
        //    new GridToExpand{internalLeagueCode = InternalLeagueCode.UK1, gridType = GridType.Fixture},
        //    new GridToExpand{internalLeagueCode = InternalLeagueCode.DE1, gridType = GridType.Standing},
        //};

        public MainWindow()
        {
            InitializeComponent();
            _gateway = new FootballDataSdkGateway();
        }

        private void DataGridLoaded_Any(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;

            Expander parentExpander = grid.Parent as Expander;
            var internalLeagueCodeString = parentExpander.Name.Substring(0, 4).Replace("_", "");
            var internalLeagueCode = (InternalLeagueCode)Enum.Parse(typeof(InternalLeagueCode), internalLeagueCodeString);
            var shouldShowLeague = ShouldShowLeague(internalLeagueCode);

            if (shouldShowLeague)
            {
                parentExpander.Header = internalLeagueCode.GetDescription();

                var gridType = GetGridType(grid.Name);

                if (ShouldExpandGrid(shouldShowLeague, internalLeagueCode, gridType))
                {
                    var color = (Color)ColorConverter.ConvertFromString("Red");
                    grid.AlternatingRowBackground = new SolidColorBrush(color);
                    grid.ColumnHeaderHeight = 2;
                    grid.RowHeaderWidth = 2;
                    grid.CanUserAddRows = false;
                    grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
                    
                    if (Mappings.IntExt.TryGetValue(internalLeagueCode, out ExternalLeagueCode externalLeagueCode))
                    {
                        GetLeagueData(grid, externalLeagueCode, gridType);
                        parentExpander.Visibility = Visibility.Visible;
                        parentExpander.IsExpanded = true;
                    }
                    else
                    {
                        //TODO ERROR
                    }
                }
                else
                {
                    parentExpander.IsExpanded = false;
                }
            }
            else
            {
                parentExpander.Visibility = Visibility.Collapsed;
            }
        }

        private bool ShouldShowLeague(InternalLeagueCode internalLeagueCode)
        {
            if (LeagueOptions.LeaguesToShow.Contains(internalLeagueCode))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private bool ShouldExpandGrid(bool showLeague, InternalLeagueCode internalLeagueCode, GridType gridType)
        {
            if (!showLeague)
            {
                return false;
            }

            if (ExpanderOptions.GridToExpands.Any(x => x.internalLeagueCode == internalLeagueCode && x.gridType == gridType))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static GridType GetGridType(string gridName)
        {
            GridType gridType = 0;

            if (gridName.StartsWith("Standing"))
            {
                gridType = GridType.Standing;
            }
            else
            {
                if (gridName.StartsWith("Result"))
                {
                    gridType = GridType.Result;
                }
                else
                {
                    if (gridName.StartsWith("Fixture"))
                    {
                        gridType = GridType.Fixture;
                    }
                }
            }

            return gridType;
        }

        private void GetLeagueData(DataGrid grid, ExternalLeagueCode externalLeagueCode, GridType gridType)
        {
            if (gridType == GridType.Standing)
            {
                var leagueResponse = _gateway.GetLeagueResponse_Standings(externalLeagueCode.ToString());
                grid.ItemsSource = leagueResponse.Standings;
            }

            if (gridType == GridType.Result || gridType == GridType.Fixture)
            {
                LeagueMatches leagueResponse = null;

                if (gridType == GridType.Result)
                {
                    leagueResponse = _gateway.GetLeagueResponse_Results(externalLeagueCode.ToString());
                }

                if (gridType == GridType.Fixture)
                {
                    leagueResponse = _gateway.GetLeagueResponse_Fixtures(externalLeagueCode.ToString());
                }

                grid.ItemsSource = leagueResponse?.MatchFixtures;
            }
        }

        private void Click_Handler1(object sender, RoutedEventArgs e)
        {
            StackPanelLeagueMode.Visibility = Visibility.Collapsed;
            StackPanelBossMode.Visibility = Visibility.Visible;
        }

        private void Click_Handler2(object sender, RoutedEventArgs e)
        {
            StackPanelLeagueMode.Visibility = Visibility.Visible;
            StackPanelBossMode.Visibility = Visibility.Collapsed;
        }
    }
}
