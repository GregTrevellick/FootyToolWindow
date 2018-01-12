using FootieData.Entities;
using FootieData.Gateway;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

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

        private void DataGridLoaded_Any(object sender, RoutedEventArgs e)
        {
            var grid = sender as DataGrid;

            var color = (Color)ColorConverter.ConvertFromString("Red");
            grid.AlternatingRowBackground = new SolidColorBrush(color);
            grid.ColumnHeaderHeight = 2;
            grid.RowHeaderWidth = 2;

            var str = grid.Name;
            var srf = str.Substring(str.Length - 1);//last char of name
            var leagueIdentifier = str.Remove(str.Length - 1, 1); //everything except the last char
            GetLeagueData(grid, leagueIdentifier, srf);

            Expander_PLs.Header = _leagueCaption;
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

    //https://stackoverflow.com/questions/17121934/wpf-datagrid-can-i-decorate-my-pocos-with-attributes-to-have-custom-column-nam
}
