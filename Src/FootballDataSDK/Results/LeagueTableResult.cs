namespace FootballDataSDK.Results
{
    public class LeagueTableResult
    {
        public string leagueCaption { get; set; }
        public Standing[] standing { get; set; }
        public string error { get; set; }
    }    
}
