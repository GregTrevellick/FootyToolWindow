using System;
using System.ComponentModel;

namespace FootieData.Entities
{
    //public class Fixture
    //{
    //    [Description("Date")]
    //    public string Date { get; set; }
    //    [Description("H")]
    //    public string HomeName { get; set; }
    //    [Description("")]
    //    public int? GoalsHome { get; set; }
    //    [Description("")]
    //    public int? GoalsAway { get; set; }
    //    [Description("A")]
    //    public string AwayName { get; set; }
    //}

    public class FixturePast
    {
        [Description("Date")]
        public string Date { get; set; }
        [Description("H")]
        public string HomeName { get; set; }
        [Description("")]
        public int? GoalsHome { get; set; }
        [Description("")]
        public int? GoalsAway { get; set; }
        [Description("A")]
        public string AwayName { get; set; }
    }

    public class FixtureFuture
    {
        [Description("Date")]
        public string Date { get; set; }
        [Description("Time")]
        public string Time { get; set; }
        [Description("H")]
        public string HomeName { get; set; }
        [Description("A")]
        public string AwayName { get; set; }
    }
}