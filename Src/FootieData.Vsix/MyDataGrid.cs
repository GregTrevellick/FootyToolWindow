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

        //private void SetColumnStylingStandings(DataGrid dataGrid)
        //{
        //    InitializeStyling();//gregt do in ctor????

        //    //these hardcoded columns numbers stinks to high heaven, but using Attributes against column properties is expensive when retrieving using reflection
        //    var primaryColumns = new List<int> { 0, 2, 3, 4, 5, 6, 7, 8, 9 };
        //    var homeColumns = new List<int> { 10, 11, 12, 13, 14, 15, 16 };
        //    var awayColumns = new List<int> { 17, 18, 19, 20, 21, 22, 23 };

        //    var rightAlignColumns = primaryColumns.Union(homeColumns).Union(awayColumns);

        //    WpfHelper.FormatDataGridColumns(dataGrid.Columns, rightAlignColumns, _rightAlignStyle);
        //    WpfHelper.FormatDataGridColumns(dataGrid.Columns, homeColumns, _homeStyle);
        //    WpfHelper.FormatDataGridColumns(dataGrid.Columns, awayColumns, _awayStyle);
        //    WpfHelper.FormatDataGridHeader(dataGrid.Columns, homeColumns, _homeStyle);
        //    WpfHelper.FormatDataGridHeader(dataGrid.Columns, awayColumns, _awayStyle);
        //}

        //private void InitializeStyling()
        //{
        //    var rightAlignSetter = new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right);

        //    _rightAlignStyle = new Style();
        //    _rightAlignStyle.Setters.Add(rightAlignSetter);

        //    var homeAwayFontColour = Brushes.SlateGray;

        //    _homeStyle = new Style();
        //    _homeStyle.Setters.Add(rightAlignSetter);
        //    _homeStyle.Setters.Add(new Setter(ForegroundProperty, homeAwayFontColour));

        //    _awayStyle = new Style();
        //    _awayStyle.Setters.Add(rightAlignSetter);
        //    _awayStyle.Setters.Add(new Setter(ForegroundProperty, homeAwayFontColour));
        //}

        protected override void OnLoadingRow(DataGridRowEventArgs e)
        {
            base.OnLoadingRow(e);

            var entityBase = (EntityBase)e.Row.Item;
            var hidePoliteError = string.IsNullOrEmpty(entityBase.PoliteError);

            foreach (var item in this.Columns)
            {
                #region error column
                var isPoliteErrorColumn = item.SortMemberPath == nameof(entityBase.PoliteError);
                var visibility = Visibility.Hidden;

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

                #region styling 1
                var isLeftAlignColumn =
                    item.SortMemberPath == nameof(Standing.Team) ||
                    item.SortMemberPath == nameof(FixturePast.AwayName) ||
                    /////item.SortMemberPath == nameof(FixturePast.HomeName) ||
                    item.SortMemberPath == nameof(FixtureFuture.AwayName);
                    /////item.SortMemberPath == nameof(FixtureFuture.HomeName)

                if (!isLeftAlignColumn)
                {
                    var rightAlignStyle = new Style();
                    rightAlignStyle.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, TextAlignment.Right));
                    item.CellStyle = rightAlignStyle;
                    item.HeaderStyle = rightAlignStyle;
                }
                #endregion

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
