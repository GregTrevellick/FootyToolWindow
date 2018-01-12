using System;
using System.Windows;
using System.Windows.Controls;

namespace HierarchicalDataTemplate
{
    public class MyDataGrid : DataGrid
    {
        //private void onload(object sender, RoutedEventArgs e)
        //{
        //    Window parentWindow = Window.GetWindow((DependencyObject)sender);
        //    if (parentWindow != null)
        //    {
        //        parentWindow.Close();
        //    }
        //}

        protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            try
            {
                base.OnAutoGeneratingColumn(e);
                var propDescr = e.PropertyDescriptor as System.ComponentModel.PropertyDescriptor;
                e.Column.Header = propDescr.Description;
            }
            catch (Exception ex)
            {
                //Utils.ReportException(ex);
            }
        }

    }
}