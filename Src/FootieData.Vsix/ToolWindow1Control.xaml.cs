﻿using FootieData.Common;
using FootieData.Entities.ReferenceData;
using FootieData.Entities;
using FootieData.Gateway;
using FootieData.Vsix.Options;
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
        private static WpfHelper _wpfHelper;
        private static Common.Options.GeneralOptions2 _generalOptions2;
        private readonly LeagueDtosSingleton _leagueDtosSingletonInstance;
        private readonly SolidColorBrush _colorRefreshed;
        private readonly SolidColorBrush _colorDataGridExpanded;
        private readonly CompetitionResultSingleton _competitionResultSingletonInstance;
        private readonly IEnumerable<NullReturn> _nullStandings = new List<NullReturn> { new NullReturn { Error = $"League table {Unavailable}" } };
        private readonly IEnumerable<NullReturn> _nullFixturePasts = new List<NullReturn> { new NullReturn { Error = $"Results {Unavailable}" } };
        private readonly IEnumerable<NullReturn> _nullFixtureFutures = new List<NullReturn> { new NullReturn { Error = $"Fixtures {Unavailable}" } };
        private const string Unavailable = "unavailable at this time - try again later";
        private readonly Style _rightAlignStyle;

        public ToolWindow1Control(GeneralOptions generalOptions)
        {
            InitializeComponent();
            _colorRefreshed = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFF00"));
            _colorDataGridExpanded = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF000"));
            _competitionResultSingletonInstance = CompetitionResultSingleton.Instance;
            _leagueDtosSingletonInstance = LeagueDtosSingleton.Instance;
            _rightAlignStyle = new Style();
            _rightAlignStyle.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right));
            _wpfHelper = new WpfHelper();
            GetOptions(generalOptions);
        }

        private void GetOptions(GeneralOptions generalOptions)
        {
            _generalOptions2 = new GeneralOptions2
            {
                LeagueOptions = new List<LeagueOption>
                {
                    GetLeagueOption(generalOptions.InterestedInBr1, InternalLeagueCode.BR1),
                    GetLeagueOption(generalOptions.InterestedInDe1, InternalLeagueCode.DE1),
                    GetLeagueOption(generalOptions.InterestedInDe2, InternalLeagueCode.DE2),
                    GetLeagueOption(generalOptions.InterestedInEs1, InternalLeagueCode.ES1),
                    GetLeagueOption(generalOptions.InterestedInFr1, InternalLeagueCode.FR1),
                    GetLeagueOption(generalOptions.InterestedInFr2, InternalLeagueCode.FR2),
                    GetLeagueOption(generalOptions.InterestedInIt1, InternalLeagueCode.IT1),
                    GetLeagueOption(generalOptions.InterestedInIt2, InternalLeagueCode.IT2),
                    GetLeagueOption(generalOptions.InterestedInNl1, InternalLeagueCode.NL1),
                    GetLeagueOption(generalOptions.InterestedInPt1, InternalLeagueCode.PT1),
                    GetLeagueOption(generalOptions.InterestedInUefa1, InternalLeagueCode.UEFA1),
                    GetLeagueOption(generalOptions.InterestedInUk1, InternalLeagueCode.UK1),
                    GetLeagueOption(generalOptions.InterestedInUk2, InternalLeagueCode.UK2),
                    GetLeagueOption(generalOptions.InterestedInUk3, InternalLeagueCode.UK3),
                    GetLeagueOption(generalOptions.InterestedInUk4, InternalLeagueCode.UK4),                
                }
            };
        }

        private static LeagueOption GetLeagueOption(bool interestedIn, InternalLeagueCode internalLeagueCode)
        {
            return new LeagueOption
            {
                InternalLeagueCode = internalLeagueCode,
                ShowLeague = interestedIn,
                LeagueSubOptions = GetLeagueSubOptions()                           
            };
        }

        private static List<LeagueSubOption> GetLeagueSubOptions()
        {
            return new List<LeagueSubOption>
            {
                new LeagueSubOption
                {
                    GridType = GridType.Standing,
                    Expand = true
                },
                new LeagueSubOption
                {
                    GridType = GridType.Result,
                    Expand = false
                },
                new LeagueSubOption
                {
                    GridType = GridType.Fixture,
                    Expand = false
                }
            };
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
                var shouldShowLeague = DataGridHelper.ShouldShowLeague(_generalOptions2.LeagueOptions, internalLeagueCode);
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
                var shouldShowLeague = DataGridHelper.ShouldShowLeague(_generalOptions2.LeagueOptions, internalLeagueCode);
                parentExpander.Header = LeagueMapping.LeagueDtos.First(x => x.InternalLeagueCode == internalLeagueCode).InternalLeagueCodeDescription + " " + WpfHelper.GetDescription(gridType);
                var internalToExternalMappingExists = _leagueDtosSingletonInstance.LeagueDtos.Any(x => x.InternalLeagueCode == internalLeagueCode);
                var externalLeagueCode = _leagueDtosSingletonInstance.LeagueDtos
                    .Single(x => x.InternalLeagueCode == internalLeagueCode).ExternalLeagueCode;
                var shouldExpandGrid = DataGridHelper.ShouldExpandGrid(_generalOptions2.LeagueOptions, internalLeagueCode, gridType);

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

        private void Click_HardcodedHandlerRefresh(object sender, RoutedEventArgs e)
        {
            //gregt GetOptions();

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


        //private void DataGridLoaded_BL1(object sender, RoutedEventArgs e)
        //{
        //    GetLeagueData(sender, "BL1");
        //}

        //private void DataGridLoaded_BL2(object sender, RoutedEventArgs e)
        //{
        //    GetLeagueData(sender, "BL2");
        //}

        //private void DataGridLoaded_PL(object sender, RoutedEventArgs e)
        //{
        //    GetLeagueData(sender, "PL");
        //}

        //private void GetLeagueData(object sender, string leagueIdentifier)
        //{
        //    var leagueResponse = _gateway.GetFromClientStandings(leagueIdentifier);

        //    var grid = sender as DataGrid;
        //    //grid.ItemsSource = leagueResponse.Result.Standings;
        //    grid.ItemsSource = leagueResponse;//.Standings;
        //    grid.GridLinesVisibility = DataGridGridLinesVisibility.None;
        //}
    }
}