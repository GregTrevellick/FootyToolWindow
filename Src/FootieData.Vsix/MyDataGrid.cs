using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace FootieData.Vsix
{
    public class MyDataGrid : DataGrid
    {
        public MyDataGrid()
        {
            var color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFF0"));
            AlternatingRowBackground = color;
            ColumnHeaderHeight = 24;
            RowHeaderWidth = 0;
            CanUserAddRows = false;
            GridLinesVisibility = DataGridGridLinesVisibility.None;
            /////////VerticalAlignment = VerticalAlignment.Stretch;
        }

        protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                base.OnAutoGeneratingColumn(e);
                var propDescr = e.PropertyDescriptor as System.ComponentModel.PropertyDescriptor;
                e.Column.Header = propDescr?.Description;
            }
            catch (Exception)
            {
                Logger.Log("No data grid column heading found");
            }
        }
    }
}