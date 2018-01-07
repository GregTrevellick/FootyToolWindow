using FootieData.Entities;
using FootieData.Gateway;
using System.Windows;
using System.Windows.Controls;

namespace FootieData.Vsix
{
    public partial class ToolWindow1Control : UserControl
    {
        private FootballDataSdkGateway _gateway;

        public ToolWindow1Control()
        {
            InitializeComponent();
            _gateway = new FootballDataSdkGateway();
        }

        private void DataGridLoaded_Bundesliga1(object sender, RoutedEventArgs e)
        {
            var leagueRequest = new LeagueRequest
            {
                LeagueIdentifier = "BL1",
                LeagueTable = true,
                RecentResults = false,
                ImminentFixtures = false
            };

            var leagueResponse = _gateway.GetLeagueResponse(leagueRequest);

            var grid = sender as DataGrid;
            grid.ItemsSource = leagueResponse.Standings;
            grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
        }

        private void DataGridLoaded_PremierLeague(object sender, RoutedEventArgs e)
        {
            var leagueRequest = new LeagueRequest
            {
                LeagueIdentifier = "PL",
                LeagueTable = true,
                RecentResults = false,
                ImminentFixtures = false
            };

            var leagueResponse = _gateway.GetLeagueResponse(leagueRequest);

            var grid = sender as DataGrid;
            grid.ItemsSource = leagueResponse.Standings;
            grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
        }
    }
}