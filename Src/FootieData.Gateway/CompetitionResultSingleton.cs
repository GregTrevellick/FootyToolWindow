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

            //this calls into class that calls the external web service 
            CompetitionResultSingletonAsync(FootballDataOrgApiGateway);
        }

        private async void CompetitionResultSingletonAsync(FootballDataOrgApiGateway FootballDataOrgApiGateway)
        {
            //this is slow - consider a task.run as this uses a different thread ?
            CompetitionResult = await FootballDataOrgApiGateway.GetCompetitionResultAsync();
        }
    }
}