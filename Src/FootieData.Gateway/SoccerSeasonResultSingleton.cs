using FootballDataSDK.Models.Results;
using FootballDataSDK.Services;

namespace FootieData.Gateway
{
    public sealed class SoccerSeasonResultSingleton
    {
        public SoccerSeasonResult SoccerSeasonResult;

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
            var footDataServices = new FootDataServices
            {
                AuthToken = "52109775b1584a93854ca187690ed4bb"
            };
            SoccerSeasonResult = footDataServices.SoccerSeasons();
        }
    }
}