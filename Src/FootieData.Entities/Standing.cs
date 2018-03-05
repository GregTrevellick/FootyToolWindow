using System.ComponentModel;

namespace FootieData.Entities
{
    public class Standing
    {
        //DO NOT RE-ORDER: this is the UI order

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

        [Description("hP")]
        public int HomePlayed => HomeWins + HomeDraws+ HomeLosses;
        [Description("hW")]
        public int HomeWins { get; set; }
        [Description("hD")]
        public int HomeDraws { get; set; }
        [Description("hL")]
        public int HomeLosses { get; set; }
        [Description("hF")]
        public int HomeGoalsFor { get; set; }
        [Description("hA")]
        public int HomeGoalsAgainst { get; set; }

        [Description("aP")]
        public int AwayPlayed => AwayWins + AwayDraws + AwayLosses;
        [Description("aW")]
        public int AwayWins { get; set; }
        [Description("aD")]
        public int AwayDraws { get; set; }
        [Description("aL")]
        public int AwayLosses { get; set; }
        [Description("aF")]
        public int AwayGoalsFor { get; set; }
        [Description("aA")]
        public int AwayGoalsAgainst { get; set; }

    }
}