using FootieData.Common;
using FootieData.Entities.ReferenceData;
using FootieData.Entities;
using FootieData.Gateway;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System;
using FootieData.Common.Options;

namespace FootieData.Vsix
{
    public partial class ToolWindow1Control : UserControl
    {
        private int expanderHeight = 150;
        private static WpfHelper _wpfHelper;
        public static GeneralOptions2 GeneralOptions2 { get; set; }
        private readonly LeagueDtosSingleton _leagueDtosSingletonInstance;
        private readonly SolidColorBrush _colorRefreshed;
        private readonly SolidColorBrush _colorDataGridExpanded;
        private readonly CompetitionResultSingleton _competitionResultSingletonInstance;
        private readonly IEnumerable<NullReturn> _nullStandings = new List<NullReturn> { new NullReturn { Error = $"League table {Unavailable}" } };
        private readonly IEnumerable<NullReturn> _nullFixturePasts = new List<NullReturn> { new NullReturn { Error = $"Results {Unavailable}" } };
        private readonly IEnumerable<NullReturn> _nullFixtureFutures = new List<NullReturn> { new NullReturn { Error = $"Fixtures {Unavailable}" } };
        private const string Unavailable = "unavailable at this time - try again later";
        private readonly Style _rightAlignStyle;

        private static Func<string, string> _getOptionsFromStoreAndMapToInternalFormatMethod { get; set; }

        public ToolWindow1Control(Func<string, string> getOptionsFromStoreAndMapToInternalFormatMethod)
        {
            InitializeComponent();

            _colorRefreshed = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00FF00"));
            _colorDataGridExpanded = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF000"));
            _competitionResultSingletonInstance = CompetitionResultSingleton.Instance;
            _getOptionsFromStoreAndMapToInternalFormatMethod = getOptionsFromStoreAndMapToInternalFormatMethod;
            _leagueDtosSingletonInstance = LeagueDtosSingleton.Instance;
            _rightAlignStyle = new Style();
            _rightAlignStyle.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right));
            _wpfHelper = new WpfHelper();            

            _getOptionsFromStoreAndMapToInternalFormatMethod("not needed - change Func to Action");       
        }
        
        private FootieDataGateway GetFootieDataGateway()
        {
            return new FootieDataGateway(_competitionResultSingletonInstance);
        }

        private void ExpanderLoaded_Any(object sender, RoutedEventArgs e)
        {
            if (sender is Expander expander)
            {
                var internalLeagueCode = WpfHelper.GetInternalLeagueCode(_wpfHelper, expander.Name);
                var shouldShowLeague = DataGridHelper.ShouldShowLeague(GeneralOptions2.LeagueOptions, internalLeagueCode);
                if (shouldShowLeague)
                {
                    expander.Visibility = Visibility.Visible;
                    expander.Style = GetExpanderStandingStyle();
                }
                else
                {
                    HideExpander(expander);
                }
            }
            else
            {
                Logger.Log("Internal error 1001 - sender is not Expander");
            }
        }

        private void DataGridLoaded_Any(object sender, RoutedEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                DataGridLoadedAsync(dataGrid, false);
            }
        }

        private async void DataGridLoadedAsync(DataGrid dataGrid, bool manuallyExpanded)
        {
            if (dataGrid.Parent is Expander parentExpander)
            {
                var gridType = _wpfHelper.GetGridType(dataGrid.Name);
                var parentExpanderName = parentExpander.Name;
                var internalLeagueCode = WpfHelper.GetInternalLeagueCode(_wpfHelper, parentExpanderName);
                var shouldShowLeague = DataGridHelper.ShouldShowLeague(GeneralOptions2.LeagueOptions, internalLeagueCode);
                parentExpander.Header = LeagueMapping.LeagueDtos.First(x => x.InternalLeagueCode == internalLeagueCode).InternalLeagueCodeDescription + " " + WpfHelper.GetDescription(gridType);
                var internalToExternalMappingExists = _leagueDtosSingletonInstance.LeagueDtos.Any(x => x.InternalLeagueCode == internalLeagueCode);
                var externalLeagueCode = _leagueDtosSingletonInstance.LeagueDtos
                    .Single(x => x.InternalLeagueCode == internalLeagueCode).ExternalLeagueCode;
                var shouldExpandGrid = DataGridHelper.ShouldExpandGrid(GeneralOptions2.LeagueOptions, internalLeagueCode, gridType);

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
                                    /////////////var standings = GetStandingsAsync(shouldShowLeague, internalToExternalMappingExists, externalLeagueCode);
                                    dataGrid.ItemsSource = standings ?? (IEnumerable)_nullStandings;
                                    WpfHelper.RightAlignDataGridColumns(dataGrid.Columns, new List<int> { 0, 2, 3, 4, 5, 6 }, _rightAlignStyle);
                                    break;
                                case GridType.Result:
                                    var results = await GetFixturePastsAsync(shouldShowLeague, internalToExternalMappingExists, externalLeagueCode); //wont run til web service call finished
                                    /////////////var results = GetFixturePastsAsync(shouldShowLeague, internalToExternalMappingExists, externalLeagueCode);
                                    dataGrid.ItemsSource = results ?? (IEnumerable)_nullFixturePasts;
                                    WpfHelper.RightAlignDataGridColumns(dataGrid.Columns, new List<int> { 0, 2 }, _rightAlignStyle);
                                    break;
                                case GridType.Fixture:
                                    var fixtures = await GetFixtureFuturesAsync(shouldShowLeague, internalToExternalMappingExists, externalLeagueCode); //wont run til web service call has finished
                                    /////////////var fixtures = GetFixtureFuturesAsync(shouldShowLeague, internalToExternalMappingExists, externalLeagueCode);
                                    dataGrid.ItemsSource = fixtures ?? (IEnumerable)_nullFixtureFutures;
                                    WpfHelper.RightAlignDataGridColumns(dataGrid.Columns, new List<int> { 0, 1 }, _rightAlignStyle);
                                    break;
                            }

                            DataGridHelper.HideHeaderIfNoDataToShow(dataGrid);
                        }
                        catch (Exception ex)
                        {
                            parentExpander.Header = "DataGridLoaded_Any internal error " + gridType;
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
                        var gateway = GetFootieDataGateway();
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
                        var gateway = GetFootieDataGateway();
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
                        var gateway = GetFootieDataGateway();
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

        ////////////////private IEnumerable<Standing> GetStandingsAsync(bool shouldShowLeague, bool internalToExternalMappingExists, ExternalLeagueCode externalLeagueCode)
        ////////////////{
        ////////////////    IEnumerable<Standing> result = null;
        ////////////////    if (shouldShowLeague && internalToExternalMappingExists)
        ////////////////    {
        ////////////////        var gateway = GetFootieDataGateway();
        ////////////////        result = gateway.GetFromClientStandings(externalLeagueCode.ToString());
        ////////////////    }
        ////////////////    return result;
        ////////////////}
        ////////////////private IEnumerable<FixturePast> GetFixturePastsAsync(bool shouldShowLeague, bool internalToExternalMappingExists, ExternalLeagueCode externalLeagueCode)
        ////////////////{
        ////////////////    IEnumerable<FixturePast> result = null;
        ////////////////    if (shouldShowLeague && internalToExternalMappingExists)
        ////////////////    {
        ////////////////        var gateway = GetFootieDataGateway();
        ////////////////        result = gateway.GetFromClientFixturePasts(externalLeagueCode.ToString(), "p7");
        ////////////////    }
        ////////////////    return result;
        ////////////////}
        ////////////////private IEnumerable<FixtureFuture> GetFixtureFuturesAsync(bool shouldShowLeague, bool internalToExternalMappingExists, ExternalLeagueCode externalLeagueCode)
        ////////////////{
        ////////////////    IEnumerable<FixtureFuture> result = null;
        ////////////////    if (shouldShowLeague && internalToExternalMappingExists)
        ////////////////    {
        ////////////////        var gateway = GetFootieDataGateway();
        ////////////////        result = gateway.GetFromClientFixtureFutures(externalLeagueCode.ToString(), "n7");
        ////////////////    }
        ////////////////    return result;         
        ////////////////}

        private void Click_Handler1(object sender, RoutedEventArgs e)
        {
            TextBlockBossMode.Text = CommonConstants.TheBossIsCommingText;
            BtnLeagueMode.Visibility = Visibility.Collapsed;
            StackPanelLeagueMode.Visibility = Visibility.Collapsed;
            BtnBossMode.Visibility = Visibility.Visible;
            StackPanelBossMode.Visibility = Visibility.Visible;
        }

        private void Click_Handler2(object sender, RoutedEventArgs e)
        {
            BtnLeagueMode.Visibility = Visibility.Visible;
            StackPanelLeagueMode.Visibility = Visibility.Visible;
            BtnBossMode.Visibility = Visibility.Collapsed;
            StackPanelBossMode.Visibility = Visibility.Collapsed;
        }

        private void ExpanderAny_OnExpanded(object sender, RoutedEventArgs e)
        {
            if (sender is Expander expander)
            {
                if (expander.Content is DataGrid dataGrid)
                {
                    var getDataFromClient = DataGridHelper.GetDataFromClient(dataGrid);

                    if (getDataFromClient)
                    {
                        DataGridLoadedAsync(dataGrid, true);
                        dataGrid.AlternatingRowBackground = _colorDataGridExpanded;
                    }
                }
            }
            else
            {
                Logger.Log("Internal error 1002 - sender is not Expander");
            }
        }

        private void Click_HardcodedHandlerRefresh(object sender, RoutedEventArgs e)
        {
            _getOptionsFromStoreAndMapToInternalFormatMethod("not needed - change Func to Action");

            foreach (var leagueOption in GeneralOptions2.LeagueOptions)
            {
                var expanderStanding = DataGridHelper.FindChild<Expander>(Application.Current.MainWindow, leagueOption.InternalLeagueCode + "_Rand0001");
                var expanderResult = DataGridHelper.FindChild<Expander>(Application.Current.MainWindow, leagueOption.InternalLeagueCode + "_Rand0002");
                var expanderFixture = DataGridHelper.FindChild<Expander>(Application.Current.MainWindow, leagueOption.InternalLeagueCode + "_Rand0003");

                if (leagueOption.ShowLeague)
                {                    
                    PrepareExpander(expanderStanding);
                    PrepareExpander(expanderResult);
                    PrepareExpander(expanderFixture);
                    
                    expanderStanding.IsExpanded = true;

                    var dataGridStanding = GetDataGrid(leagueOption, "Standing_");
                    var dataGridResult = GetDataGrid(leagueOption, "Results1_");
                    var dataGridFixture = GetDataGrid(leagueOption, "Fixtures_");

                    if (dataGridStanding == null)
                    {
                        dataGridStanding = GetMyDataGrid(leagueOption, "Standing_");
                    }
                    if (dataGridResult == null)
                    {
                        dataGridResult = GetMyDataGrid(leagueOption, "Results1_");
                    }
                    if (dataGridFixture == null)
                    {
                        dataGridFixture = GetMyDataGrid(leagueOption, "Fixtures_");
                    }

                    PrepareDataGrid(dataGridStanding);
                    PrepareDataGrid(dataGridResult);
                    PrepareDataGrid(dataGridFixture);
                 
                    //ADD DATAGRID TO EXPANDER
                    expanderStanding.Content = dataGridStanding;
                    expanderResult.Content = dataGridResult;
                    expanderFixture.Content = dataGridFixture;
                }
                else
                {
                    if (expanderStanding != null)
                    {
                        HideExpander(expanderStanding);
                    }
                    if (expanderResult != null)
                    {
                        HideExpander(expanderResult);
                    }
                    if (expanderFixture != null)
                    {
                        HideExpander(expanderFixture);
                    }
                }
            }
        }

        private void PrepareExpander(Expander expander)
        {
            expander.Visibility = Visibility.Visible;
            expander.Style = GetExpanderStandingStyle();            
            expander.Height = expanderHeight;
        }

        private void PrepareDataGrid(DataGrid dataGrid)
        {
            SetDataGridLoadedEventHandler(dataGrid);
            DataGridLoaded_Any(dataGrid, null);
            SetAlternatingRowBackground(dataGrid);
        }

        private void SetDataGridLoadedEventHandler(DataGrid dataGrid)
        {
            dataGrid.Loaded += DataGridLoaded_Any;
        }

        private void SetAlternatingRowBackground(DataGrid dataGrid)
        {
            dataGrid.AlternatingRowBackground = _colorRefreshed;
        }

        private static void HideExpander(Expander expander)
        {
            expander.Visibility = Visibility.Hidden;
            expander.Height = 0;
        }

        private static MyDataGrid GetMyDataGrid(LeagueOption leagueOption, string dataGridPrefix)
        {
            var dataGridStanding = new MyDataGrid
            {
                Height = 250,
                Name = dataGridPrefix + leagueOption.InternalLeagueCode,
                Visibility = Visibility.Visible,
            };
            return dataGridStanding;
        }

        private static MyDataGrid GetDataGrid(LeagueOption leagueOption, string dataGridPrefix)
        {
            var dataGridStanding = DataGridHelper.FindChild<MyDataGrid>(Application.Current.MainWindow, dataGridPrefix + leagueOption.InternalLeagueCode);
            return dataGridStanding;
        }

        private Style GetExpanderStandingStyle()
        {
            return (Style)TryFindResource("PlusMinusExpander");
        }
    }
}
