using System.Collections.Generic;
using FootieData.Entities.ReferenceData;

namespace FootieData.Entities
{
    public sealed class LeagueCodeMappingsSingleton
    {
        public IEnumerable<OneMap> LeagueCodeMappings;

        private static readonly LeagueCodeMappingsSingleton _instance = new LeagueCodeMappingsSingleton();

        public static LeagueCodeMappingsSingleton Instance
        {
            get
            {
                return _instance;
            }
        }

        private LeagueCodeMappingsSingleton()
        {
            LeagueCodeMappings = GetValidMappings();
        }

        private static IEnumerable<OneMap> GetValidMappings()
        {
            var validMappings = AllLeagueCodes.AllMappings;
            return validMappings;
        }
    }
}