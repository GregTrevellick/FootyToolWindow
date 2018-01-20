using System.ComponentModel;

namespace FootieData.Entities
{
    public class Standing
    {
        [Description("")]
        public int Rank { get; set; }
        [Description("Club")]
        public string Team { get; set; }
        [Description("P")]
        public int Played { get; set; }
        //public string CrestURI { get; set; }
        [Description("F")]
        public int For { get; set; }
        [Description("A")]
        public int Against { get; set; }
        [Description("GD")]
        public int Diff { get; set; }
        [Description("PTs")]
        public int Points { get; set; }
    }
}