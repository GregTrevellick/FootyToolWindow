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
            DataArtefacts = new List<DataArtefact>();
        }

        public string LeagueName { get; }
        public List<DataArtefact> DataArtefacts { get; }
    }
}