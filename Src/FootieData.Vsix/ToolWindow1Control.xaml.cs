using FootieData.Common;
using FootieData.Common.Options;
using FootieData.Entities;
using FootieData.Entities.ReferenceData;
using FootieData.Gateway;
using FootieData.Vsix.Providers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
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

        #region Private members
        private CompetitionResultSingleton _competitionResultSingletonInstance;
        private LeagueDtosSingleton _leagueDtosSingletonInstance;

        private readonly IEnumerable<NullReturn> _nullStandings = new List<NullReturn> { new NullReturn { PoliteError = $"League table {Unavailable}" } };
        private readonly IEnumerable<NullReturn> _nullFixturePasts = new List<NullReturn> { new NullReturn { PoliteError = $"League results {Unavailable}" } };
        private readonly IEnumerable<NullReturn> _nullFixtureFutures = new List<NullReturn> { new NullReturn { PoliteError = $"League fixtures {Unavailable}" } };
        private const string Unavailable = "unavailable at this time - please try again later";

        private static Func<string, DateTime> GetLastUpdatedDate { get; set; }
        private static Action<string> GetOptionsFromStoreAndMapToInternalFormatMethod { get; set; }
        private static Action<string> UpdateLastUpdatedDate { get; set; }
        #endregion

        public ToolWindow1Control(Action<string> getOptionsFromStoreAndMapToInternalFormatMethod, Action<string> updateLastUpdatedDate, Func<string, DateTime> getLastUpdatedDate)
        {
            try
            {
                //throw new Exception(); //for debugging

                //Debug.WriteLine("Worker thread: " + Thread.CurrentThread.ManagedThreadId);
                InitializeComponent();
                TextBlockUnexpectedErrorOccuredActivityLogin.Visibility = Visibility.Hidden;

                try
                {
                    _competitionResultSingletonInstance = CompetitionResultSingleton.Instance;
                    //throw new Exception(); //for debugging
                }
                catch (Exception)
                {
                    //Do nothing - the resultant null _competitionResultSingletonInstance is handled further down the call stack
                }

                GetLastUpdatedDate = getLastUpdatedDate;
                GetOptionsFromStoreAndMapToInternalFormatMethod = getOptionsFromStoreAndMapToInternalFormatMethod;
                UpdateLastUpdatedDate = updateLastUpdatedDate;
                _leagueDtosSingletonInstance = LeagueDtosSingleton.Instance;

                PopulateUi(false);

                //throw new Exception(); //for debugging
            }
            catch (Exception ex)
            {
                TextBlockUnexpectedErrorOccuredActivityLogin.Text = EntityConstants.UnexpectedErrorOccuredActivityLog;
                TextBlockUnexpectedErrorOccuredActivityLogin.Visibility = Visibility.Visible;
            }
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
                    DataGridLoaded(dataGrid, internalLeagueCode, gridType);//TODO seems to never get invoked, consider removal
                }
                else
                {
                    //Called here so that when the results/future fixtures is expanded (for very first time) we get the data for it
                    dataGrid = GetMyDataGrid(internalLeagueCode, gridType);
                    expander.Content = dataGrid;
                }               
            }
            else
            {
                Logger.Log("Internal error 1002 - sender is not Expander");
            }
        }

        private void DataGridLoaded(DataGrid dataGrid, InternalLeagueCode internalLeagueCode, GridType gridType)
        {
            try
            {
                var externalLeagueCode = _leagueDtosSingletonInstance.LeagueDtos.Single(x => x.InternalLeagueCode == internalLeagueCode).ExternalLeagueCode;
                //throw new Exception();//for debugging

                var dataGridEmpty = DataGridHelper.IsDataGridEmpty(dataGrid);

                if (dataGridEmpty)
                {
                    //throw new Exception();//for debugging
                    ThreadedDataProvider threadedDataProvider;

                    switch (gridType)
                    {
                        case GridType.Standing:
                            threadedDataProvider = new ThreadedDataProvider(externalLeagueCode);
                            this.DataContext = threadedDataProvider;
                            threadedDataProvider.FetchDataFromGateway(externalLeagueCode, GridType.Standing);
                            dataGrid.ItemsSource = threadedDataProvider.LeagueParents.Single(x => x.ExternalLeagueCode == externalLeagueCode).Standings ?? (IEnumerable)_nullStandings;
                            break;
                        case GridType.Result:
                            threadedDataProvider = new ThreadedDataProvider(externalLeagueCode);
                            this.DataContext = threadedDataProvider;
                            threadedDataProvider.FetchDataFromGateway(externalLeagueCode, GridType.Result);
                            dataGrid.ItemsSource = threadedDataProvider.LeagueParents.Single(x => x.ExternalLeagueCode == externalLeagueCode).FixturePasts ?? (IEnumerable)_nullFixturePasts;
                            break;
                        case GridType.Fixture:
                            threadedDataProvider = new ThreadedDataProvider(externalLeagueCode);
                            this.DataContext = threadedDataProvider;
                            threadedDataProvider.FetchDataFromGateway(externalLeagueCode, GridType.Fixture);
                            dataGrid.ItemsSource = threadedDataProvider.LeagueParents.Single(x => x.ExternalLeagueCode == externalLeagueCode).FixtureFutures ?? (IEnumerable)_nullFixtureFutures;
                            break;
                    }

                    DataGridHelper.HideHeaderIfNoDataToShow(dataGrid);
                    UpdateLastUpdatedDate(null);
                }
            }
            catch (Exception ex)
            {
                var errorText = $"{EntityConstants.UnexpectedErrorOccured} ({internalLeagueCode}_{gridType})";
                Logger.Log($"{errorText} {ex.Message}");
                dataGrid.ItemsSource = new List<NullReturn> { new NullReturn { PoliteError = errorText } };
            }
        }

        private void Click_HandlerBossComing(object sender, RoutedEventArgs e)
        {
            BtnBossMode.Visibility = Visibility.Visible;
            BtnLeagueMode.Visibility = Visibility.Collapsed;
            StackPanelBossMode.Visibility = Visibility.Visible;
            StackPanelLeagueMode.Visibility = Visibility.Collapsed;
            TextBlockBossMode.Text = CommonConstants.TheBossIsCommingText;
            BtnRefresh.IsEnabled = false;
            BossModeEventHandler?.Invoke();
        }

        private void Click_HandlerReturn(object sender, RoutedEventArgs e)
        {
            BtnBossMode.Visibility = Visibility.Collapsed;
            BtnLeagueMode.Visibility = Visibility.Visible;
            BtnRefresh.IsEnabled = true;
            StackPanelBossMode.Visibility = Visibility.Collapsed;
            StackPanelLeagueMode.Visibility = Visibility.Visible;
            LeagueModeEventHandler?.Invoke();
        }

        private void Click_HandlerRefresh(object sender, RoutedEventArgs e)
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
            }
        }

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
                DataGridLoaded(dataGrid, internalLeagueCode, gridType);
            }

            return dataGrid;
        }
    }
}

