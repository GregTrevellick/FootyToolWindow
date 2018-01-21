using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace HierarchicalDataTemplate
{
    public class MyDataGrid : DataGrid
    {
        public MyDataGrid()
        {
            var color = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFF0"));
            AlternatingRowBackground = color;
            ColumnHeaderHeight = 21;
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
            }
            catch (Exception)
            {
                //Do nothing (gregt except log it ?) as simply means no column heading
            }
        }
    }
}