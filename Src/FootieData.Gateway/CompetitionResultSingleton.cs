using FootballDataOrg;

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
            FootballDataOrgApiGateway = new FootballDataOrgApiGateway();

            CompetitionResultSingletonSync(FootballDataOrgApiGateway);//calls into class that calls the external web service 
        }

        private void CompetitionResultSingletonSync(FootballDataOrgApiGateway FootballDataOrgApiGateway)
        {
            CompetitionResult = FootballDataOrgApiGateway.GetCompetitionResult();
        }
    }
}