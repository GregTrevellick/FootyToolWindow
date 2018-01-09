using System.Collections.Generic;
using System.Linq;

namespace HierarchicalDataTemplate
{
    public class ListLeagueList : List<League2>
    {

        public ListLeagueList()
        {

            Add(new League2("Premier league")
            {
                ActualDataStandings = new List<Stringer>() { new Stringer(){Aaa="man city", Bbb="chelsea", Ccc="man utd"} },
                ActualDataResults = new List<Stringer>() { new Stringer(){Aaa="chelsea 1-0 man city", Bbb="liverpool 3-2 everton"} },
                ActualDataFixtures = new List<Stringer>() { new Stringer(){Aaa="dd/mm/yy team1 v team2", Bbb="dd/mm/yy team1 v team2", Ccc="dd/mm/yy team1 v team2"} },
            });
            Add(new League2("Budesliga 1")
            {
                ActualDataStandings = new List<Stringer>() { new Stringer(){Aaa="schalke", Bbb="hamburg", Ccc="berlin utd" } },
                ActualDataResults = new List<Stringer>() { new Stringer(){Aaa="schalke 1-0 hamburg", Bbb="berlin 3-2 hamburg" } },
                ActualDataFixtures = new List<Stringer>() { new Stringer(){Aaa="dd/mm/yy team1 v team2", Bbb="dd/mm/yy team1 v team2", Ccc="dd/mm/yy team1 v team2" }},
            });
            Add(new League2("Budesliga 2")
            {
                ActualDataStandings = new List<Stringer> { new Stringer(){Aaa="munich", Bbb="bayern", Ccc="colonge" } },
                ActualDataResults = new List<Stringer> { new Stringer(){Aaa="munich 1-0 cologne", Bbb="bayern 3-2 munich"} },
                ActualDataFixtures = new List<Stringer> { new Stringer(){Aaa="dd/mm/yy team1 v team2", Bbb="dd/mm/yy team1 v team2", Ccc="dd/mm/yy team1 v team2"} },
            });
        }

     }
}