using System;
using System.Windows.Controls;

namespace HierarchicalDataTemplate
{
    public class MyDataGrid : DataGrid
    {
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