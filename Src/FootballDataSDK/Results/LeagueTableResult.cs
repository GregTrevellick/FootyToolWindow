using FootballDataSDK.Common;

namespace FootballDataSDK.Results
{

    public class LeagueTableResult
    {
        public Links _links { get; set; }
        public string leagueCaption { get; set; }
        public int matchday { get; set; }
        public Standing[] standing { get; set; }

        public string error { get; set; }
    }
    
}
