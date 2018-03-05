using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
//gregt stuff to be deleted

using FootballDataOrg.ResponseEntities.HomeAway;
//using FootieData.Entities.CustomAttributes;
//using System.Reflection;

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
                var brushes = Brushes.LightSteelBlue;

//gregt stuff to be deleted
                //var style2 = new Style(typeof(System.Windows.Controls.Primitives.DataGridCellsPresenter));



                //// Get the class type to access its metadata.
                //var clsType = typeof(HomeAttribute);
                //// Get the assembly object.
                //var assy = clsType.Assembly;
                //// Store the assembly's name.
                ////String assyName = assy.GetName().Name;
                ////var a = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(assy, typeof(HomeAttribute));
                //Attribute a = Attribute.GetCustomAttribute(assy, typeof(HomeAttribute));
                //if ()

                ////var brushes = ColumnHeaderForegroundColor;
                style.Setters.Add(new Setter(ForegroundProperty, brushes));
                e.Column.HeaderStyle = style;

                //var style2 = new Style(typeof(System.Windows.Controls.Primitives.DataGridCellsPresenter));
                //style2.Setters.Add(new Setter(ForegroundProperty, brushes));

                //if (e.Column.Header.ToString().StartsWith("h"))
                //{
                //    e.Column.CellStyle = style2;
                //}
            }
            catch (Exception)
            {
                Logger.Log("No data grid column heading found");
            }
        }
    }
}