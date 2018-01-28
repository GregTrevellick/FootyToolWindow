using FootballDataOrg;
using FootballDataOrg.ResponseEntities;

namespace FootieData.Gateway
{
    public sealed class CompetitionResultSingleton
    {
        public CompetitionResponseDto CompetitionResult;
        public FootballDataOrgApiGateway FootballDataOrgApiGateway;

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
            FootballDataOrgApiGateway = new FootballDataOrgApiGateway("52109775b158" + "4a93854ca187690ed4b");
            CompetitionResult = FootballDataOrgApiGateway.GetCompetitionResult();
        }
    }
}