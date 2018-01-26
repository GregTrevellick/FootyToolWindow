using System.Collections.Generic;
using FootballDataSDK.Models;

namespace FootballDataSDK.Results
{
    public class FixturesResult
    {
        public IEnumerable<Fixture> fixtures { get; set; }
        public string error { get; set; }        
    }    
}
