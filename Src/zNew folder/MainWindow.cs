using FootballDataSDK.Services;
using FootieData.Common;
using FootieData.Common.Helpers;
using FootieData.Entities;
using FootieData.Gateway;
using HierarchicalDataTemplate.ReferenceData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HierarchicalDataTemplate
{
    public partial class MainWindow : Window
    {
        private static WpfHelper _wpfHelper;
        private static GeneralOptions _generalOptions;
        private readonly SolidColorBrush _colorRefreshed;
        private readonly SoccerSeasonResultSingleton _soccerSeasonResultSingletonInstance;
        private readonly IEnumerable<NullReturn> _nullStandings = new List<NullReturn> { new NullReturn { Error = $"League table {Unavailable}" } };
        private readonly IEnumerable<NullReturn> _nullFixturePasts = new List<NullReturn> { new NullReturn { Error = $"Results {Unavailable}" } };
        private readonly IEnumerable<NullReturn> _nullFixtureFutures = new List<NullReturn> { new NullReturn { Error = $"Fixtures {Unavailable}" } };
        private const string Unavailable = "unavailable at this time - try again later";

        public MainWindow()
        {
            InitializeComponent();

            _colorRefreshed = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FFFF"));
            _soccerSeasonResultSingletonInstance = SoccerSeasonResultSingleton.Instance;
            _wpfHelper = new WpfHelper();

            GetOptions();
        }

        private static void GetOptions()
        {
            _generalOptions = new TempryGetOptions().GetGeneralOptions();
        }

        private FootballDataSdkGateway GetGateway()
        {
            var footDataServices = new FootDataServices
            {
                AuthToken = "52109775b1584a93854ca187690ed4bb"
            };
            return new FootballDataSdkGateway(footDataServices, _soccerSeasonResultSingletonInstance);
        }

        private void ExpanderLoaded_Any(object sender, RoutedEventArgs e)
        {
            if (sender is Expander expander)
            {
                var internalLeagueCode = GetInternalLeagueCode(expander.Name);
                var shouldShowLeague = ShouldShowLeague(internalLeagueCode);
                if (shouldShowLeague)
                {
                    expander.Visibility = Visibility.Visible;
                    expander.Style = (Style)TryFindResource("PlusMinusExpander");
                }
                else
                {
                    expander.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                Logger.Log("Internal error gregt");
            }
        }

        private static bool ShouldShowLeague(InternalLeagueCode internalLeagueCode)
        {
            return _generalOptions.LeagueOptions.Any(x => x.InternalLeagueCode == internalLeagueCode && x.ShowLeague);
        }

        private async void DataGridLoaded_Any(object sender, RoutedEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                if (dataGrid.Parent is Expander parentExpander)
                {
                    var gridType = _wpfHelper.GetGridType(dataGrid.Name);
                    var parentExpanderName = parentExpander.Name;
                    var internalLeagueCode = GetInternalLeagueCode(parentExpanderName);
                    var shouldShowLeague = ShouldShowLeague(internalLeagueCode);
                    parentExpander.Header = internalLeagueCode.GetDescription() + " " + gridType.GetDescription();

                    var internalToExternalMappingExists = LeagueCodeMappings.Mappings.TryGetValue(internalLeagueCode, out var externalLeagueCode);
                    var shouldExpandGrid = ShouldExpandGrid(internalLeagueCode, gridType);
                    ///////////////////////////////////////////////////parentExpander.IsExpanded = true;
                    parentExpander.IsExpanded = shouldExpandGrid;

                    try
                    {
                        switch (gridType)
                        {
                            case GridType.Standing:
                                var standings = await GetStandingsAsync(gridType, shouldShowLeague, internalLeagueCode, internalToExternalMappingExists, shouldExpandGrid, externalLeagueCode); //wont run til web service call has finished
                                dataGrid.ItemsSource = standings ?? (IEnumerable)_nullStandings;
                                break;
                            case GridType.Result:
                                var results = await GetFixturePastsAsync(gridType, shouldShowLeague, internalLeagueCode, internalToExternalMappingExists, shouldExpandGrid, externalLeagueCode); //wont run til web service call finished
                                dataGrid.ItemsSource = results ?? (IEnumerable)_nullFixturePasts;
                                break;
                            case GridType.Fixture:
                                var fixtures = await GetFixtureFuturesAsync(gridType, shouldShowLeague, internalLeagueCode, internalToExternalMappingExists, shouldExpandGrid, externalLeagueCode); //wont run til web service call has finished
                                dataGrid.ItemsSource = fixtures ?? (IEnumerable)_nullFixtureFutures;
                                break;
                        }
                    }
                    catch (Exception)
                    {                       
                        parentExpander.Header = "DataGridLoaded_Any internal error";
                    }
                }
            }
        }

        private async Task<IEnumerable<Standing>> GetStandingsAsync(GridType gridType, bool shouldShowLeague, InternalLeagueCode internalLeagueCode, bool internalToExternalMappingExists, bool shouldExpandGrid, ExternalLeagueCode externalLeagueCode)
        {
            try
            {
                var theTask = Task.Run(() =>
                {
                    IEnumerable<Standing> result = null;
                    if (shouldShowLeague)
                    {
                        //var internalToExternalMappingExists = LeagueCodeMappings.Mappings.TryGetValue(internalLeagueCode, out var externalLeagueCode);
                        //var shouldExpandGrid = ShouldExpandGrid(internalLeagueCode, gridType);
                        if (shouldExpandGrid && internalToExternalMappingExists)
                        {
                            var gateway = GetGateway();
                            result = gateway.GetFromClientStandings(externalLeagueCode.ToString());
                        }
                    }
                    return result;
                });
                await Task.WhenAll(theTask);

                return theTask.Result;
            }
            catch (Exception)
            {
                return new List<Standing> { new Standing { Team = "GetStandingsAsync internal error" } };
            }
        }

        private async Task<IEnumerable<FixturePast>> GetFixturePastsAsync(GridType gridType, bool shouldShowLeague, InternalLeagueCode internalLeagueCode, bool internalToExternalMappingExists, bool shouldExpandGrid, ExternalLeagueCode externalLeagueCode)
        {
            try
            {
                var theTask = Task.Run(() =>
                {
                    IEnumerable<FixturePast> result = null;
                    if (shouldShowLeague)
                    {
                        //var internalToExternalMappingExists = LeagueCodeMappings.Mappings.TryGetValue(internalLeagueCode, out var externalLeagueCode);
                        //var shouldExpandGrid = ShouldExpandGrid(internalLeagueCode, gridType);
                        if (shouldExpandGrid && internalToExternalMappingExists)
                        {
                            var gateway = GetGateway();
                            result = gateway.GetFromClientFixturePasts(externalLeagueCode.ToString(), "p7");
                        }
                    }
                    return result;
                });
                await Task.WhenAll(theTask);

                return theTask.Result;
            }
            catch (Exception)
            {
                return new List<FixturePast> { new FixturePast { HomeName = "GetResultsAsync internal error" } };
            }
        }

        private async Task<IEnumerable<FixtureFuture>> GetFixtureFuturesAsync(GridType gridType, bool shouldShowLeague, InternalLeagueCode internalLeagueCode, bool internalToExternalMappingExists, bool shouldExpandGrid, ExternalLeagueCode externalLeagueCode)
        {
            try
            {
                var theTask = Task.Run(() =>
                {
                    IEnumerable<FixtureFuture> result = null;
                    if (shouldShowLeague)
                    {
                        //var internalToExternalMappingExists = LeagueCodeMappings.Mappings.TryGetValue(internalLeagueCode, out var externalLeagueCode);
                        //var shouldExpandGrid = ShouldExpandGrid(internalLeagueCode, gridType);
                        if (shouldExpandGrid && internalToExternalMappingExists)
                        {
                            var gateway = GetGateway();
                            result = gateway.GetFromClientFixtureFutures(externalLeagueCode.ToString(), "n7");
                        }
                    }
                    return result;
                });
                await Task.WhenAll(theTask);

                return theTask.Result;
            }
            catch (Exception)
            {
                return new List<FixtureFuture> { new FixtureFuture { HomeName = "GetFixturesAsync internal error" } };
            }
        }

        private static bool ShouldExpandGrid(InternalLeagueCode internalLeagueCode, GridType gridType)
        {
            return _generalOptions.LeagueOptions.Any(x => x.InternalLeagueCode == internalLeagueCode
                                                          && x.ShowLeague
                                                          && x.LeagueSubOptions.Any(y => y.GridType == gridType
                                                                                         && y.Expand));
        }

        private void Click_Handler1(object sender, RoutedEventArgs e)
        {
            TextBlockBossMode.Text = CommonConstants.TheBossIsCommingText;
            StackPanelLeagueMode.Visibility = Visibility.Collapsed;
            StackPanelBossMode.Visibility = Visibility.Visible;
        }

        private void Click_Handler2(object sender, RoutedEventArgs e)
        {
            StackPanelLeagueMode.Visibility = Visibility.Visible;
            StackPanelBossMode.Visibility = Visibility.Collapsed;
        }

        private static InternalLeagueCode GetInternalLeagueCode(string expanderName)
        {
            var internalLeagueCodeString = _wpfHelper.GetInternalLeagueCodeString(expanderName);
            return (InternalLeagueCode)Enum.Parse(typeof(InternalLeagueCode), internalLeagueCodeString);
        }

        private static object TryFindResource(FrameworkElement frameworkElement, object resourceKey)
        {
            var currentElement = frameworkElement;

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

        private void Click_HandlerRefresh(object sender, RoutedEventArgs e)
        {
            GetOptions();

            var dataGrid = FindChild<DataGrid>(Application.Current.MainWindow, "Standing_Rand0001");
            DataGridLoaded_Any(dataGrid, null);
            dataGrid.AlternatingRowBackground = _colorRefreshed;

            dataGrid = FindChild<DataGrid>(Application.Current.MainWindow, "Results1_Rand0001");
            DataGridLoaded_Any(dataGrid, null);
            dataGrid.AlternatingRowBackground = _colorRefreshed;

            dataGrid = FindChild<DataGrid>(Application.Current.MainWindow, "Fixtures_Rand0001");
            DataGridLoaded_Any(dataGrid, null);
            dataGrid.AlternatingRowBackground = _colorRefreshed;
        }

        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, a null parent is being returned.</returns>
        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null)
            {
                return null;
            }

            T foundChild = null;

            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                
                // If the child is not of the request child type child
                T childType = child as T;

                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else
                {
                    if (!string.IsNullOrEmpty(childName))
                    {
                        var frameworkElement = child as FrameworkElement;

                        // If the child's name is set for search
                        if (frameworkElement != null && frameworkElement.Name == childName)
                        {
                            // if the child's name is of the request name
                            foundChild = (T)child;
                            break;
                        }
                    }
                    else
                    {
                        // child element found.
                        foundChild = (T)child;
                        break;
                    }
                }
            }

            return foundChild;
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


////////////////////var leagueSubOptionsToShow = _generalOptions.LeagueOptions.Where(x => x.ShowLeague);
////////////////////foreach (var subOptionsToShow in leagueSubOptionsToShow)
////////////////////{
////////////////////    var internalToExternalMappingExists = LeagueCodeMappings.Mappings.TryGetValue(subOptionsToShow.InternalLeagueCode, out ExternalLeagueCode externalLeagueCode);
////////////////////    if (internalToExternalMappingExists)
////////////////////    {
////////////////////        foreach (var subOption in subOptionsToShow.LeagueSubOptions)
////////////////////        {
////////////////////            if (subOption.Expand)
////////////////////            {
////////////////////                LoadLeagueToShow(externalLeagueCode, subOption.GridType);
////////////////////            }
////////////////////        }
////////////////////    }                
////////////////////}





//static async void PopulateDataGrid1Async(object sender)
//{
//    // This method runs asynchronously.
//    await Task.Run(() => PopulateDataGrid2Async(sender));
//}

//private static async void PopulateDataGrid2Async(object sender)
//private static void PopulateDataGrid2Async(object sender)
//private void GetAndPopulateDataGrid(object sender)
//{
//    var dataGrid = sender as DataGrid;
//    Expander parentExpander = dataGrid.Parent as Expander;
//    var internalLeagueCode = InternalLeagueCode(parentExpander.Name);
//    var shouldShowLeague = ShouldShowLeague(internalLeagueCode);

//    if (shouldShowLeague)
//    {
//        var gridType = _wpfHelper.GetGridType(dataGrid.Name);

//        parentExpander.Header= internalLeagueCode.GetDescription() + " " + gridType.GetDescription();

//        if (ShouldExpandGrid(internalLeagueCode, gridType))
//        {
//            var color = (Color)ColorConverter.ConvertFromString("#FFFFF0");
//            dataGrid.AlternatingRowBackground = new SolidColorBrush(color);
//            dataGrid.ColumnHeaderHeight = 2;
//            dataGrid.RowHeaderWidth = 2;
//            dataGrid.CanUserAddRows = false;
//            dataGrid.GridLinesVisibility = DataGridGridLinesVisibility.None;

//            var internalToExternalMappingExists = LeagueCodeMappings.Mappings.TryGetValue(internalLeagueCode, out ExternalLeagueCode externalLeagueCode);
//            if (internalToExternalMappingExists)
//            {
//                GetLeagueData(dataGrid, externalLeagueCode, gridType);
//                parentExpander.IsExpanded = true;
//            }
//            else
//            {
//                //TODO ERROR
//                parentExpander.IsExpanded = false;
//            }
//        }
//        else
//        {
//            parentExpander.IsExpanded = false;
//        }
//    }
//    else
//    {
//        parentExpander.Visibility = Visibility.Collapsed;
//    }
//}

//private static bool ShouldShowLeague(InternalLeagueCode internalLeagueCode)
//{
//    if (_generalOptions.LeagueOptions.Any(x => x.InternalLeagueCode == internalLeagueCode && x.ShowLeague))
//    {
//        return true;
//    }
//    else
//    {
//        return false;
//    }
//}

//private static bool ShouldExpandGrid(InternalLeagueCode internalLeagueCode, GridType gridType)
//{
//    if (_generalOptions.LeagueOptions.Any(x => x.InternalLeagueCode == internalLeagueCode
//                                               && x.ShowLeague
//                                               && x.LeagueSubOptions.Any(ccc => ccc.GridType == gridType
//                                                                                && ccc.Expand)))
//    {
//        return true;
//    }
//    else
//    {
//        return false;
//    }
//}

//private static async void GetLeagueData(DataGrid dataGrid, ExternalLeagueCode externalLeagueCode, GridType gridType)
////private static void GetLeagueData(DataGrid dataGrid, ExternalLeagueCode externalLeagueCode, GridType gridType)
//{
//    if (gridType == GridType.Standing)
//    {
//        //var leagueResponse = await _gateway.GetLeagueResponse_Standings(externalLeagueCode.ToString());
//        //dataGrid.ItemsSource = leagueResponse.Standings;

//        if (dataGridStanding2s.Any(x => x.ExternalLeagueCode == externalLeagueCode &&
//                                        x.Standings.Count > 0))
//        {
//            //dataGridStanding2s is already populated so do nowt, not even setting the data grid source (as that will already have happened)
//        }
//        else
//        {
//            //dataGridStanding2s is not populated for this league
//            LoadLeagueToShow(externalLeagueCode, gridType);
//            var dataGridItemsSource = dataGridStanding2s
//                ?.Where(x => x.ExternalLeagueCode == externalLeagueCode)
//                .Select(x => x.Standings);
//            dataGrid.ItemsSource = dataGridItemsSource.First();
//        }
//    }

//    if (gridType == GridType.Result)
//    {
//        ////LeagueMatchesResults leagueMatchesResults = null;
//        ////if (gridType == GridType.Result)
//        ////{
//        ////    leagueMatchesResults = await _gateway.GetLeagueResponse_Results(externalLeagueCode.ToString());
//        ////}
//        ////dataGrid.ItemsSource = leagueMatchesResults.MatchFixtures;
//        //dataGrid.ItemsSource = dataGridResult2s?.Where(x => x.ExternalLeagueCode == externalLeagueCode).Select(x => x.Results); 

//        if (dataGridResult2s.Any(x => x.ExternalLeagueCode == externalLeagueCode &&
//                                        x.Results.Count > 0))
//        {
//            //dataGridResult2s is already populated so do nowt, not even setting the data grid source (as that will already have happened)
//        }
//        else
//        {
//            //dataGridResult2s is not populated for this league
//            LoadLeagueToShow(externalLeagueCode, gridType);
//            var dataGridItemsSource = dataGridResult2s
//                ?.Where(x => x.ExternalLeagueCode == externalLeagueCode)
//                .Select(x => x.Results);
//            dataGrid.ItemsSource = dataGridItemsSource.First();
//        }
//    }

//    if (gridType == GridType.Fixture)
//    {
//        ////LeagueMatchesFixtures leagueMatchesFixtures = null;
//        ////if (gridType == GridType.Fixture)
//        ////{
//        ////    leagueMatchesFixtures = await _gateway.GetLeagueResponse_Fixtures(externalLeagueCode.ToString());
//        ////}
//        ////dataGrid.ItemsSource = leagueMatchesFixtures.MatchFixtures;
//        //dataGrid.ItemsSource = dataGridFixture2s?.Where(x => x.ExternalLeagueCode == externalLeagueCode).Select(x => x.Fixtures);

//        if (dataGridFixture2s.Any(x => x.ExternalLeagueCode == externalLeagueCode &&
//                                      x.Fixtures.Count > 0))
//        {
//            //dataGridFixture2s is already populated so do nowt, not even setting the data grid source (as that will already have happened)
//        }
//        else
//        {
//            //dataGridFixture2s is not populated for this league
//            LoadLeagueToShow(externalLeagueCode, gridType);
//            var dataGridItemsSource = dataGridFixture2s
//                ?.Where(x => x.ExternalLeagueCode == externalLeagueCode)
//                .Select(x => x.Fixtures);
//            dataGrid.ItemsSource = dataGridItemsSource.First();
//        }
//    }
//}




//private static async void LoadLeagueToShow(ExternalLeagueCode externalLeagueCode, GridType gridType)
//{
//    await Task.Run(() => LoadShownData(externalLeagueCode, gridType));
//}
//private static void LoadLeagueToShow(ExternalLeagueCode externalLeagueCode, GridType gridType)
//{
//    LoadShownData(externalLeagueCode, gridType);
//}

//private static async void LoadShownData(ExternalLeagueCode externalLeagueCode, GridType gridType)
//private static void LoadShownData(ExternalLeagueCode externalLeagueCode, GridType gridType)
//{
//    if (gridType == GridType.Standing)
//    {
//        //var leagueResponse = await _gateway.GetLeagueResponse_Standings(externalLeagueCode.ToString());
//        var leagueResponse = _gateway.GetLeagueResponse_Standings(externalLeagueCode.ToString());
//        dataGridStanding2s.Clear();
//        dataGridStanding2s.Add(new DataGridStanding2
//        {
//            ExternalLeagueCode = externalLeagueCode,
//            Standings = (List<Standing>)leagueResponse.Standings
//        });
//    }

//    if (gridType == GridType.Result)
//    {
//        //var leagueMatchesResults = await _gateway.GetLeagueResponse_Results(externalLeagueCode.ToString());                
//        var leagueMatchesResults = _gateway.GetLeagueResponse_Results(externalLeagueCode.ToString());
//        dataGridResult2s.Add(new DataGridResult2
//        {
//            ExternalLeagueCode = externalLeagueCode,
//            Results = (List<Fixture>)leagueMatchesResults.MatchFixtures
//        });
//    }

//    if (gridType == GridType.Fixture)
//    {
//        //var leagueMatchesFixtures = await _gateway.GetLeagueResponse_Fixtures(externalLeagueCode.ToString());
//        var leagueMatchesFixtures = _gateway.GetLeagueResponse_Fixtures(externalLeagueCode.ToString());
//        dataGridFixture2s.Add(new DataGridFixture2
//        {
//            ExternalLeagueCode = externalLeagueCode,
//            Fixtures = (List<Fixture>)leagueMatchesFixtures.MatchFixtures
//        });
//    }
//}


//private void GetAndPopulateDataGrid(GridType gridType,  ExternalLeagueCode externalLeagueCode)
//{
//    switch (gridType)
//    {
//        case GridType.Unknown:
//            break;
//        case GridType.Standing:
//            dataGridItemsSourceStanding = GetLeagueDataStanding(externalLeagueCode);/*oops dataGridItemsSourceStanding is shared but ought to be returned to caller */
//            break;
//        case GridType.Result:
//            dataGridItemsSourceResult = GetLeagueDataResult(externalLeagueCode);/*oops dataGridItemsSourceStanding is shared but ought to be returned to caller */
//            break;
//        case GridType.Fixture:
//            dataGridItemsSourceFixture = GetLeagueDataFixture(externalLeagueCode);/*oops dataGridItemsSourceStanding is shared but ought to be returned to caller */
//            break;
//    }
//}

//private async Task<string> PopulateDataGridAsync(string parentExpanderName, GridType gridType)
//{
//    try
//    {
//        var theTask = Task.Run(() =>
//        {
//            var internalLeagueCode = InternalLeagueCode(parentExpanderName);
//            var shouldShowLeague = ShouldShowLeague(internalLeagueCode);
//            var result = internalLeagueCode.GetDescription() + " " + gridType.GetDescription();
//            if (shouldShowLeague)
//            {
//                var internalToExternalMappingExists = LeagueCodeMappings.Mappings.TryGetValue(internalLeagueCode, out ExternalLeagueCode externalLeagueCode);
//                var shouldExpandGrid = ShouldExpandGrid(internalLeagueCode, gridType);
//                if (shouldExpandGrid && internalToExternalMappingExists)
//                {
//                    switch (gridType)
//                    {
//                        case GridType.Unknown:
//                            break;
//                        case GridType.Standing:
//                            var dataGridItemsSourceStanding = GetLeagueDataStanding(externalLeagueCode);
//                            break;
//                        case GridType.Result:
//                            var dataGridItemsSourceResult = GetLeagueDataResult(externalLeagueCode);
//                            break;
//                        case GridType.Fixture:
//                            var dataGridItemsSourceFixture = GetLeagueDataFixture(externalLeagueCode);
//                            break;
//                    }

//                }
//            }
//            return result;
//        });
//        await Task.WhenAll(theTask);

//        return theTask.Result;
//    }
//    catch (Exception)
//    {
//        return "login failed";
//    }
//}


//private List<Standing> GetLeagueDataStanding(ExternalLeagueCode externalLeagueCode)
//{
//    var dataGridItemsSource = LoadShownDataStanding(externalLeagueCode);
//    return dataGridItemsSource;
//}

//private List<Fixture> GetLeagueDataResult(ExternalLeagueCode externalLeagueCode)
//{
//    var dataGridItemsSource = LoadShownDataResult(externalLeagueCode);
//    return dataGridItemsSource;
//}

//private List<Fixture> GetLeagueDataFixture(ExternalLeagueCode externalLeagueCode)
//{
//    var dataGridItemsSource = LoadShownDataFixture(externalLeagueCode);
//    return dataGridItemsSource;
//}

//private static List<Standing> LoadShownDataStanding(ExternalLeagueCode externalLeagueCode)
//{
//    var leagueResponse = _gateway.GetLeagueResponse_Standings(externalLeagueCode.ToString());
//    return (List<Standing>)leagueResponse.Standings;
//}

//private static List<Fixture> LoadShownDataResult(ExternalLeagueCode externalLeagueCode)
//{
//    var leagueMatchesResults = _gateway.GetLeagueResponse_Results(externalLeagueCode.ToString());
//    return (List<Fixture>)leagueMatchesResults.MatchFixtures;
//}

//private static List<Fixture> LoadShownDataFixture(ExternalLeagueCode externalLeagueCode)
//{
//    var leagueMatchesFixtures = _gateway.GetLeagueResponse_Fixtures(externalLeagueCode.ToString());
//    return (List<Fixture>)leagueMatchesFixtures.MatchFixtures;
//}

//////////////private static InternalLeagueCode GetInternalLeagueCode(string internalLeagueCodeString)
//////////////{
//////////////    var internalLeagueCode = (InternalLeagueCode)Enum.Parse(typeof(InternalLeagueCode), internalLeagueCodeString);
//////////////    return internalLeagueCode;
//////////////}

//#region button
//private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
//{
//try
//{
//var result = await LoginAsync();
////next line wont run til LoginAsync has finished
//MyBtn.Content = result;

//}
//catch (Exception)
//{
//MyBtn.Content = "internal error";
//}
//}

//private async Task<string> LoginAsync()
//{
//try
//{
////the next 3 run at same time 
//var loginTask = Task.Run(() =>
//{
//Thread.Sleep(2000);
//return "login successful";
//});

//var payTask = Task.Delay(2000);

//var purchaseTask = Task.Delay(1000);

////next line ensures that only when all 3 are done do we return 
//await Task.WhenAll(loginTask, payTask, purchaseTask);

////all 3 done before this line runs
//return loginTask.Result;
//}
//catch (Exception)
//{
//return "login failed";
//}
//}
//#endregion
