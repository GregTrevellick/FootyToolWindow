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

        // Be warned that the `Loaded` event runs anytime the window loads into view, so you
        // will probably want to include an Unloaded event that detaches the collection

        protected override void OnLoadingRow(DataGridRowEventArgs e)
        {
            base.OnLoadingRow(e);
            var standing = (Standing)e.Row.Item;
            if (standing.Team.Contains("Man"))
            {
                var _hideStyle = new Style();
                //_hideStyle.Setters.Add(new Setter(MaxWidthProperty, 0m));
                var color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#b53939"));
                _hideStyle.Setters.Add(new Setter(BackgroundProperty, color));
                foreach (var item in this.Columns)
                {
                    item.HeaderStyle = _hideStyle;
                    item.CellStyle = _hideStyle;
                    //dataGrid.HeadersVisibility = DataGridHeadersVisibility.None;
                }
            }
        }
    }
}