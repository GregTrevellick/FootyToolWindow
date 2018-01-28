using System.Collections.Generic;

namespace FootballDataOrg.ResponseEntities
{
    public class StandingsResponse
    {
        //public string leagueCaption { get; set; }
        public IEnumerable<Standing> Standing { get; set; }
        public string Error { get; set; }
    }    
}
