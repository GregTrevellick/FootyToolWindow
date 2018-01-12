using System.Collections.Generic;
using FootieData.Entities;
using FootieData.Gateway;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        //GERMANY
        //BL1 Germany 1. Bundesliga
        //BL2 Germany 2. Bundesliga
        //BL3 Germany 3. Bundesliga
        //DFB Germany Dfb-Cup

        //ENGLAND
        //PL England Premiere League
        //EL1 England League One
        //ELC England Championship
        //FAC England FA-Cup

        //ITALY
        //SA Italy Serie A
        //SB Italy Serie B

        //SPAIN
        //PD Spain Primera Division
        //SD Spain Segunda Division
        //CDR Spain Copa del Rey

        //FRANCE
        //FL1 France Ligue 1
        //FL2 France Ligue 2

        //OTHER
        //DED Netherlands Eredivisie
        //PPL Portugal Primeira Liga
        //GSL Greece Super League

        //EUROPE
        //CL Europe Champions-League
        //EL Europe UEFA-Cup
        //EC Europe European-Cup of Nations
           
        //FIFA
        //WC World World-Cup

        private void DataGridLoaded_Any(object sender, RoutedEventArgs e)
        {
            var leaguesToShow = new List<string>
            {
                "PL",
                "PD",
                "BL1",
                "BL2",
                "EL1",
                "FAC",
                "CL",
                "EL",
                "WC",
            };

            var leaguesToExpand = new List<string>
            {
                //"PL",
                "PD",
                //"BL1",
                //"BL2",
                //"EL1",
                //"FAC",
                //"CL",
                //"EL",
                //"WC",
            };

            var grid = sender as DataGrid;

            var str = grid.Name;
            var leagueIdentifier = str.Remove(str.Length - 1, 1); //everything except the last char

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

                    var lastChar = str.Substring(str.Length - 1);
                    var srf = GetSrf(lastChar);

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

        private static Srf GetSrf(string lastChar)
        {
            Srf srf;
            switch (lastChar)
            {
                case "s":
                    srf = Srf.Standings;
                    break;
                case "r":
                    srf = Srf.Results;
                    break;
                case "f":
                    srf = Srf.Fixtures;
                    break;
                default:
                    srf = 0;
                    break;
            }

            return srf;
        }

        private void GetLeagueData(DataGrid grid, string leagueIdentifier, Srf srf)
        {
            grid.GridLinesVisibility = DataGridGridLinesVisibility.None;

            if (srf == Srf.Standings)
            {
                var leagueResponse = _gateway.GetLeagueResponse_Standings(leagueIdentifier);
                _leagueCaption = leagueResponse.LeagueCaption;
                grid.ItemsSource = leagueResponse.Standings;
            }

            if (srf == Srf.Results || srf == Srf.Fixtures)
            {
                LeagueMatches leagueResponse = null;

                if (srf == Srf.Results)
                {
                    leagueResponse = _gateway.GetLeagueResponse_Results(leagueIdentifier);
                }

                if (srf == Srf.Fixtures)
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
    }
}
