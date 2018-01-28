using System;

namespace FootballDataOrg.ResponseEntities
{
    public class Fixture
    {
        public DateTime date { get; set; }
        public string status { get; set; }
        public string homeTeamName { get; set; }
        public string awayTeamName { get; set; }
        public ResultMatch result { get; set; }
    }
}