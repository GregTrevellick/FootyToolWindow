using System.Collections.Generic;

namespace FootballDataOrg.ResponseEntities
{
    public class FixturesResponse
    {
        public IEnumerable<Fixture> fixtures { get; set; }
        public string error { get; set; }        
    }    
}
