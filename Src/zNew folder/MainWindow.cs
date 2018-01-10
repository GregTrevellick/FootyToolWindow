using FootieData.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using FootieData.Entities;

namespace HierarchicalDataTemplate
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DataGridLoaded_PLs(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;
            GetLeagueData(sender, grid.Name, "s");
            this.Expander_PLs.Header = "dgdgdgdgdgdgdgd";
        }

        private void DataGridLoaded_PLr(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;
            GetLeagueData(sender, "PL", "r");
        }

        private void DataGridLoaded_PLf(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;
            GetLeagueData(sender, "PL", "f");
        }

        private void DataGridLoaded_BL1s(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;
            GetLeagueData(sender, grid.Name, "s");
        }

        private void DataGridLoaded_BL2s(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;
            GetLeagueData(sender, grid.Name, "s");
        }

        private void GetLeagueData(object sender, string leagueIdentifier, string srf)
        {
            var _gateway = new FootballDataSdkGateway();

            var grid = sender as DataGrid;
            grid.GridLinesVisibility = DataGridGridLinesVisibility.None;

            if (srf == "s")
            {
                var leagueResponseS = _gateway.GetLeagueResponse_Standings(leagueIdentifier);
                grid.ItemsSource = leagueResponseS.Standings;
            }

            if (srf == "r")
            {
                var leagueResponseR = _gateway.GetLeagueResponse_Results(leagueIdentifier);
                grid.ItemsSource = leagueResponseR.MatchFixtures;
            }

            if (srf == "f")
            {
                var leagueResponseF = _gateway.GetLeagueResponse_Fixtures(leagueIdentifier);
                grid.ItemsSource = leagueResponseF.MatchFixtures;
            }
        }
    }
}
