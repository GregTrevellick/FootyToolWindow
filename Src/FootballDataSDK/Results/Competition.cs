using System;

namespace FootballDataSDK.Results
{
    public class Competition
    {
        public LinksSeason _links { get; set; }
        public int id { get; set; }
        public string caption { get; set; }
        public string league { get; set; }
        public string year { get; set; }
        public int currentMatchday { get; set; }
        public int numberOfTeams { get; set; }
        public int numberOfGames { get; set; }
        public DateTime lastUpdated { get; set; }
    }
    
}
