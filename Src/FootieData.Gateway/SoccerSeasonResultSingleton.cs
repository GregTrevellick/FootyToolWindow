using FootballDataSDK;
using FootballDataSDK.Results;

namespace FootieData.Gateway
{
    public sealed class SoccerSeasonResultSingleton
    {
        public CompetitionResult SoccerSeasonResult;

        private static readonly SoccerSeasonResultSingleton _instance = new SoccerSeasonResultSingleton();

        public static SoccerSeasonResultSingleton Instance
        {
            get
            {
                return _instance;
            }
        }

        private SoccerSeasonResultSingleton()
        {
            var footDataServices = new FootDataServices("52109775b158" + "4a93854ca187690ed4b");
            SoccerSeasonResult = footDataServices.GetCompetitionResult();
        }
    }
}