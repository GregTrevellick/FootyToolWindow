// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace HierarchicalDataTemplate
{
    public class DataArtefact
    {
        public DataArtefact(string name)
        {
            Name = name;
            ActualData = new List<string>();
        }

        public string Name { get; }

        public List<string> ActualData { get; }

    }
}