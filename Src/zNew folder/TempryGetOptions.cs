using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HierarchicalDataTemplate.ReferenceData;

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

            //AddThem(InternalLeagueCode.DE1);
            //AddThem(InternalLeagueCode.DE2);
            //AddThem(InternalLeagueCode.DE3);
            //AddThem(InternalLeagueCode.DE4);
            //AddThem(InternalLeagueCode.ES1);
            //AddThem(InternalLeagueCode.ES2);
            //AddThem(InternalLeagueCode.ES3);
            //AddThem(InternalLeagueCode.FR1);
            //AddThem(InternalLeagueCode.FR2);
            //AddThem(InternalLeagueCode.GR1);
            //AddThem(InternalLeagueCode.IT1);
            //AddThem(InternalLeagueCode.IT2);
            //AddThem(InternalLeagueCode.NL1);
            //AddThem(InternalLeagueCode.PT1);
            AddThem(InternalLeagueCode.UK1);
            AddThem(InternalLeagueCode.UK2);
            //AddThem(InternalLeagueCode.UK3);
            //AddThem(InternalLeagueCode.UK4);
            //AddThem(InternalLeagueCode.UEFA1);
            //AddThem(InternalLeagueCode.UEFA2);
            //AddThem(InternalLeagueCode.UEFA3);
            //AddThem(InternalLeagueCode.FIFA1);

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
