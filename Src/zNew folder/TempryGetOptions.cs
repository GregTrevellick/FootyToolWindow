using HierarchicalDataTemplate.ReferenceData;
using System.Collections.Generic;

namespace HierarchicalDataTemplate
{
    public class TempryGetOptions
    {
        private static GeneralOptions _generalOptions;

        public GeneralOptions GetGeneralOptions()
        {
            _generalOptions = new GeneralOptions
            {
                LeagueOptions = new List<LeagueOption>()
            };

            //all ok
            AddThem(InternalLeagueCode.DE1);
            //AddThem(InternalLeagueCode.DE2);
            //AddThem(InternalLeagueCode.ES1);
            //AddThem(InternalLeagueCode.FR1);
            //AddThem(InternalLeagueCode.FR2);
            //AddThem(InternalLeagueCode.IT1);
            //AddThem(InternalLeagueCode.IT2);
            //AddThem(InternalLeagueCode.NL1);
            //AddThem(InternalLeagueCode.PT1);
            //AddThem(InternalLeagueCode.UK1);
            //AddThem(InternalLeagueCode.UK2);
            //AddThem(InternalLeagueCode.UK3);

            //////////////////////////////////////////////////////////////////////////AddThem(InternalLeagueCode.DE4);
            //////////////////////////////////////////////////////////////////////////AddThem(InternalLeagueCode.GR1);

            //AddThem(InternalLeagueCode.UK4);//always no data ? (f.a.cup)
            //AddThem(InternalLeagueCode.DE3);//always no data ?
            //AddThem(InternalLeagueCode.ES2);//always no data ?
            //AddThem(InternalLeagueCode.ES3);//always no data ?
            //AddThem(InternalLeagueCode.UEFA1);//always no data ?
            //AddThem(InternalLeagueCode.UEFA2);//always no data ?
            //AddThem(InternalLeagueCode.UEFA3);//always no data ?
            //AddThem(InternalLeagueCode.FIFA1);//always no data ?

            return _generalOptions;
        }

        private void AddThem(InternalLeagueCode internalLeagueCode)
        {
            _generalOptions.LeagueOptions.Add(
                new LeagueOption
                {
                    InternalLeagueCode = internalLeagueCode,
                    ShowLeague = true,
                    LeagueSubOptions = new List<LeagueSubOption>
                    {
                        new LeagueSubOption {Expand = true, GridType = GridType.Standing},
                        new LeagueSubOption {Expand = true, GridType = GridType.Result},
                        new LeagueSubOption {Expand = true, GridType = GridType.Fixture}
                    }
                });
        }
    }
}
