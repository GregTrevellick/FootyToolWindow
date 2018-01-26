using FootballDataSDK.Models;

namespace FootballDataSDK.Results
{
    public class FixturesResult
    {
        public string timeFrameStart { get; set; }
        public string timeFrameEnd { get; set; }       
        public int count { get; set; }
        public Fixture[] fixtures { get; set; }
        public string error { get; set; }        
    }    
}
