// // Copyright (c) Microsoft. All rights reserved.
// // Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;

namespace HierarchicalDataTemplate
{
    public class ListLeagueList : List<League>
    {
        public ListLeagueList()
        {
            League l;
            League2 league2;

            //gregt
            Add(l = new League("League A"));
            l.League2s.Add((league2 = new League2("Premier league")));
            league2.DataArtefacts.Add(new DataArtefact("Standings"){ActualData = { "man city", "chelsea", "man utd"}});
            league2.DataArtefacts.Add(new DataArtefact("Recent results") {
                ActualData = {
                    "chelsea 1-0 man city", "liverpool 3-2 everton"}
                });
            league2.DataArtefacts.Add(new DataArtefact("Upcoming fixtures")
            {
                ActualData = {
                "dd/mm/yy team1 v team2","dd/mm/yy team1 v team2","dd/mm/yy team1 v team2"}});
            //d.Teams.Add(new Team("Team IV"));
            //d.Teams.Add(new Team("Team V"));
            l.League2s.Add((league2 = new League2("Budesliga 1")));
            league2.DataArtefacts.Add(new DataArtefact("Standings") { ActualData = { "schalke", "hamburg", "berlin utd" } });
            league2.DataArtefacts.Add(new DataArtefact("Recent results")
            {
                ActualData = {
                    "schalke 1-0 hamburg", "berlin 3-2 hamburg" }});
            league2.DataArtefacts.Add(new DataArtefact("Upcoming fixtures")
            {
                ActualData = {
                "dd/mm/yy team1 v team2", "dd/mm/yy team1 v team2", "dd/mm/yy team1 v team2" }});
            //d.Teams.Add(new Team("Team Green"));
            //d.Teams.Add(new Team("Team Orange"));
            l.League2s.Add((league2 = new League2("Budesliga 2")));
            league2.DataArtefacts.Add(new DataArtefact("Standings") { ActualData = { "munich", "bayern", "colonge" } });
            league2.DataArtefacts.Add(new DataArtefact("Recent results")
            {
                ActualData = {
                    "munich 1-0 cologne", "bayern 3-2 munich" }});
            league2.DataArtefacts.Add(new DataArtefact("Upcoming fixtures")
            {
                ActualData = {
                    "dd/mm/yy team1 v team2", "dd/mm/yy team1 v team2", "dd/mm/yy team1 v team2" }});
            //d.Teams.Add(new Team("Team South"));
            //Add(l = new League("League B"));
            //l.Divisions.Add((d = new Division("Division A")));
            //d.Teams.Add(new Team("Team 1"));
            //d.Teams.Add(new Team("Team 2"));
            //d.Teams.Add(new Team("Team 3"));
            //d.Teams.Add(new Team("Team 4"));
            //d.Teams.Add(new Team("Team 5"));
            //l.Divisions.Add((d = new Division("Division B")));
            //d.Teams.Add(new Team("Team Diamond"));
            //d.Teams.Add(new Team("Team Heart"));
            //d.Teams.Add(new Team("Team Club"));
            //d.Teams.Add(new Team("Team Spade"));
            //l.Divisions.Add((d = new Division("Division C")));
            //d.Teams.Add(new Team("Team Alpha"));
            //d.Teams.Add(new Team("Team Beta"));
            //d.Teams.Add(new Team("Team Gamma"));
            //d.Teams.Add(new Team("Team Delta"));
            //d.Teams.Add(new Team("Team Epsilon"));
        }

        public League this[string name] => this.FirstOrDefault(l => l.Name == name);
    }
}