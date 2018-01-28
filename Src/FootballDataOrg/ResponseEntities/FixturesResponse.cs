using System.Collections.Generic;

namespace FootballDataOrg.ResponseEntities
{
    public class FixturesResponse
    {
        public IEnumerable<Fixture> Fixtures { get; set; }
        public string Error { get; set; }        
    }    
}
