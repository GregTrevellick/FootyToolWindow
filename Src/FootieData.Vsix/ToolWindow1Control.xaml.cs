using FootieData.Entities;
using FootieData.Gateway;
using System.Windows;
using System.Windows.Controls;
using FootballDataSDK.Services;

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

            var _footDataServices = new FootDataServices
            {
                AuthToken = "52109775b1584a93854ca187690ed4bb"
            };
            _gateway = new FootballDataSdkGateway(_footDataServices);

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
            var leagueResponse = _gateway.GetLeagueResponse_Standings(leagueIdentifier);

            var grid = sender as DataGrid;
            //grid.ItemsSource = leagueResponse.Result.Standings;
            grid.ItemsSource = leagueResponse.Standings;
            grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
        }
    }
}