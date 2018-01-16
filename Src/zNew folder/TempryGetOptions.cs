using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            //AddThem(HierarchicalDataTemplate.InternalLeagueCode.DE1);
            //AddThem(HierarchicalDataTemplate.InternalLeagueCode.DE2);
            //AddThem(HierarchicalDataTemplate.InternalLeagueCode.DE3);
            //AddThem(HierarchicalDataTemplate.InternalLeagueCode.ES1);
            //AddThem(HierarchicalDataTemplate.InternalLeagueCode.ES2);
            //AddThem(HierarchicalDataTemplate.InternalLeagueCode.FR1);
            //AddThem(HierarchicalDataTemplate.InternalLeagueCode.FR2);
            //AddThem(HierarchicalDataTemplate.InternalLeagueCode.GR1);
            //AddThem(HierarchicalDataTemplate.InternalLeagueCode.IT1);
            //AddThem(HierarchicalDataTemplate.InternalLeagueCode.IT2);
            //AddThem(HierarchicalDataTemplate.InternalLeagueCode.NL1);
            //AddThem(HierarchicalDataTemplate.InternalLeagueCode.PT1);
            AddThem(HierarchicalDataTemplate.InternalLeagueCode.UK1);
            //AddThem(HierarchicalDataTemplate.InternalLeagueCode.UK2);
            //AddThem(HierarchicalDataTemplate.InternalLeagueCode.UK2);
            //AddThem(HierarchicalDataTemplate.InternalLeagueCode.UK3);

            //////AddThem(HierarchicalDataTemplate.InternalLeagueCode.DE4);
            //////AddThem(HierarchicalDataTemplate.InternalLeagueCode.ES3);
            //////AddThem(HierarchicalDataTemplate.InternalLeagueCode.UK4);

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
                        //new LeagueSubOption {Expand = false, GridType = GridType.Result},
                        //new LeagueSubOption {Expand = false, GridType = GridType.Fixture}
                    }
                });

            //_generalOptions.LeagueOptions.Add(new LeagueOption
            //{
            //    InternalLeagueCode = internalLeagueCode,
            //    ShowLeague = true,
            //    LeagueSubOptions = new List<LeagueSubOption>
            //        {new LeagueSubOption {Expand = false, GridType = GridType.Result}}
            //});

            //_generalOptions.LeagueOptions.Add(new LeagueOption
            //{
            //    InternalLeagueCode = internalLeagueCode,
            //    ShowLeague = true,
            //    LeagueSubOptions = new List<LeagueSubOption>
            //        {new LeagueSubOption {Expand = false, GridType = GridType.Fixture}}
            //});
        }

    }
}
