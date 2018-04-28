//using System.Collections.Generic;
//using FootieData.Entities.ReferenceData;
////using FootieData.Gateway;

//namespace FootieData.Entities
//{
//    public sealed class LeagueDtosSingleton
//    {
//        public IEnumerable<LeagueDto> LeagueDtos;

//        private static readonly LeagueDtosSingleton _instance = new LeagueDtosSingleton();

//        //private CompetitionResultSingleton _competitionResultSingleton;

//        //public LeagueDtosSingleton(CompetitionResultSingleton competitionResultSingleton)
//        //{
//        //    _competitionResultSingleton = competitionResultSingleton;
//        //}

//        public static LeagueDtosSingleton Instance
//        {
//            get
//            {
//                return _instance;
//            }
//        }

//        private LeagueDtosSingleton()
//        {
//            LeagueDtos = LeagueMapping.GetLeagueDtos();//this._competitionResultSingleton);
//        }
//    }
//}