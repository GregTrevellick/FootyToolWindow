using System.Collections.Generic;

namespace FootieData.Entities
{
    public class LeagueResponse_Standings
    {
        public string LeagueCaption { get; set; }
        public int MatchDay { get; set; }
        public IEnumerable<Standing> Standings;
    }
}
