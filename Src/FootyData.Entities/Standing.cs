using System.ComponentModel;

namespace FootieData.Entities
{
    public class Standing
    {
        [Description("Pos")]
        public int Rank { get; set; }
        public string Team { get; set; }
        public int Played { get; set; }
        //public string CrestURI { get; set; }
        public int For { get; set; }
        public int Against { get; set; }
        public int Diff { get; set; }
        public int Points { get; set; }
    }
}