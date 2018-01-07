namespace FootieData.Entities
{
    public class Standing
    {
        public int Rank { get; set; }
        public string Team { get; set; }
        public int PlayedGames { get; set; }
        //public string CrestURI { get; set; }
        public int Points { get; set; }
        public int Goals { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
    }
}