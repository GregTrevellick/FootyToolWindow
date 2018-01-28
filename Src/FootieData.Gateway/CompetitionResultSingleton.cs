﻿using FootballDataOrg;
using FootballDataOrg.ResponseEntities;

namespace FootieData.Gateway
{
    public sealed class CompetitionResultSingleton
    {
        public CompetitionResponseDto CompetitionResult;

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
            var footballDataOrgApiGateway = new FootballDataOrgApiGateway("52109775b158" + "4a93854ca187690ed4b");
            CompetitionResult = footballDataOrgApiGateway.GetCompetitionResult();
        }
    }
}