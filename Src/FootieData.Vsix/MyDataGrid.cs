using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            //    var brushesAway = Brushes.LightSteelBlue;//gregtdeupde
             //   var brushesHome = Brushes.Lavender;//gregtdeupde

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



//               / if (e.Column.DisplayIndex >= 6 && e.Column.DisplayIndex<=12)                
//               / {
//               /     style.Setters.Add(new Setter(ForegroundProperty, brushesHome));
//               / }
///
//                if (e.Column.DisplayIndex >= 13 && e.Column.DisplayIndex <= 16)
//                {
//                    style.Setters.Add(new Setter(ForegroundProperty, brushesAway));
//                }

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

        //protected override void OnLoadingRow(DataGridRowEventArgs e)
        //{
        //    base.OnLoadingRow(e);
        //    if (e.Row.GetIndex() == 0)
        //    {
        //        e.Row.ren.SetRenderMethodDelegate(new RenderMethod(RenderHeader));

        //    }
        //}

        //protected void GridViewWithDynamicHeader_RowCreated(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == GridControlRowType.Header)
        //        //Render the header
        //        e.Row.SetRenderMethodDelegate(new RenderMethod(RenderHeader));
        //}
    }



    ////https://www.codeproject.com/Articles/27824/Dynamic-Multiple-Row-Column-Grid-Header

    //public class DynamicHeaderCell
    //{
    //    public String Header { get; set; }
    //    public int RowSpan { get; set; }
    //    public int ColSpan { get; set; }

    //    public DynamicHeaderCell(String header)
    //    {
    //        RowSpan = 1;
    //        ColSpan = 1;
    //        Header = header;
    //    }
    //}






    //public class DynamicHeader
    //{
    //    public int HeaderDepth { get; set; }
    //    public String[] Headers { get; set; }
    //    public DynamicHeader(String header)
    //    {
    //        Headers = header.Split('|');
    //        HeaderDepth = Headers.Length;
    //    }
    //}




    //public class DynamicHeaders
    //{
    //    List<DynamicHeader> Headers;
    //    int HeaderRows;
    //    int HeaderCols;

    //    public DynamicHeaders(String Header)
    //    {
    //        Headers = new List<DynamicHeader>();
    //        String[] HeaderParts = Header.Split(',');
    //        foreach (String tmpHeaderPart in HeaderParts)
    //            Headers.Add(new DynamicHeader(tmpHeaderPart));

    //        HeaderCols = Headers.Count;
    //        HeaderRows = Headers.Max(H => H.HeaderDepth);
    //        ParseHeader();
    //    }

    //    public ArrayList ParseHeader();
    //}
}