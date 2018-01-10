using System;

namespace FootieData.Entities
{
    public class Fixture
    {
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public DateTime Date { get; set; }
        public Result Result { get; set; }
        public Odds Odds { get; set; }
    }
}