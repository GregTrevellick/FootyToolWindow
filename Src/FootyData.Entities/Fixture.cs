using System;

namespace FootieData.Entities
{
    public class Fixture
    {
        public DateTime Date { get; set; }
        public string HomeName { get; set; }
        public int? GoalsHome { get; set; }
        public int? GoalsAway { get; set; }
        public string AwayName { get; set; }
    }
}