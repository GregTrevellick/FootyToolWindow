using System.ComponentModel;

namespace FootieData.Entities
{
    public class FixtureFuture
    {
        [Description("Date")]
        public string Date { get; set; }
        [Description("Time")]
        public string Time { get; set; }
        [Description("Home")]
        public string HomeName { get; set; }
        [Description("Away")]
        public string AwayName { get; set; }
    }
}