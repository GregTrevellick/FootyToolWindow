using System;
using System.ComponentModel;
using System.Windows.Media;
//gregt stuff to be deleted
//using FootieData.Entities.CustomAttributes;

namespace FootieData.Entities
{
    public class Standing
    {
        //DO NOT RE-ORDER: this is the UI order

        [Description("")]
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
        [Description("GD")]
        public int Diff { get; set; }
        [Description("PTs")]
        public int Points { get; set; }

        //gregt stuff to be deleted

        //[ColumnHeaderForegroundColor(Brushes.Red)]
        //[Home(true)]
        [Description("Home Pl")]
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

        [Description("Away Pl")]
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

    }
}