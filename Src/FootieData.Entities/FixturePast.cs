using System.ComponentModel;

namespace FootieData.Entities
{
    public class FixturePast : EntityBase
    {
        [Description("Date")]
        public string Date { get; set; }
        [Description("Home")]
        public string HomeName { get; set; }
        [Description("")]
        public int? GoalsHome { get; set; }
        [Description("")]
        public int? GoalsAway { get; set; }
        [Description("Away")]
        public string AwayName { get; set; }
    }
}