using System;

namespace FootballDataOrg.ResponseEntities
{
    public class CompetitionResponse
    {
        public int Id { get; set; }
        public string Caption { get; set; }//gregt not reqd ?
        public string League { get; set; }
        public string Year { get; set; }//gregt not reqd ?
        public int NumberOfGames { get; set; }//gregt not reqd ?
        public DateTime LastUpdated { get; set; }//gregt not reqd ?
    }
}
