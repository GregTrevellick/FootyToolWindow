using System.Collections.Generic;
using FootieData.Entities.ReferenceData;

namespace FootieData.Entities
{
    public sealed class LeagueCodeMappingsSingleton
    {
        //public IDictionary<InternalLeagueCode, ExternalLeagueCode> LeagueCodeMappings;
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

        //private static IDictionary<InternalLeagueCode, ExternalLeagueCode> GetValidMappings()
        //{
        //    //var validMappings = (from k in AllLeagueCodes.AllMappings
        //    //                     where !BadLeagueCodes.BadDataExternalLeagueCodes.Contains(k.Value)
        //    //                     select k).ToDictionary(x => x.Key, x => x.Value);
        //    var validMappings = AllLeagueCodes.AllMappings;
        //    return validMappings;
        //}
        private static IEnumerable<OneMap> GetValidMappings()
        {
            //var validMappings = (from k in AllLeagueCodes.AllMappings
            //                     where !BadLeagueCodes.BadDataExternalLeagueCodes.Contains(k.Value)
            //                     select k).ToDictionary(x => x.Key, x => x.Value);
            var validMappings = AllLeagueCodes.AllMappings;
            return validMappings;
        }
    }
}