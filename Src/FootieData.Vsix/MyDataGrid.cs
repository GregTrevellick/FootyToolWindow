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
    }
}