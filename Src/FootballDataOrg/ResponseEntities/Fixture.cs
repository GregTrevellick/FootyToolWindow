using System;
using FootballDataOrg.ResponseEntities.HomeAway;

namespace FootballDataOrg.ResponseEntities
{
    public class Fixture
    {
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public HomeAwayGoals Result { get; set; }
    }
}