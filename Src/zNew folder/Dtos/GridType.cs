using System.ComponentModel;

namespace HierarchicalDataTemplate
{
    public enum GridType
    {
        Unknown = 0,
        [Description("Table")]
        Standing,
        [Description("Recent results")]
        Result,
        [Description("Upcoming fixtures")]
        Fixture
    }
}