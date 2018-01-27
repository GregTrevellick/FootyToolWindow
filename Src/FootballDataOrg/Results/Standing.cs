namespace FootballDataOrg.Results
{
    public class Standing
    {
        public int rank { get; set; }//position
        public string team { get; set; }//teamName
        //public string crestURI { get; set; }
        public int playedGames { get; set; }
        public int points { get; set; }
        public int goals { get; set; }
        public int goalsAgainst { get; set; }
        public int goalDifference { get; set; }
        public int wins { get; set; }
        public int draws { get; set; }
        public int losses { get; set; }
        public Home home { get; set; }
        public Away away { get; set; }
    }
}