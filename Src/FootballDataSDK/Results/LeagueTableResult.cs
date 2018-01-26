using FootballDataSDK.Models.Common;

namespace FootballDataSDK.Models.Results
{

    public class LeagueTableResult
    {
        public Links _links { get; set; }
        public string leagueCaption { get; set; }
        public int matchday { get; set; }
        public Standing[] standing { get; set; }

        public string error { get; set; }
    }


    public class LinksStanding
    {
        public Link team { get; set; }
    }

    

    public class Home
    {
        public int goals { get; set; }
        public int goalsAgainst { get; set; }
        public int wins { get; set; }
        public int draws { get; set; }
        public int losses { get; set; }
    }

    public class Away
    {
        public int goals { get; set; }
        public int goalsAgainst { get; set; }
        public int wins { get; set; }
        public int draws { get; set; }
        public int losses { get; set; }
    }
    
}
