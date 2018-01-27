using System.Threading.Tasks;
using FootballDataSDK;
using FootballDataSDK.Results;

namespace FootieData.Gateway
{
    public sealed class CompetitionResultSingleton
    {
        public CompetitionResult CompetitionResult;

        private static readonly CompetitionResultSingleton _instance = new CompetitionResultSingleton();

        public static CompetitionResultSingleton Instance
        {
            get
            {
                return _instance;
            }
        }

        private CompetitionResultSingleton()
        {
            var footDataServices = new FootDataServices("52109775b158" + "4a93854ca187690ed4b");
            CompetitionResult = footDataServices.GetCompetitionResult();
        }

        //private CompetitionResultSingleton()
        //{
        //    CompetitionResult = GetCompetitionResultSingletonAsync().Result;
        //}

        //private async Task<CompetitionResult> GetCompetitionResultSingletonAsync()
        //{
        //    var footDataServices = new FootDataServices("52109775b158" + "4a93854ca187690ed4b");
        //    var competitionResult = await footDataServices.GetCompetitionResultAsync();
        //    return competitionResult;
        //}

       

    }
}