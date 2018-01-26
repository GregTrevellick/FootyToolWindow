using System;

namespace FootballDataSDK.Results
{
    public class Competition
    {
        public int id { get; set; }
        public string caption { get; set; }
        public string league { get; set; }
        public string year { get; set; }
        public int numberOfGames { get; set; }
        public DateTime lastUpdated { get; set; }
    }
}
