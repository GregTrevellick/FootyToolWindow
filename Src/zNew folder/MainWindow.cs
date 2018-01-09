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

        private void DataGridLoaded_PLs(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;
            var leagueResponse = GetLeagueResponse(grid.Name);
            grid.ItemsSource = leagueResponse.Standings;
            grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
        }

        private void DataGridLoaded_PLr(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;
            var leagueResponse = GetLeagueResponse(grid.Name);
            grid.ItemsSource = leagueResponse.Results;
            grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
        }

        private void DataGridLoaded_PLf(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;
            var leagueResponse = GetLeagueResponse(grid.Name);
            grid.ItemsSource = leagueResponse.Fixtures;
            grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
        }

        private void DataGridLoaded_BL1s(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;
            var leagueResponse = GetLeagueResponse(grid.Name);
            grid.ItemsSource = leagueResponse.Standings;
            grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
        }

        private void DataGridLoaded_BL2s(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;
            var leagueResponse = GetLeagueResponse(grid.Name);
            grid.ItemsSource = leagueResponse.Standings;
            grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
        }
        //private void DataGridLoaded_Results(object sender, RoutedEventArgs e)
        //{
        //    var leagueResponse = GetLeagueResponse();

        //    var grid = sender as DataGrid;
        //    grid.ItemsSource = leagueResponse.Results;
        //    grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
        //}

        //private void DataGridLoaded_Fixtures(object sender, RoutedEventArgs e)
        //{
        //    var leagueResponse = GetLeagueResponse();

        //    var grid = sender as DataGrid;
        //    grid.ItemsSource = leagueResponse.Fixtures;
        //    grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
        //}

        private LeagueResponse GetLeagueResponse(string gridName)
        {
            LeagueResponse league2 = new LeagueResponse();

            switch (gridName)
            {
                case "PLs":
                case "PLr":
                case "PLf":
                    league2 = new LeagueResponse("Serie a")
                    {
                        Standings = new List<Standing> {
                            new Standing() {Team = "mancity", Rank = 1, Points = 30},
                            new Standing() {Team = "chelsea", Rank = 2, Points = 20},
                            new Standing() {Team = "manutd", Rank = 3, Points = 15},
                        },
                        Results = new List<Fixture>
                        {
                            new Fixture{AwayTeamName="united", HomeTeamName="city", Date = new DateTime(2017,02,03),goalsAwayTeam=3,goalsHomeTeam=3},
                            new Fixture{AwayTeamName="united", HomeTeamName="city", Date = new DateTime(2017,02,03),goalsAwayTeam=2,goalsHomeTeam=2},
                        },
                        Fixtures = new List<Fixture>
                        {
                            new Fixture{AwayTeamName="everton", HomeTeamName="everton", Date = new DateTime(2017,02,04)},
                            new Fixture{AwayTeamName="west ham", HomeTeamName="saints", Date = new DateTime(2017,02,05)},
                            new Fixture{AwayTeamName="everton", HomeTeamName="everton", Date = new DateTime(2017,02,04)},
                            new Fixture{AwayTeamName="west ham", HomeTeamName="saints", Date = new DateTime(2017,02,05)},
                        },
                    };
                    break;
                case "BL2s":
                    league2 = new LeagueResponse("Serie a")
                    {
                        Standings = new List<Standing> {
                            new Standing() {Team = "roma", Rank = 1, Points = 30},
                            new Standing() {Team = "inter", Rank = 2, Points = 20},
                            new Standing() {Team = "naples", Rank = 3, Points = 15},
                            new Standing() {Team = "naples", Rank = 3, Points = 15},
                            new Standing() {Team = "naples", Rank = 3, Points = 15},
                            new Standing() {Team = "naples", Rank = 3, Points = 15},
                        },
                        Results = new List<Fixture>
                        {
                            new Fixture{AwayTeamName="naples", HomeTeamName="roma", Date = new DateTime(2017,02,03),goalsAwayTeam=3,goalsHomeTeam=3},
                            new Fixture{AwayTeamName="inter", HomeTeamName="naples", Date = new DateTime(2017,02,03),goalsAwayTeam=2,goalsHomeTeam=2},
                        },
                        Fixtures = new List<Fixture>
                        {
                            new Fixture{AwayTeamName="inter", HomeTeamName="naples", Date = new DateTime(2017,02,04),goalsHomeTeam=1},
                            new Fixture{AwayTeamName="naples", HomeTeamName="roma", Date = new DateTime(2017,02,05),goalsAwayTeam=5,goalsHomeTeam=5},
                        },
                    };
                    break;
            }

            
            return league2;
        }
        
    }
}
