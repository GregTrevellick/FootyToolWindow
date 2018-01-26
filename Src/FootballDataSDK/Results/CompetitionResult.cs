using System.Collections.Generic;

namespace FootballDataSDK.Results
{
    public class CompetitionResult
    {
        public IEnumerable<Competition> competitions { get; set; }
        public string error { get; set; }
    }
}
