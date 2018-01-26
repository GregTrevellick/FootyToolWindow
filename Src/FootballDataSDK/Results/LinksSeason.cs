using FootballDataSDK.Models.Common;

namespace FootballDataSDK.Models.Results
{
    public class LinksSeason
    {
        public Link self { get; set; }
        public Link teams { get; set; }
        public Link fixtures { get; set; }
        public Link leagueTable { get; set; }
    }
    
}
