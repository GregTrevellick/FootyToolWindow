using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HierarchicalDataTemplate
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DataGridLoaded_Standings(object sender, RoutedEventArgs e)
        {
            var leagueResponse = GetLeagueResponse();

            var grid = sender as DataGrid;
            grid.ItemsSource = leagueResponse.ActualDataStandings;
            grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
        }

        private void DataGridLoaded_Results(object sender, RoutedEventArgs e)
        {
            var leagueResponse = GetLeagueResponse();

            var grid = sender as DataGrid;
            grid.ItemsSource = leagueResponse.ActualDataResults;
            grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
        }

        private void DataGridLoaded_Fixtures(object sender, RoutedEventArgs e)
        {
            var leagueResponse = GetLeagueResponse();

            var grid = sender as DataGrid;
            grid.ItemsSource = leagueResponse.ActualDataFixtures;
            grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
        }

        private League2 GetLeagueResponse()
        {
            League2 league2 = new League2("Serie a")
            {
                ActualDataStandings = new List<Stringer>() { new Stringer() { Aaa = "roma", Bbb = "naples", Ccc = "milan" } },
                ActualDataResults = new List<Stringer>() { new Stringer() { Aaa = "roma 1-0 naples", Bbb = "milan 3-2 naples" } },
                ActualDataFixtures = new List<Stringer>() { new Stringer() { Aaa = "dd/mm/yy team1 v team2", Bbb = "dd/mm/yy team1 v team2", Ccc = "dd/mm/yy team1 v team2" } },
            };
            return league2;
        }
        
    }
}
