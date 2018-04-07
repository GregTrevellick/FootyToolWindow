using FootieData.Common;
using FootieData.Common.Options;
using FootieData.Entities;
using FootieData.Entities.ReferenceData;
using FootieData.Gateway;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FootieData.Vsix
{
    public partial class ToolWindow1Control : UserControl
    {
        public delegate void EventHandler();
        public event EventHandler BossModeEventHandler;
        public event EventHandler LeagueModeEventHandler;
        public static LeagueGeneralOptions LeagueGeneralOptions { get; set; }

        private readonly CompetitionResultSingleton _competitionResultSingletonInstance;
        private readonly LeagueDtosSingleton _leagueDtosSingletonInstance;
        private readonly IEnumerable<NullReturn> _nullStandings = new List<NullReturn> { new NullReturn { Error = $"League table {Unavailable}" } };
        private readonly Style _awayStyle;
        private readonly Style _homeStyle;
        private readonly Style _rightAlignStyle;
        private readonly IEnumerable<NullReturn> _zeroFixturePasts = new List<NullReturn> { new NullReturn { Error = $"No results available for the past {CommonConstants.DaysCount} days" } };
        private readonly IEnumerable<NullReturn> _zeroFixtureFutures = new List<NullReturn> { new NullReturn { Error = $"No fixtures available for the next {CommonConstants.DaysCount} days" } };

        private const string PoliteRequestLimitReached = "The free request limit has been reached - please w";
        private const string RequestLimitReached = "You reached your request limit. W";
        private const string Unavailable = "unavailable at this time - please try again later";

        private static Func<string, DateTime> GetLastUpdatedDate { get; set; }
        private static Action<string> GetOptionsFromStoreAndMapToInternalFormatMethod { get; set; }
        private static Action<string> UpdateLastUpdatedDate { get; set; }

        //gregt
        private static void SomeLongRunningCode()
        {
            for (int i = 0; i < 10_000_000; i++)//gregt long running code - circa 12/13 seconds
            {
                DateTime.Now.ToString();
            }
        }

        public ToolWindow1Control(Action<string> getOptionsFromStoreAndMapToInternalFormatMethod, Action<string> updateLastUpdatedDate, Func<string, DateTime> getLastUpdatedDate)
        {
            InitializeComponent();

            try
            {
                //expensive (calls the rest api in perhaps a call stack that is non-async) - do on a background thread if possible
                _competitionResultSingletonInstance = CompetitionResultSingleton.Instance;//This is slow, the rest is fast
                ////////////////////////////////////SomeLongRunningCode();
                ////////////////////////////////////_competitionResultSingletonInstance = new CompetitionResultSingleton();
            }
            catch (Exception)
            {
                //Do nothing - the resultant null _competitionResultSingletonInstance is handled further down the call stack
            }

            GetLastUpdatedDate = getLastUpdatedDate;
            GetOptionsFromStoreAndMapToInternalFormatMethod = getOptionsFromStoreAndMapToInternalFormatMethod;
            UpdateLastUpdatedDate = updateLastUpdatedDate;
            _leagueDtosSingletonInstance = LeagueDtosSingleton.Instance;

            var rightAlignSetter = new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right);

            _rightAlignStyle = new Style();
            _rightAlignStyle.Setters.Add(rightAlignSetter);
            
            var homeAwayFontColour = Brushes.SlateGray;
            _homeStyle = new Style();
            _homeStyle.Setters.Add(rightAlignSetter);
            _homeStyle.Setters.Add(new Setter(ForegroundProperty, homeAwayFontColour));
            _awayStyle = new Style();
            _awayStyle.Setters.Add(rightAlignSetter);
            _awayStyle.Setters.Add(new Setter(ForegroundProperty, homeAwayFontColour));

            PopulateUi(false);
            //////////////////////////////////////////////////////////////////////////DoStuff(false);
        }

        private FootieDataGateway GetFootieDataGateway()
        {
            return new FootieDataGateway(_competitionResultSingletonInstance);
        }

        private async void ExpanderAny_OnExpanded(object sender, RoutedEventArgs e)
        {
            if (sender is Expander expander)
            {
                var gridType = WpfHelper.GetGridType(expander.Name);
                var internalLeagueCode = WpfHelper.GetInternalLeagueCode(expander.Name);

                if (expander.Content is DataGrid dataGrid)
                {
                    await DataGridLoadedAsync(dataGrid, internalLeagueCode, gridType);
                }
                else
                {
                    dataGrid = GetMyDataGrid(internalLeagueCode, gridType);
                    expander.Content = dataGrid;
                }
            }
            else
            {
                Logger.Log("Internal error 1002 - sender is not Expander");
            }
        }

        private async Task DataGridLoadedAsync(DataGrid dataGrid, InternalLeagueCode internalLeagueCode, GridType gridType)
        {         
            var externalLeagueCode = _leagueDtosSingletonInstance.LeagueDtos.Single(x => x.InternalLeagueCode == internalLeagueCode).ExternalLeagueCode;

            var dataGridEmpty = DataGridHelper.IsDataGridEmpty(dataGrid);

            if (dataGridEmpty)
            {
                try
                {                    
                    switch (gridType)
                    {
                        case GridType.Standing:
                            var standings = await GetStandingsAsync(externalLeagueCode); //wont run til web service call has finished
                            var standingsList = standings.ToList();
                            if (standingsList.Any(x => x.Team != null && x.Team.StartsWith(RequestLimitReached)))
                            {
                                dataGrid.ItemsSource = new List<NullReturn> { new NullReturn {Error = standingsList.First(x => x.Team.StartsWith(RequestLimitReached)).Team.Replace(RequestLimitReached, PoliteRequestLimitReached) } };
                                dataGrid.HeadersVisibility = DataGridHeadersVisibility.None;
                            }
                            else
                            {
                                if (standingsList.Any(x => x.Team != null && x.Team.StartsWith(EntityConstants.PotentialTimeout)))
                                {
                                    dataGrid.ItemsSource = new List<NullReturn> { new NullReturn { Error = EntityConstants.PotentialTimeout } } ;
                                    dataGrid.HeadersVisibility = DataGridHeadersVisibility.None;
                                }
                                else
                                {
                                    dataGrid.ItemsSource = standings ?? (IEnumerable) _nullStandings;

                                    //Yes these hardcoded columns numbers stinks to high heaven, but using Attributes against column properties is expensive when retrieving using reflection
                                    var primaryColumns = new List<int> {0, 2, 3, 4, 5, 6, 7, 8, 9};
                                    var homeColumns = new List<int> { 10, 11, 12, 13, 14, 15, 16};
                                    var awayColumns = new List<int> { 17, 18, 19, 20, 21, 22, 23};
                                    var rightAlignColumns = primaryColumns.Union(homeColumns).Union(awayColumns);

                                    WpfHelper.FormatDataGridColumns(dataGrid.Columns, rightAlignColumns, _rightAlignStyle);
                                    WpfHelper.FormatDataGridColumns(dataGrid.Columns, homeColumns, _homeStyle);
                                    WpfHelper.FormatDataGridColumns(dataGrid.Columns, awayColumns, _awayStyle);

                                    WpfHelper.FormatDataGridHeader(dataGrid.Columns, homeColumns, _homeStyle);
                                    WpfHelper.FormatDataGridHeader(dataGrid.Columns, awayColumns, _awayStyle);
                                }
                            }

                            break;
                        case GridType.Result:
                            var results = await GetFixturePastsAsync(externalLeagueCode); //wont run til web service call finished
                            var resultsList = results.ToList();
                            if (resultsList.Any())
                            {
                                if (resultsList.Any(x => x.HomeName != null && x.HomeName.StartsWith(RequestLimitReached)))
                                {
                                    dataGrid.ItemsSource = new List<NullReturn> { new NullReturn { Error = resultsList.First(x => x.HomeName.StartsWith(RequestLimitReached)).HomeName.Replace(RequestLimitReached, PoliteRequestLimitReached) } };
                                    dataGrid.HeadersVisibility = DataGridHeadersVisibility.None;
                                }
                                else
                                {
                                    if (resultsList.Any(x => x.HomeName != null && x.HomeName.StartsWith(EntityConstants.PotentialTimeout)))
                                    {
                                        dataGrid.ItemsSource = new List<NullReturn> { new NullReturn { Error = EntityConstants.PotentialTimeout } };
                                        dataGrid.HeadersVisibility = DataGridHeadersVisibility.None;
                                    }
                                    else
                                    {
                                        dataGrid.ItemsSource = resultsList;
                                        WpfHelper.FormatDataGridColumns(dataGrid.Columns, new List<int> { 0, 2 }, _rightAlignStyle);
                                    }
                                }
                            }
                            else
                            {
                                dataGrid.ItemsSource = _zeroFixturePasts;
                            }
                            break;
                        case GridType.Fixture:
                            var fixtures = await GetFixtureFuturesAsync(externalLeagueCode); //wont run til web service call has finished
                            var fixturesList = fixtures.ToList();
                            if (fixturesList.Any())
                            {
                                if (fixturesList.Any(x => x.HomeName != null && x.HomeName.StartsWith(RequestLimitReached)))
                                {
                                    dataGrid.ItemsSource = new List<NullReturn> { new NullReturn { Error = fixturesList.First(x => x.HomeName.StartsWith(RequestLimitReached)).HomeName.Replace(RequestLimitReached, PoliteRequestLimitReached) } };
                                    dataGrid.HeadersVisibility = DataGridHeadersVisibility.None;
                                }
                                else
                                {
                                    if (fixturesList.Any(x => x.HomeName != null && x.HomeName.StartsWith(EntityConstants.PotentialTimeout)))
                                    {
                                        dataGrid.ItemsSource = new List<NullReturn> { new NullReturn { Error = EntityConstants.PotentialTimeout } };
                                        dataGrid.HeadersVisibility = DataGridHeadersVisibility.None;
                                    }
                                    else
                                    {
                                        dataGrid.ItemsSource = fixturesList;
                                        WpfHelper.FormatDataGridColumns(dataGrid.Columns, new List<int> { 0, 1 }, _rightAlignStyle);
                                    }
                                }
                            }
                            else
                            {
                                dataGrid.ItemsSource = _zeroFixtureFutures;
                            }
                            break;
                    }

                    DataGridHelper.HideHeaderIfNoDataToShow(dataGrid);
                    UpdateLastUpdatedDate(null);
                }
                catch (Exception ex)
                {
                    var errorText = $"Internal error loading data ERR0542 {internalLeagueCode} {gridType}";
                    Logger.Log($"{errorText} {ex.Message}");
                    dataGrid.ItemsSource = new List<NullReturn> { new NullReturn { Error = errorText } };
                }
            }
        }

        private async Task<IEnumerable<Standing>> GetStandingsAsync(ExternalLeagueCode externalLeagueCode)
        {
            try
            {
                var theTask = Task.Run(() =>
                {                 
                    var gateway = GetFootieDataGateway();
                    var result = gateway.GetFromClientStandings(externalLeagueCode.ToString());
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

        private async Task<IEnumerable<FixturePast>> GetFixturePastsAsync(ExternalLeagueCode externalLeagueCode)
        {
            try
            {
                var theTask = Task.Run(() =>
                {
                    var gateway = GetFootieDataGateway();
                    var result = gateway.GetFromClientFixturePasts(externalLeagueCode.ToString(), $"p{CommonConstants.DaysCount}");                    
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

        private async Task<IEnumerable<FixtureFuture>> GetFixtureFuturesAsync(ExternalLeagueCode externalLeagueCode)
        {
            try
            {
                var theTask = Task.Run(() =>
                {
                    var gateway = GetFootieDataGateway();
                    var result = gateway.GetFromClientFixtureFutures(externalLeagueCode.ToString(), $"n{CommonConstants.DaysCount}");
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

        private void Click_HandlerBossComing(object sender, RoutedEventArgs e)
        {
            TextBlockBossMode.Text = CommonConstants.TheBossIsCommingText;
            BtnLeagueMode.Visibility = Visibility.Collapsed;
            StackPanelLeagueMode.Visibility = Visibility.Collapsed;
            BtnBossMode.Visibility = Visibility.Visible;
            StackPanelBossMode.Visibility = Visibility.Visible;
            BtnRefresh.IsEnabled = false;
            BossModeEventHandler?.Invoke();
        }

        private void Click_HandlerReturn(object sender, RoutedEventArgs e)
        {
            BtnLeagueMode.Visibility = Visibility.Visible;
            StackPanelLeagueMode.Visibility = Visibility.Visible;
            BtnBossMode.Visibility = Visibility.Collapsed;
            StackPanelBossMode.Visibility = Visibility.Collapsed;
            BtnRefresh.IsEnabled = true;
            LeagueModeEventHandler?.Invoke();
        }

        private async void Click_HandlerRefresh(object sender, RoutedEventArgs e)
        {
            var lastUpdatedDate = GetLastUpdatedDate(null);
            var updatedWithinLastXSeconds = DataGridHelper.UpdatedWithinLastXSeconds(lastUpdatedDate, CommonConstants.RefreshIntervalInSeconds, DateTime.Now);

            if (updatedWithinLastXSeconds)
            {
                var pleaseWaitTime = WpfHelper.GetPleaseWaitTime(lastUpdatedDate, DateTime.Now, CommonConstants.RefreshIntervalInSeconds);
                var refreshPostoned = $"Data last updated within last {CommonConstants.RefreshIntervalInSeconds} seconds, please re-try in {pleaseWaitTime} seconds.";
                TextBlockRefreshPostponed.Text = refreshPostoned;
                TextBlockRefreshPostponed.Visibility = Visibility.Visible;
            }
            else
            {
                TextBlockRefreshPostponed.Visibility = Visibility.Collapsed;
                PopulateUi(true);
                ////////////////////////////////////////////////////////////////////////////DoStuff(true);
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////public async Task DoStuff(bool retainExpandCollapseState)
        //////////////////////////////////////////////////////////////////////////////////////////{
        //////////////////////////////////////////////////////////////////////////////////////////    await Task.Run(() =>
        //////////////////////////////////////////////////////////////////////////////////////////    {
        //////////////////////////////////////////////////////////////////////////////////////////        LongRunningOperation(retainExpandCollapseState);
        //////////////////////////////////////////////////////////////////////////////////////////    });
        //////////////////////////////////////////////////////////////////////////////////////////}

        //////////////////////////////////////////////////////////////////////////////////////////private async Task LongRunningOperation(bool retainExpandCollapseState)
        //////////////////////////////////////////////////////////////////////////////////////////{
        //////////////////////////////////////////////////////////////////////////////////////////    //int counter;
        //////////////////////////////////////////////////////////////////////////////////////////    //for (counter = 0; counter < 50000; counter++)
        //////////////////////////////////////////////////////////////////////////////////////////    //{
        //////////////////////////////////////////////////////////////////////////////////////////    //    Console.WriteLine(counter);
        //////////////////////////////////////////////////////////////////////////////////////////    //}
        //////////////////////////////////////////////////////////////////////////////////////////    //return "Counter = " + counter;

        //////////////////////////////////////////////////////////////////////////////////////////    try
        //////////////////////////////////////////////////////////////////////////////////////////    {
        //////////////////////////////////////////////////////////////////////////////////////////        PopulateUi(retainExpandCollapseState);
        //////////////////////////////////////////////////////////////////////////////////////////    }
        //////////////////////////////////////////////////////////////////////////////////////////    catch (Exception ex)
        //////////////////////////////////////////////////////////////////////////////////////////    {
        //////////////////////////////////////////////////////////////////////////////////////////        //Due to high risk of deadlock you cannot call GetService
        //////////////////////////////////////////////////////////////////////////////////////////        //from a background thread in an AsyncPackage derived class. 
        //////////////////////////////////////////////////////////////////////////////////////////        //You should instead call GetServiceAsync(without calling 
        //////////////////////////////////////////////////////////////////////////////////////////        //Result or Wait on the resultant Task object) or switch 
        //////////////////////////////////////////////////////////////////////////////////////////        //to the UI thread with the JoinableTaskFactory.SwitchToMainThreadAsync 
        //////////////////////////////////////////////////////////////////////////////////////////        //method before calling GetService.
        //////////////////////////////////////////////////////////////////////////////////////////        throw;
        //////////////////////////////////////////////////////////////////////////////////////////    }
        //////////////////////////////////////////////////////////////////////////////////////////}

        private void PopulateUi(bool retainExpandCollapseState)
        {
            GetOptionsFromStoreAndMapToInternalFormatMethod(null);

            RetainExpandCollapseState(retainExpandCollapseState);

            StackPanelLeagueMode.Children.RemoveRange(0, StackPanelLeagueMode.Children.Count);

            foreach (var leagueOption in LeagueGeneralOptions.LeagueOptions)
            {
                if (leagueOption.ShowLeague)
                {
                    foreach (var leagueSubOption in leagueOption.LeagueSubOptions)
                    {
                        var expander = new Expander();

                        PrepareExpander(expander, leagueOption.InternalLeagueCode, leagueSubOption.GridType);

                        if (leagueSubOption.Expand)
                        {
                            expander.IsExpanded = true;
                            var dataGrid = GetMyDataGrid(leagueOption.InternalLeagueCode, leagueSubOption.GridType);
                            expander.Content = dataGrid;
                        }

                        StackPanelLeagueMode.Children.Add(expander);
                    }                                        
                }
            }
        }

        private void RetainExpandCollapseState(bool retainExpandCollapseState)
        {
            if (retainExpandCollapseState)
            {
                foreach (var leagueOption in LeagueGeneralOptions.LeagueOptions)
                {
                    foreach (var leagueSubOption in leagueOption.LeagueSubOptions)
                    {
                        foreach (Expander child in StackPanelLeagueMode.Children)
                        {
                            //UK1_Standing
                            if (child.Name.StartsWith(leagueOption.InternalLeagueCode.ToString()) &&
                                child.Name.EndsWith(leagueSubOption.GridType.ToString()))
                            {
                                leagueSubOption.Expand = child.IsExpanded;
                            }
                        }
                    }
                }
            }
        }

        private void PrepareExpander(Expander expander, InternalLeagueCode internalLeagueCode, GridType gridType)
        {
            expander.Name = internalLeagueCode + "_" + gridType;
            expander.Visibility = Visibility.Visible;
            expander.Style = (Style)TryFindResource("PlusMinusExpander");

            var headerPrefix = WpfHelper.GetHeaderPrefix(internalLeagueCode);
            var headerSuffix = WpfHelper.GetHeaderSuffix(gridType);
            expander.Header = $"{headerPrefix} {headerSuffix}";

            expander.Expanded += ExpanderAny_OnExpanded;
        }

        private MyDataGrid GetMyDataGrid(InternalLeagueCode internalLeagueCode, GridType gridType)
        {
            var dataGrid = new MyDataGrid
            {
                Name = internalLeagueCode + gridType.ToString(),
                Visibility = Visibility.Visible,
            };

            if (_competitionResultSingletonInstance == null)
            {
                dataGrid.ItemsSource = _nullStandings;
            }
            else
            {
                DataGridLoadedAsync(dataGrid, internalLeagueCode, gridType);
            }

            return dataGrid;
        }
    }
}