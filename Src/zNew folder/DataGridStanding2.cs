using System.Collections.Generic;
using FootieData.Entities;

namespace HierarchicalDataTemplate
{
    public class DataGridStanding2
    {
        public ExternalLeagueCode ExternalLeagueCode { get; set; }
        public List<Standing> Standings { get; set; }
    }
}