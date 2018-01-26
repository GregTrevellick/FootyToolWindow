// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace HierarchicalDataTemplate
{
    public class League2
    {
        public League2(string name)
        {
            LeagueName = name;
            //DataArtefacts = new List<DataArtefact>();
            ActualDataStandings = new List<Stringer>();
            ActualDataResults = new List<Stringer>();
            ActualDataFixtures = new List<Stringer>();
        }

        public string LeagueName { get; }
        //public List<DataArtefact> DataArtefacts { get; }
        public IEnumerable<Stringer> ActualDataStandings { get; set; }
        public IEnumerable<Stringer> ActualDataResults { get; set; }
        public IEnumerable<Stringer> ActualDataFixtures { get; set; }

    }

    public class Stringer
    {
        public string Aaa { get; set; }
        public string Bbb { get; set; }
        public string Ccc { get; set; }
    }
}