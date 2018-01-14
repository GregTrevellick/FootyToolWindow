using FootieData.Common.Helpers;
using FootieData.Entities;
using FootieData.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HierarchicalDataTemplate
{
    public partial class MainWindow : Window
    {
        private GeneralOptions _generalOptions = new GeneralOptions
        {
            LeagueOptions = new List<LeagueOption>
            {
                new LeagueOption{
                    InternalLeagueCode= HierarchicalDataTemplate.InternalLeagueCode.UK1,
                    ShowLeague = true,
                    LeagueSubOptions = new List<LeagueSubOption>
                    {new LeagueSubOption{Expand = true,GridType = GridType.Standing}}},
                new LeagueOption{
                    InternalLeagueCode= HierarchicalDataTemplate.InternalLeagueCode.UK1,
                    ShowLeague = true,
                    LeagueSubOptions = new List<LeagueSubOption>
                    {new LeagueSubOption{Expand = false,GridType = GridType.Result}}},
                new LeagueOption{
                    InternalLeagueCode= HierarchicalDataTemplate.InternalLeagueCode.UK1,
                    ShowLeague = true,
                    LeagueSubOptions = new List<LeagueSubOption>
                    {new LeagueSubOption {Expand =false,GridType = GridType.Fixture}}},
                //new LeagueOption{
                //    InternalLeagueCode= HierarchicalDataTemplate.InternalLeagueCode.DE1,
                //    ShowLeague = true,
                //    LeagueSubOptions = new List<LeagueSubOption>
                //        {new LeagueSubOption{Expand = true,GridType = GridType.Standing}}},
                //new LeagueOption{
                //    InternalLeagueCode= HierarchicalDataTemplate.InternalLeagueCode.DE1,
                //    ShowLeague = true,
                //    LeagueSubOptions = new List<LeagueSubOption>
                //        {new LeagueSubOption{Expand = true,GridType = GridType.Result}}},
                //new LeagueOption{
                //    InternalLeagueCode= HierarchicalDataTemplate.InternalLeagueCode.DE1,
                //    ShowLeague = true,
                //    LeagueSubOptions = new List<LeagueSubOption>
                //        {new LeagueSubOption {Expand = false,GridType = GridType.Fixture}}},
                //new LeagueOption{
                //    InternalLeagueCode= HierarchicalDataTemplate.InternalLeagueCode.DE2,
                //    ShowLeague = false},
                //    //LeagueSubOptions = new List<LeagueSubOption>
                //    //    {new LeagueSubOption{Expand = true,GridType = GridType.Standing}}},
                //new LeagueOption{
                //    InternalLeagueCode= HierarchicalDataTemplate.InternalLeagueCode.DE2,
                //    ShowLeague = false},
                //    //LeagueSubOptions = new List<LeagueSubOption>
                //    //    {new LeagueSubOption{Expand = true,GridType = GridType.Result}}},
                //new LeagueOption{
                //    InternalLeagueCode= HierarchicalDataTemplate.InternalLeagueCode.DE2,
                //    ShowLeague = false},
                //    //LeagueSubOptions = new List<LeagueSubOption>
                //    //    {new LeagueSubOption {Expand = false,GridType = GridType.Fixture}}},
            }
        };

        private readonly FootballDataSdkGateway _gateway;
        private readonly WpfHelper _wpfHelper;

        public MainWindow()
        {
            InitializeComponent();
            _gateway = new FootballDataSdkGateway();
            _wpfHelper = new WpfHelper();
        }

        private void ExpanderLoaded_Any(object sender, RoutedEventArgs e)
        {
            var expander = sender as Expander;
            var internalLeagueCode = InternalLeagueCode(expander.Name);
            var shouldShowLeague = ShouldShowLeague(internalLeagueCode);
            if (shouldShowLeague)
            {
                expander.Visibility = Visibility.Visible;
            }
            else
            {
                expander.Visibility = Visibility.Collapsed;
            }

            expander.Style = (Style)TryFindResource("PlusMinusExpander");
        }

        public static object TryFindResource(FrameworkElement element, object resourceKey)//gregt make private ?
        {
            var currentElement = element;

            while (currentElement != null)
            {
                var resource = currentElement.Resources[resourceKey];
                if (resource != null)
                {
                    return resource;
                }

                currentElement = currentElement.Parent as FrameworkElement;
            }

            return Application.Current.Resources[resourceKey];
        }

        private void DataGridLoaded_Any(object sender, RoutedEventArgs e)
        {
            PopulateDataGrid(sender);
        }

        private void PopulateDataGrid(object sender)
        {
            var dataGrid = sender as DataGrid;
            Expander parentExpander = dataGrid.Parent as Expander;
            var internalLeagueCode = InternalLeagueCode(parentExpander.Name);
            var shouldShowLeague = ShouldShowLeague(internalLeagueCode);

            if (shouldShowLeague)
            {
                var gridType = _wpfHelper.GetGridType(dataGrid.Name);

                parentExpander.Header= internalLeagueCode.GetDescription() + " " + gridType.GetDescription();

                if (ShouldExpandGrid(internalLeagueCode, gridType))
                {
                    var color = (Color)ColorConverter.ConvertFromString("#FFFFF0");
                    dataGrid.AlternatingRowBackground = new SolidColorBrush(color);
                    dataGrid.ColumnHeaderHeight = 2;
                    dataGrid.RowHeaderWidth = 2;
                    dataGrid.CanUserAddRows = false;
                    dataGrid.GridLinesVisibility = DataGridGridLinesVisibility.None;

                    var internalToExternalMappingExists = LeagueCodeMappings.Mappings.TryGetValue(internalLeagueCode, out ExternalLeagueCode externalLeagueCode);
                    if (internalToExternalMappingExists)
                    {
                        GetLeagueData(dataGrid, externalLeagueCode, gridType);
                        parentExpander.IsExpanded = true;
                    }
                    else
                    {
                        //TODO ERROR
                        parentExpander.IsExpanded = false;
                    }
                }
                else
                {
                    parentExpander.IsExpanded = false;
                }
            }
            else
            {
                parentExpander.Visibility = Visibility.Collapsed;
            }
        }

        private bool ShouldShowLeague(InternalLeagueCode internalLeagueCode)
        {
            if (_generalOptions.LeagueOptions.Any(x => x.InternalLeagueCode == internalLeagueCode && x.ShowLeague))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ShouldExpandGrid(InternalLeagueCode internalLeagueCode, GridType gridType)
        {
            if (_generalOptions.LeagueOptions.Any(x => x.InternalLeagueCode == internalLeagueCode
                                                       && x.ShowLeague
                                                       && x.LeagueSubOptions.Any(ccc => ccc.GridType == gridType
                                                                                        && ccc.Expand)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void GetLeagueData(DataGrid dataGrid, ExternalLeagueCode externalLeagueCode, GridType gridType)
        {
            if (gridType == GridType.Standing)
            {
                var leagueResponse = await _gateway.GetLeagueResponse_Standings(externalLeagueCode.ToString());
                dataGrid.ItemsSource = leagueResponse.Standings;
            }

            if (gridType == GridType.Result)
            {
                LeagueMatchesResults leagueMatchesResults = null;

                if (gridType == GridType.Result)
                {
                    leagueMatchesResults = await _gateway.GetLeagueResponse_Results(externalLeagueCode.ToString());
                }
                dataGrid.ItemsSource = leagueMatchesResults.MatchFixtures;

            }

            if (gridType == GridType.Fixture)
            { 
                LeagueMatchesFixtures leagueMatchesFixtures = null;

                if (gridType == GridType.Fixture)
                {
                    leagueMatchesFixtures = await _gateway.GetLeagueResponse_Fixtures(externalLeagueCode.ToString());
                }

                dataGrid.ItemsSource = leagueMatchesFixtures.MatchFixtures;
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

        private InternalLeagueCode InternalLeagueCode(string expanderName)
        {
            var internalLeagueCodeString = _wpfHelper.GetInternalLeagueCodeString(expanderName);
            var internalLeagueCode = GetInternalLeagueCode(internalLeagueCodeString);
            return internalLeagueCode;
        }

        private static InternalLeagueCode GetInternalLeagueCode(string internalLeagueCodeString)
        {
            var internalLeagueCode = (InternalLeagueCode)Enum.Parse(typeof(InternalLeagueCode), internalLeagueCodeString);
            return internalLeagueCode;
        }
    }
}

////////////private void ContextMenuOpening_Any(object sender, ContextMenuEventArgs e)
////////////{
////////////    var expander = sender as Expander;

////////////    //var style = new Style();
////////////    //style.Resources = new ResourceDictionary();
////////////    //style.Resources.Add("StaticResource", "PlusMinusExpander");
////////////    //expander.SetValue(StyleProperty, style);

////////////    foreach (MyDataGrid myDataGrid in FindVisualChildren<MyDataGrid>(expander))
////////////    {
////////////        PopulateDataGrid(myDataGrid);
////////////    }
////////////}

//private void ExpanderExpanded_Any(object sender, RoutedEventArgs e)
//{
//    //    var expander = sender as Expander;

//    //    //var style = new Style();
//    //    //style.Resources = new ResourceDictionary();
//    //    //style.Resources.Add("StaticResource", "PlusMinusExpander");
//    //    //expander.SetValue(StyleProperty, style);

//    //    var kids = FindVisualChildren<MyDataGrid>(expander);

//    //    var kidsCount = kids.Count();
//    //    //if (kids.Count() == 0)
//    //    //{
//    //    //    expander.UpdateLayout();
//    //    //    foreach (MyDataGrid myDataGrid in kids)
//    //    //    {
//    //    //        PopulateDataGrid(myDataGrid);
//    //    //    }
//    //    //}
//    //    //else
//    //    //{
//    //        foreach (MyDataGrid myDataGrid in kids)
//    //        {
//    //            PopulateDataGrid(myDataGrid);
//    //        }
//    //    //}
//}

//public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
//{
//    if (depObj != null)
//    {
//        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
//        {
//            DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
//            if (child != null && child is T)
//            {
//                yield return (T)child;
//            }

//            foreach (T childOfChild in FindVisualChildren<T>(child))
//            {
//                yield return childOfChild;
//            }
//        }
//    }
//}
