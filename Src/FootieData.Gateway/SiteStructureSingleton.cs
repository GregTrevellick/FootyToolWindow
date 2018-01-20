using FootballDataSDK.Models.Results;
using FootballDataSDK.Services;

namespace FootieData.Gateway
{
    public sealed class SiteStructureSingleton
    {
        public SoccerSeasonResult SoccerSeasonResult;

        private static readonly SiteStructureSingleton _instance = new SiteStructureSingleton();
        public static SiteStructureSingleton Instance
        {
            get
            {
                return _instance;
            }
        }

        private SiteStructureSingleton()
        {
            var footDataServices = new FootDataServices
            {
                AuthToken = "52109775b1584a93854ca187690ed4bb"
            };
            SoccerSeasonResult = footDataServices.SoccerSeasons();
        }
    }
}