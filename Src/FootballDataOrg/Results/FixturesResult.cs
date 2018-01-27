using System.Collections.Generic;
using FootballDataOrg.Models;

namespace FootballDataOrg.Results
{
    public class FixturesResult
    {
        public IEnumerable<Fixture> fixtures { get; set; }
        public string error { get; set; }        
    }    
}
