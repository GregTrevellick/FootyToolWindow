// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace HierarchicalDataTemplate
{
    public class DataArtefact
    {
        public DataArtefact(string name)
        {
            SubName = name;
            ActualData = new List<string>();
        }

        public string SubName { get; }

        public List<string> ActualData { get; }

    }
}