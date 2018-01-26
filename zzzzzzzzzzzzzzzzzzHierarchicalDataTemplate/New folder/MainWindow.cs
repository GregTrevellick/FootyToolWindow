// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace HierarchicalDataTemplate
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
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

        private League2 GetLeagueResponse()
        {
            League2 league2 = new League2("Serie a")
            {
                ActualDataStandings = new List<Stringer>() { new Stringer() { Aaa = "roma", Bbb = "naples", Ccc = "milan" } },
                ActualDataResults = new List<Stringer>() { new Stringer() { Aaa = "roma 1-0 naples", Bbb = "milan 3-2 naples" } },
                ActualDataFixtures = new List<Stringer>() { new Stringer() { Aaa = "dd/mm/yy team1 v team2", Bbb = "dd/mm/yy team1 v team2", Ccc = "dd/mm/yy team1 v team2" } },
            };
            return league2;
        }

        //private void TreeView_Loaded(object sender, RoutedEventArgs e)
        //{

        //    var standingsTreePL = new TreeViewItem
        //    {
        //        IsExpanded = true,
        //        Header = "standings",
        //        ItemsSource = new string[] {"man city", "chelsea", "man utd", "arsenal"}
        //    };

        //    var resultsTreePL = new TreeViewItem
        //    {
        //        IsExpanded = true,
        //        Header = "results",
        //        ItemsSource = new string[] { "man city 1-0 chelsea", "chelsea 4-0 man utd" }
        //    };

        //    var fixturesTreePL = new TreeViewItem
        //    {
        //        IsExpanded = true,
        //        Header = "results",
        //        ItemsSource = new string[] { "dd/mm/ccyy team1 v team2", "12/1/2018 chelsea v arsenal" }
        //    };

        //    var treePL = sender as TreeView;
        //    treePL.Items.Add(standingsTreePL);
        //    treePL.Items.Add(resultsTreePL);
        //    treePL.Items.Add(fixturesTreePL);









        //    var standingsTreeBL = new TreeViewItem
        //    {
        //        IsExpanded = true,
        //        Header = "standings",
        //        ItemsSource = new string[] { "hamburg city", "chelsea", "man utd", "arsenal" }
        //    };

        //    var resultsTreeBL = new TreeViewItem
        //    {
        //        IsExpanded = true,
        //        Header = "results",
        //        ItemsSource = new string[] { "hamburg city 1-0 chelsea", "chelsea 4-0 man utd" }
        //    };

        //    var fixturesTreeBL = new TreeViewItem
        //    {
        //        IsExpanded = true,
        //        Header = "results",
        //        ItemsSource = new string[] { "dd/mm/ccyy hamburg v team2", "12/1/2018 chelsea v arsenal" }
        //    };

        //    var treeBL = sender as TreeView;
        //    treeBL.Items.Add(standingsTreeBL);
        //    treeBL.Items.Add(resultsTreeBL);
        //    treeBL.Items.Add(fixturesTreeBL);








        //    var itemPL = new TreeViewItem
        //    {
        //        IsExpanded = true,
        //        Header = "Prem lge",
        //        ItemsSource = new string[] {"Standings", "Results", "Fixtures"}
        //    };

        //    var itemBL = new TreeViewItem
        //    {
        //        IsExpanded = true,
        //        Header = "Budesliga",

        //        ItemsSource = new string[] {"Standings", "Results", "Fixtures"}
        //    };

        //    // ... Get TreeView reference and add both items.
        //    var tree = sender as TreeView;
        //    tree.Items.Add(itemPL);
        //    tree.Items.Add(itemBL);
        //}

        //private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //{
        //    var tree = sender as TreeView;

        //    // ... Determine type of SelectedItem.
        //    if (tree.SelectedItem is TreeViewItem)
        //    {
        //        // ... Handle a TreeViewItem.
        //        var item = tree.SelectedItem as TreeViewItem;
        //        this.Title = "Selected header: " + item.Header.ToString();
        //    }
        //    else if (tree.SelectedItem is string)
        //    {
        //        // ... Handle a string.
        //        this.Title = "Selected: " + tree.SelectedItem.ToString();
        //    }
        //}
    }
}
