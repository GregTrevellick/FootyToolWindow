using System.Collections.Generic;
using FootieData.Entities;

namespace HierarchicalDataTemplate
{
    public class DataGridResult2
    {
        public ExternalLeagueCode ExternalLeagueCode { get; set; }
        public List<Fixture> Results { get; set; }
    }
}