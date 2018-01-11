using System.ComponentModel;

namespace FootieData.Entities
{
    public class Standing
    {
        [Description("Pos")]
        public int Rank { get; set; }
        [Description("Club")]
        public string Team { get; set; }
        [Description("Pl")]
        public int Played { get; set; }
        //public string CrestURI { get; set; }
        [Description("F")]
        public int For { get; set; }
        [Description("A")]
        public int Against { get; set; }
        [Description("Gd")]
        public int Diff { get; set; }
        [Description("Pts")]
        public int Points { get; set; }
    }
}