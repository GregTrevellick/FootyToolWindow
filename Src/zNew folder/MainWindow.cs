using FootballDataSDK.Services;
using FootieData.Common;
using FootieData.Common.Helpers;
using FootieData.Entities;
using FootieData.Gateway;
using HierarchicalDataTemplate.ReferenceData;
using System;
using System.Collections;
using System.Collections.Generic;
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
        private readonly LeagueCodeMappingsSingleton _leagueCodeMappingsSingletonInstance;
        private readonly SolidColorBrush _colorRefreshed;
        private readonly SolidColorBrush _colorDataGridExpanded;
        private readonly SoccerSeasonResultSingleton _soccerSeasonResultSingletonInstance;
        private readonly IEnumerable<NullReturn> _nullStandings = new List<NullReturn> { new NullReturn { Error = $"League table {Unavailable}" } };
        private readonly IEnumerable<NullReturn> _nullFixturePasts = new List<NullReturn> { new NullReturn { Error = $"Results {Unavailable}" } };
        private readonly IEnumerable<NullReturn> _nullFixtureFutures = new List<NullReturn> { new NullReturn { Error = $"Fixtures {Unavailable}" } };
        private const string Unavailable = "unavailable at this time - try again later";
        
        public MainWindow()
        {
            InitializeComponent();
            _colorRefreshed = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            _colorDataGridExpanded = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF000"));
            _leagueCodeMappingsSingletonInstance = LeagueCodeMappingsSingleton.Instance;
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
                var internalLeagueCode = DataGridHelper.GetInternalLeagueCode(_wpfHelper, expander.Name);
                var shouldShowLeague = DataGridHelper.ShouldShowLeague(_generalOptions.LeagueOptions, internalLeagueCode);
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
        
        private void DataGridLoaded_Any(object sender, RoutedEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                DataGridLoaded_Any2(dataGrid, false);
            }
        }

        private async void DataGridLoaded_Any2(DataGrid dataGrid, bool manuallyExpanded)
        {
            if (dataGrid.Parent is Expander parentExpander)
            {
                var gridType = _wpfHelper.GetGridType(dataGrid.Name);
                var parentExpanderName = parentExpander.Name;
                var internalLeagueCode = DataGridHelper.GetInternalLeagueCode(_wpfHelper, parentExpanderName);
                var shouldShowLeague = DataGridHelper.ShouldShowLeague(_generalOptions.LeagueOptions, internalLeagueCode);
                parentExpander.Header = internalLeagueCode.GetDescription() + " " + gridType.GetDescription();
                var internalToExternalMappingExists = _leagueCodeMappingsSingletonInstance.LeagueCodeMappings.TryGetValue(internalLeagueCode, out var externalLeagueCode);
                var shouldExpandGrid = DataGridHelper.ShouldExpandGrid(_generalOptions.LeagueOptions, internalLeagueCode, gridType);

                if (shouldExpandGrid || manuallyExpanded)
                {
                    parentExpander.IsExpanded = true;

                    var getDataFromClient = DataGridHelper.GetDataFromClient(dataGrid);

                    if (getDataFromClient)
                    {
                        try
                        {
                            switch (gridType)
                            {
                                case GridType.Standing:
                                    var standings = await GetStandingsAsync(shouldShowLeague, internalToExternalMappingExists, externalLeagueCode); //wont run til web service call has finished
                                    dataGrid.ItemsSource = standings ?? (IEnumerable)_nullStandings;
                                    break;
                                case GridType.Result:
                                    var results = await GetFixturePastsAsync(shouldShowLeague, internalToExternalMappingExists, externalLeagueCode); //wont run til web service call finished
                                    dataGrid.ItemsSource = results ?? (IEnumerable)_nullFixturePasts;
                                    break;
                                case GridType.Fixture:
                                    var fixtures = await GetFixtureFuturesAsync(shouldShowLeague, internalToExternalMappingExists, externalLeagueCode); //wont run til web service call has finished
                                    dataGrid.ItemsSource = fixtures ?? (IEnumerable)_nullFixtureFutures;
                                    break;
                            }

                            DataGridHelper.HideHeaderIfNoDataToShow(dataGrid);
                        }
                        catch (Exception)
                        {
                            parentExpander.Header = "DataGridLoaded_Any internal error";
                        }
                    }
                }
            }
        }

        private async Task<IEnumerable<Standing>> GetStandingsAsync(bool shouldShowLeague, bool internalToExternalMappingExists, ExternalLeagueCode externalLeagueCode)
        {
            try
            {
                var theTask = Task.Run(() =>
                {
                    IEnumerable<Standing> result = null;
                    if (shouldShowLeague && internalToExternalMappingExists)
                    {
                        var gateway = GetGateway();
                        result = gateway.GetFromClientStandings(externalLeagueCode.ToString());
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

        private async Task<IEnumerable<FixturePast>> GetFixturePastsAsync(bool shouldShowLeague, bool internalToExternalMappingExists, ExternalLeagueCode externalLeagueCode)
        {
            try
            {
                var theTask = Task.Run(() =>
                {
                    IEnumerable<FixturePast> result = null;
                    if (shouldShowLeague && internalToExternalMappingExists)                        
                    {
                        var gateway = GetGateway();
                        result = gateway.GetFromClientFixturePasts(externalLeagueCode.ToString(), "p7");
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

        private async Task<IEnumerable<FixtureFuture>> GetFixtureFuturesAsync(bool shouldShowLeague, bool internalToExternalMappingExists, ExternalLeagueCode externalLeagueCode)
        {
            try
            {
                var theTask = Task.Run(() =>
                {
                    IEnumerable<FixtureFuture> result = null;
                    if (shouldShowLeague && internalToExternalMappingExists)                        
                    {
                        var gateway = GetGateway();
                        result = gateway.GetFromClientFixtureFutures(externalLeagueCode.ToString(), "n7");
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

        private void Click_HardcodedHandlerRefresh(object sender, RoutedEventArgs e)
        {
            GetOptions();

            var dataGridStanding = DataGridHelper.FindChild<DataGrid>(Application.Current.MainWindow, "Standing_Rand0001");
            if (dataGridStanding != null && dataGridStanding.IsVisible)
            {
                DataGridLoaded_Any(dataGridStanding, null);
                dataGridStanding.AlternatingRowBackground = _colorRefreshed;
            }

            var dataGridResult = DataGridHelper.FindChild<DataGrid>(Application.Current.MainWindow, "Results1_Rand0001");
            if (dataGridResult != null && dataGridResult.IsVisible)
            {
                DataGridLoaded_Any(dataGridResult, null);
                dataGridResult.AlternatingRowBackground = _colorRefreshed;
            }            

            var dataGridFixture = DataGridHelper.FindChild<DataGrid>(Application.Current.MainWindow, "Fixtures_Rand0001");
            if (dataGridFixture != null && dataGridFixture.IsVisible)
            {
                DataGridLoaded_Any(dataGridFixture, null);
                dataGridFixture.AlternatingRowBackground = _colorRefreshed;
            }            
        }

        private void ExpanderAny_OnExpanded(object sender, RoutedEventArgs e)
        {
            if (sender is Expander expander)
            {
                if (expander.Content is DataGrid dataGrid)
                {
                    var gregtTemp = expander.Name;

                    var getDataFromClient = DataGridHelper.GetDataFromClient(dataGrid);

                    if (getDataFromClient)
                    {
                        DataGridLoaded_Any2(dataGrid, true);
                        dataGrid.AlternatingRowBackground = _colorDataGridExpanded;
                    }
                }                             
            }
            else
            {
                Logger.Log("Internal error2 gregt");
            }
        }
    }
}