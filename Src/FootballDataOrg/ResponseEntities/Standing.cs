using FootballDataOrg.ResponseEntities.HomeAway;

namespace FootballDataOrg.ResponseEntities
{
    public class Standing
    {
        public int Rank { get; set; }//'position' if using non-minified request
        public string Team { get; set; }//'teamName' if using non-minified request
        //public string crestURI { get; set; }
        public int PlayedGames { get; set; }
        public int Points { get; set; }
        public int Goals { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public Home Home { get; set; }
        public Away Away { get; set; }
    }
}