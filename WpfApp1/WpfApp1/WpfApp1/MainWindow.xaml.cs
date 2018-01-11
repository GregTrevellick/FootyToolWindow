using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DataGridLoaded_BL1(object sender, RoutedEventArgs e)
        {
            var standings = new List<Standing>();
            standings.Add(new Standing { Against = 1, Diff = 2, For = 3, Played = 4, Points = 5, Rank = 6, Team = "manu" });
            standings.Add(new Standing { Against = 1, Diff = 2, For = 3, Played = 4, Points = 5, Rank = 6, Team = "city" });
            standings.Add(new Standing { Against = 1, Diff = 2, For = 3, Played = 4, Points = 5, Rank = 6, Team = "manu" });
            standings.Add(new Standing { Against = 1, Diff = 2, For = 3, Played = 4, Points = 5, Rank = 6, Team = "city" });
            standings.Add(new Standing { Against = 1, Diff = 2, For = 3, Played = 4, Points = 5, Rank = 6, Team = "manu" });
            standings.Add(new Standing { Against = 1, Diff = 2, For = 3, Played = 4, Points = 5, Rank = 6, Team = "city" });
            var grid = sender as DataGrid;
            grid.ItemsSource = standings;
            grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
        }
    }

    public class Standing
    {
        [Description("Pos")]
        public int Rank { get; set; }
        [Description("club")]
        public string Team { get; set; }
        [Description("P")]
        public int Played { get; set; }
        //public string CrestURI { get; set; }
        public int For { get; set; }
        public int Against { get; set; }
        [Description("GD")]
        public int Diff { get; set; }
        [Description("Pts")]
        public int Points { get; set; }
    }

    //https://stackoverflow.com/questions/17121934/wpf-datagrid-can-i-decorate-my-pocos-with-attributes-to-have-custom-column-nam
    public class MyDataGrid : DataGrid  
    {
        protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                base.OnAutoGeneratingColumn(e);
                var propDescr = e.PropertyDescriptor as System.ComponentModel.PropertyDescriptor;
                e.Column.Header = propDescr.Description;
            }
            catch (Exception ex)
            {
                //Utils.ReportException(ex);
            }
        }

    }
}
