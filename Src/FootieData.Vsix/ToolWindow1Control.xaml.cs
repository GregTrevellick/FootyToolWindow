using FootieData.Entities;
using FootieData.Gateway;
using System.Windows;
using System.Windows.Controls;
using FootballDataOrg;

namespace FootieData.Vsix
{
    public partial class ToolWindow1Control : UserControl
    {
        private FootieDataGateway _gateway;
        private readonly CompetitionResultSingleton _competitionResultSingletonInstance;

        private bool _showPl  = true;
        private bool _expandPl  = false;
        private bool _showBl1  = true;
        private bool _expandL1 = false;
        private bool _showBl2  = false;
        private bool _expandBl2 = false;

        public ToolWindow1Control()
        {
            InitializeComponent();

            _competitionResultSingletonInstance = CompetitionResultSingleton.Instance;

            var footballDataOrgApiGateway = new FootballDataOrgApiGateway("5210" + "9775b1584a93854ca1" + "87690ed4b");
            _gateway = new FootieDataGateway(footballDataOrgApiGateway, _competitionResultSingletonInstance);

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
            var leagueResponse = _gateway.GetFromClientStandings(leagueIdentifier);

            var grid = sender as DataGrid;
            //grid.ItemsSource = leagueResponse.Result.Standings;
            grid.ItemsSource = leagueResponse;//.Standings;
            grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
        }
    }
}