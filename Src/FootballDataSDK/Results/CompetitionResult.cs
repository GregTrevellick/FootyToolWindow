using System.Collections.Generic;

namespace FootballDataSDK.Results
{
    public class CompetitionResult
    {
        public IEnumerable<Competition> Competitions { get; set; }
        public string error { get; set; }
    }
}
