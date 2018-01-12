using System.Collections.Generic;
using FootieData.Entities;
using FootieData.Gateway;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace HierarchicalDataTemplate
{
    public partial class MainWindow : Window
    {
        private string _leagueCaption;
        private readonly FootballDataSdkGateway _gateway;

        public MainWindow()
        {
            InitializeComponent();
            _gateway = new FootballDataSdkGateway();
        }

        private void DataGridLoaded_Any(object sender, RoutedEventArgs e)
        {
            var leaguesToShow = new List<string>
            {
                "PL",
                "BL1",
                "BL2"
            };

            var leaguesToExpand = new List<string>
            {
                "PL",
                "BL1"
            };

            var grid = sender as DataGrid;

            var str = grid.Name;
            var leagueIdentifier = str.Remove(str.Length - 1, 1); //everything except the last char

            //////////////var parentExpander = VisualTreeHelper.GetParent(grid) as Expander;
            //////////////  Expander parentExpander = GetWindow((DependencyObject)grid) as Expander;
            //////////////UIElement parent = grid.Parent;
            Expander parentExpander = grid.Parent as Expander;

            if (leaguesToShow.Contains(leagueIdentifier))
            {
                Expander_PLs.Header = _leagueCaption;

                if (leaguesToExpand.Contains(leagueIdentifier))
                {
                    var color = (Color)ColorConverter.ConvertFromString("Red");
                    grid.AlternatingRowBackground = new SolidColorBrush(color);
                    grid.ColumnHeaderHeight = 2;
                    grid.RowHeaderWidth = 2;
                    grid.CanUserAddRows = false;

                    var srf = str.Substring(str.Length - 1);//last char of name

                    GetLeagueData(grid, leagueIdentifier, srf);
                    
                    parentExpander.Visibility = Visibility.Visible;
                    parentExpander.IsExpanded = true;
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

        private void GetLeagueData(DataGrid grid, string leagueIdentifier, string srf)
        {
            grid.GridLinesVisibility = DataGridGridLinesVisibility.None;

            if (srf == "s")
            {
                var leagueResponse = _gateway.GetLeagueResponse_Standings(leagueIdentifier);
                _leagueCaption = leagueResponse.LeagueCaption;
                grid.ItemsSource = leagueResponse.Standings;
            }

            if (srf == "r" || srf == "f")
            {
                LeagueMatches leagueResponse = null;

                if (srf == "r")
                {
                    leagueResponse = _gateway.GetLeagueResponse_Results(leagueIdentifier);
                }

                if (srf == "f")
                {
                    leagueResponse = _gateway.GetLeagueResponse_Fixtures(leagueIdentifier);
                }

                _leagueCaption = leagueResponse?.LeagueCaption;
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

        //public static T TryFindParent<T>(DependencyObject current) where T : class
        //{
        //    DependencyObject parent = VisualTreeHelper.GetParent(current);
        //    if (parent == null)
        //        parent = LogicalTreeHelper.GetParent(current);
        //    if (parent == null)
        //        return null;
        //    if (parent is T)
        //        return parent as T;
        //    else
        //        return TryFindParent<T>(parent);
        //}
    }
}
