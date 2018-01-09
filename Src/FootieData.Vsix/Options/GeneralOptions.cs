using Microsoft.VisualStudio.Shell;
using System.ComponentModel;
using FootieData.Vsix.Helpers;

namespace FootieData.Vsix.Options
{
    public class GeneralOptions : DialogPage
    {
        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName("Show premier league")]
        [Description("todo")]
        public bool ShowPL { get; set; } = true;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName("Expand premier league")]
        [Description("todo")]
        public bool ExpandPL { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName("Show budesliga 1")]
        [Description("todo")]
        public bool ShowBL1 { get; set; } = true;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName("Expand budesliga 1")]
        [Description("todo")]
        public bool ExpandL1 { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName("Show budesliga 2")]
        [Description("todo")]
        public bool ShowBL2 { get; set; } = false;
        
        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName("Expand budesliga 2")]
        [Description("todo")]
        public bool ExpandBL2 { get; set; } = false;
    }
}
