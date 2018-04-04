using FootballDataOrg;
/////////////////////////////////////////////////////////////////using FootballDataOrg.ResponseEntities;

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
            CompetitionResultSingletonAsync(FootballDataOrgApiGateway);
        }

        private async void CompetitionResultSingletonAsync(FootballDataOrgApiGateway FootballDataOrgApiGateway)
        {
            CompetitionResult = await FootballDataOrgApiGateway.GetCompetitionResultAsync();
        }
    }
}