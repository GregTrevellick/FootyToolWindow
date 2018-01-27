﻿using System.Collections.Generic;

namespace FootballDataOrg.Results
{
    public class LeagueTableResult
    {
        public string leagueCaption { get; set; }
        public IEnumerable<Standing> standing { get; set; }
        public string error { get; set; }
    }    
}
