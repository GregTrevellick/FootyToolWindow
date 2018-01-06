using FootieData.Gateway;
using System.Windows;
using System.Windows.Controls;
using FootyData.Entities;

namespace FootieData.Ui
{
    public partial class FootieUserControl : UserControl
    {
        public FootieUserControl()
        {
            InitializeComponent();
            //SizeToContent = SizeToContent.WidthAndHeight;
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var gateway = new FootballDataSDKGateway();
            var leagueTable = gateway.GetLeagueTable(new LeagueRequest
            {
                LeagueTable = true,
                RecentResults = false,
                ImminentFixtures = false
            });

            var grid = sender as DataGrid;
            grid.ItemsSource = leagueTable.Standings;
            grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
        }
    }
}