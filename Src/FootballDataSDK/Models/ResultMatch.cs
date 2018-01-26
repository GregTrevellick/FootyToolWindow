namespace FootballDataSDK.Models
{
    public class ResultMatch
    {
        public int? goalsHomeTeam { get; set; }
        public int? goalsAwayTeam { get; set; }

        public Halftime halfTime { get; set; }
    }
}