using System.ComponentModel;

namespace HierarchicalDataTemplate
{
    public enum GridType
    {
        Unknown = 0,
        [Description("latest league table")]
        Standing,
        [Description("recent results")]
        Result,
        [Description("upcoming fixtures")]
        Fixture
    }
}