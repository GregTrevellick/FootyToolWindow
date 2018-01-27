using System.Collections.Generic;
using FootieData.Entities.ReferenceData;

namespace FootieData.Entities
{
    public sealed class LeagueDtosSingleton
    {
        public IEnumerable<LeagueDto> LeagueDtos;

        private static readonly LeagueDtosSingleton _instance = new LeagueDtosSingleton();

        public static LeagueDtosSingleton Instance
        {
            get
            {
                return _instance;
            }
        }

        private LeagueDtosSingleton()
        {
            LeagueDtos = LeagueMapping.LeagueDtos;
        }
    }
}