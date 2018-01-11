using FootieData.Entities;
using FootieData.Gateway;
using System.Windows;
using System.Windows.Controls;

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

        private void DataGridLoaded_PLs(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;
            GetLeagueData(grid, "PL", "s");
            Expander_PLs.Header = _leagueCaption;
        }

        private void DataGridLoaded_PLr(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;
            GetLeagueData(grid, "PL", "r");
            Expander_PLr.Header = _leagueCaption;
        }

        private void DataGridLoaded_PLf(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;
            GetLeagueData(grid, "PL", "f");
            Expander_PLf.Header = _leagueCaption;
        }

        private void DataGridLoaded_BL1s(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;
            GetLeagueData(grid, "BL1", "s");
            Expander_BL1s.Header = _leagueCaption;
        }

        private void DataGridLoaded_BL2s(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;
            GetLeagueData(grid, "BL2", "s");
            Expander_BL2s.Header = _leagueCaption;
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
    }

    //https://stackoverflow.com/questions/17121934/wpf-datagrid-can-i-decorate-my-pocos-with-attributes-to-have-custom-column-nam
}
