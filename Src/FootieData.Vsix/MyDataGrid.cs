using FootieData.Entities;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FootieData.Vsix
{
    public class MyDataGrid : DataGrid
    {
        public MyDataGrid()
        {
            var color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F8F8F8"));//("#F2F2F2"));//("#FFFFF0"));
            AlternatingRowBackground = color;
            CanUserAddRows = false;
            ColumnHeaderHeight = 24;
            GridLinesVisibility = DataGridGridLinesVisibility.None;
            RowHeaderWidth = 0;
        }

        protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                base.OnAutoGeneratingColumn(e);

                var propDescr = e.PropertyDescriptor as System.ComponentModel.PropertyDescriptor;
                e.Column.Header = propDescr?.Description;

                var style = new Style(typeof(System.Windows.Controls.Primitives.DataGridColumnHeader));
                style.Setters.Add(new Setter(ToolTipService.ToolTipProperty, "Click to sort"));

                e.Column.HeaderStyle = style;
            }
            catch (Exception)
            {
                Logger.Log("No data grid column heading found");
            }
        }

        protected override void OnLoadingRow(DataGridRowEventArgs e)
        {
            base.OnLoadingRow(e);

            var entityBase = (EntityBase)e.Row.Item;
            var hidePoliteError = string.IsNullOrEmpty(entityBase.PoliteError);

            foreach (var item in this.Columns)
            {
                //TODO extract out
                #region error column 
                var isPoliteErrorColumn = item.SortMemberPath == nameof(entityBase.PoliteError);
                var visibility = Visibility.Collapsed;

                if (isPoliteErrorColumn)
                {
                    if (!hidePoliteError)
                    {
                        visibility = Visibility.Visible;
                    }
                }
                else
                {
                    if (hidePoliteError)
                    {
                        visibility = Visibility.Visible;
                    }
                }

                item.Visibility = visibility;
                #endregion

                //TODO extract out
                #region styling 1
                var isRightAlignColumn =
                    item.SortMemberPath == nameof(FixturePast.Date) ||
                    item.SortMemberPath == nameof(FixturePast.GoalsHome) ||
                    item.SortMemberPath == nameof(FixturePast.HomeName) ||
                    item.SortMemberPath == nameof(FixtureFuture.Date) ||
                    item.SortMemberPath == nameof(FixtureFuture.HomeName) ||
                    item.SortMemberPath == nameof(Standing.Against) ||
                    item.SortMemberPath == nameof(Standing.AwayDraws) ||
                    item.SortMemberPath == nameof(Standing.AwayGoalsAgainst) ||
                    item.SortMemberPath == nameof(Standing.AwayGoalsFor) ||
                    item.SortMemberPath == nameof(Standing.AwayLosses) ||
                    item.SortMemberPath == nameof(Standing.AwayPlayed) ||
                    item.SortMemberPath == nameof(Standing.AwayPoints) ||
                    item.SortMemberPath == nameof(Standing.AwayWins) ||
                    item.SortMemberPath == nameof(Standing.Diff) ||
                    item.SortMemberPath == nameof(Standing.Draws) ||
                    item.SortMemberPath == nameof(Standing.For) ||
                    item.SortMemberPath == nameof(Standing.HomeDraws) ||
                    item.SortMemberPath == nameof(Standing.HomeGoalsAgainst) ||
                    item.SortMemberPath == nameof(Standing.HomeGoalsFor) ||
                    item.SortMemberPath == nameof(Standing.HomeLosses) ||
                    item.SortMemberPath == nameof(Standing.HomePlayed) ||
                    item.SortMemberPath == nameof(Standing.HomePoints) ||
                    item.SortMemberPath == nameof(Standing.HomeWins) ||
                    item.SortMemberPath == nameof(Standing.Losses)||
                    item.SortMemberPath == nameof(Standing.Played) ||
                    item.SortMemberPath == nameof(Standing.Points) ||
                    item.SortMemberPath == nameof(Standing.Rank) ||
                    item.SortMemberPath == nameof(Standing.Wins);

                if (isRightAlignColumn)
                {
                    var rightAlignStyle = new Style();
                    rightAlignStyle.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right));
                    item.CellStyle = rightAlignStyle;
                    item.HeaderStyle = rightAlignStyle;
                }
                #endregion
                
                //TODO extract out
                #region styling 2
                //var homeAwayFontColour = Brushes.SlateGray;
                //////////    _homeStyle = new Style();
                //////////    _homeStyle.Setters.Add(new Setter(ForegroundProperty, homeAwayFontColour));
                //////////    _awayStyle = new Style();
                //////////    _awayStyle.Setters.Add(new Setter(ForegroundProperty, homeAwayFontColour));
                #endregion
            }
        }
    }
}















//////////////////////////////////////////_hideCellStyle.Setters.Add(new Setter(MaxWidthProperty, (double)0));
//////////////////////////////////////////_hideCellStyle.Setters.Add(new Setter(ColumnWidthProperty, (double)0)); 
//////////////////////////////////////////_hideCellStyle.Setters.Add(new Setter(VisibilityProperty, Visibility.Collapsed));

//////////////////////////////////////////var CellStyle = new Style(typeof(DataGridCell));
////////////////////////////////////////////CellStyle.Setters.Add(new Setter(DataGridCell.MaxWidthProperty, 0));
//////////////////////////////////////////CellStyle.Setters.Add(new Setter(DataGridCell.VisibilityProperty, Visibility.Collapsed));
////////////////////////////////////////////_hideCellStyle.Setters.Add(new Setter(CellStyleProperty, Visibility.Collapsed));
//////////////////////////////////////////_hideCellStyle = CellStyle;

//////////////////////////////////////////https://stackoverflow.com/questions/39725214/datagrid-cell-styling-with-dynamic-generating-column?utm_medium=organic&utm_source=google_rich_qa&utm_campaign=google_rich_qa

//////////////////////////////////////////DataGridRow row = e.Row as DataGridRow;
//////////////////////////////////////////row.Visibility == Visibility.Collapsed;

////////////////////////////////////////item.CellStyle = _hideCellStyle;

//var _hideHeaderStyle = new Style();
//var color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a75ced"));
//_hideHeaderStyle.Setters.Add(new Setter(BackgroundProperty, color));
//                //_hideHeaderStyle.Setters.Add(new Setter(HeadersVisibilityProperty, DataGridHeadersVisibility.None));//"None"));

//var _hideCellStyle = new Style();
//color = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#728769"));
//                _hideCellStyle.Setters.Add(new Setter(BackgroundProperty, color));//replace green background with hiding of the columns (except for the NewErrorText column)
/////item.HeaderStyle = _hideHeaderStyle;
