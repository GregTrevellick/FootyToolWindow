using System;
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

        private LeagueResponse GetLeagueResponse()
        {
            LeagueResponse league2 = new LeagueResponse("Serie a")
            {
                ActualDataStandings = new List<Standing>() {
                    new Standing() {Team = "roma", Rank = 3, Points = 30},
                    new Standing() {Team = "inter", Rank = 3, Points = 30},
                    new Standing() {Team = "naples", Rank = 3, Points = 30},
                },
                ActualDataResults = new List<Fixture>
                {
                    new Fixture{AwayTeamName="naples", HomeTeamName="roma", Date = new DateTime(2017,02,03),Result=new Result{goalsAwayTeam=3,goalsHomeTeam=3}},
                    new Fixture{AwayTeamName="inter", HomeTeamName="naples", Date = new DateTime(2017,02,03),Result=new Result{goalsAwayTeam=2,goalsHomeTeam=2}},                },
                ActualDataFixtures = new List<Fixture>
                {
                    new Fixture{AwayTeamName="inter", HomeTeamName="naples", Date = new DateTime(2017,02,04),Result=new Result{goalsAwayTeam=1,goalsHomeTeam=1}},
                    new Fixture{AwayTeamName="naples", HomeTeamName="roma", Date = new DateTime(2017,02,05),Result=new Result{goalsAwayTeam=5,goalsHomeTeam=5}},                },
            };
            return league2;
        }
        
    }
}
