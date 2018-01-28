using System.Collections.Generic;

namespace FootballDataOrg.ResponseEntities
{
    public class StandingsResponse
    {
        //public string leagueCaption { get; set; }
        public IEnumerable<Standing> standing { get; set; }
        public string error { get; set; }
    }    
}
