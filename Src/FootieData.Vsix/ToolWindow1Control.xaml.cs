using FootieData.Entities;
using FootieData.Gateway;
using System.Windows;
using System.Windows.Controls;

namespace FootieData.Vsix
{
    public partial class ToolWindow1Control : UserControl
    {
        private FootballDataSdkGateway _gateway;

        private bool _showPl  = true;
        private bool _expandPl  = false;
        private bool _showBl1  = true;
        private bool _expandL1 = false;
        private bool _showBl2  = false;
        private bool _expandBl2 = false;

        public ToolWindow1Control()
        {
            InitializeComponent();
            _gateway = new FootballDataSdkGateway();

            if (_showPl)
            {
                //todo
            }

            if (_showBl1)
            {
                //todo
            }

            if (_showBl2)
            {
                //todo
            }
        }

        private void DataGridLoaded_BL1(object sender, RoutedEventArgs e)
        {
            GetLeagueData(sender, "BL1");
        }

        private void DataGridLoaded_BL2(object sender, RoutedEventArgs e)
        {
            GetLeagueData(sender, "BL2");
        }

        private void DataGridLoaded_PL(object sender, RoutedEventArgs e)
        {
            GetLeagueData(sender, "PL");
        }

        private void GetLeagueData(object sender, string leagueIdentifier)
        {
            var leagueRequest = new LeagueRequest
            {
                LeagueIdentifier = leagueIdentifier,
                LeagueTable = true,
                RecentResults = false,
                ImminentFixtures = false
            };

            var leagueResponse = _gateway.GetLeagueResponse_Standings(leagueRequest);

            var grid = sender as DataGrid;
            grid.ItemsSource = leagueResponse.Standings;
            grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
        }
    }
}