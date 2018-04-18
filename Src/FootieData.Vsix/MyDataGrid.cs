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
            var color = new SolidColorBrush((Color) ColorConverter.ConvertFromString("#F8F8F8"));//("#F2F2F2"));//("#FFFFF0"));
            AlternatingRowBackground = color;
            ColumnHeaderHeight = 24;
            RowHeaderWidth = 0;
            CanUserAddRows = false;
            GridLinesVisibility = DataGridGridLinesVisibility.None;
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
            var standing = (Standing)e.Row.Item;//gregt replace Standing with a try/catch or similar
            if (standing.Team.Contains("Chelsea"))//gregt replace .Team with .NewErrorText
            {
                var _hideHeaderStyle = new Style();
                _hideHeaderStyle.Setters.Add(new Setter(HeadersVisibilityProperty, DataGridHeadersVisibility.None));//"None"));

                var _hideCellStyle = new Style();
                var color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#728769"));
                _hideCellStyle.Setters.Add(new Setter(BackgroundProperty, color));//gregt replace green background with hiding of the columns (except for the NewErrorText column)
                //_hideCellStyle.Setters.Add(new Setter(MaxWidthProperty, (double)0));
                //_hideCellStyle.Setters.Add(new Setter(ColumnWidthProperty, (double)0)); 

                foreach (var item in this.Columns)
                {
                    item.HeaderStyle = _hideHeaderStyle;
                    item.CellStyle = _hideCellStyle;
                }
            }
        }
    }
}