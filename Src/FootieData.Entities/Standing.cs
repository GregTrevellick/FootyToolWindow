using System.ComponentModel;

namespace FootieData.Entities
{
    public class Standing : EntityBase
    {
        //DO NOT RE-ORDER: this is the UI order

        //public string CrestURI { get; set; }

        [Description("")]
        public int Rank { get; set; }
        [Description("Club")]
        public string Team { get; set; }
        [Description("P")]
        public int Played { get; set; }
        [Description("W")]
        public int Wins => HomeWins + AwayWins;
        [Description("D")]
        public int Draws => HomeDraws + AwayDraws;
        [Description("L")]
        public int Losses => HomeLosses + AwayLosses;
        [Description("F")]
        public int For { get; set; }
        [Description("A")]
        public int Against { get; set; }
        [Description("GD")]
        public int Diff { get; set; }
        [Description("PTs")]
        public int Points { get; set; }

        [Description("Home")]
        public int HomePlayed => HomeWins + HomeDraws + HomeLosses;
        [Description("W")]
        public int HomeWins { get; set; }
        [Description("D")]
        public int HomeDraws { get; set; }
        [Description("L")]
        public int HomeLosses { get; set; }
        [Description("F")]
        public int HomeGoalsFor { get; set; }
        [Description("A")]
        public int HomeGoalsAgainst { get; set; }
        [Description("GD")]
        public int HomeGoalDiff => HomeGoalsFor - HomeGoalsAgainst;
        [Description("PTs")]
        public int HomePoints => 3 * HomeWins + HomeDraws;

        [Description("Away")]
        public int AwayPlayed => AwayWins + AwayDraws + AwayLosses;
        [Description("W")]
        public int AwayWins { get; set; }
        [Description("D")]
        public int AwayDraws { get; set; }
        [Description("L")]
        public int AwayLosses { get; set; }
        [Description("F")]
        public int AwayGoalsFor { get; set; }
        [Description("A")]
        public int AwayGoalsAgainst { get; set; }
        [Description("GD")]
        public int AwayGoalDiff => AwayGoalsFor - AwayGoalsAgainst;
        [Description("PTs")]
        public int AwayPoints => 3 * AwayWins + AwayDraws;
    }
}