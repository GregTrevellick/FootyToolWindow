using System;
using System.Collections.Generic;
using System.Linq;

namespace HierarchicalDataTemplate
{
    public class ListLeagueResponse : List<LeagueResponse>
    {

        public ListLeagueResponse()
        {

            Add(new LeagueResponse("Premier league")
            {
                ActualDataStandings = new List<Standing>()
                {
                    new Standing(){Team="mancity", Rank = 1, Points=50},
                    new Standing(){Team="chelsea", Rank = 2, Points = 40},
                    new Standing(){Team="manutd", Rank = 3, Points=30},
                },
                ActualDataResults = new List<Fixture>()
                {
                    new Fixture{AwayTeamName="mancity", HomeTeamName="chelsea", Date = new DateTime(2017,02,03),Result=new Result{goalsAwayTeam=0,goalsHomeTeam=1}},
                    new Fixture{AwayTeamName="chelsea", HomeTeamName="mancity", Date = new DateTime(2017,02,04),Result=new Result{goalsAwayTeam=0,goalsHomeTeam=1}},
                    new Fixture{AwayTeamName="manutd", HomeTeamName="mancity", Date = new DateTime(2017,02,05),Result=new Result{goalsAwayTeam=0,goalsHomeTeam=0}},
                },
                ActualDataFixtures = new List<Fixture>()
                {
                    new Fixture{AwayTeamName="chelsea", HomeTeamName="mancity", Date = new DateTime(2017,02,03),Result=new Result{goalsAwayTeam=1,goalsHomeTeam=1}},
                    new Fixture{AwayTeamName="chelsea", HomeTeamName="mancity", Date = new DateTime(2017,03,03),Result=new Result{goalsAwayTeam=2,goalsHomeTeam=1}},
                    new Fixture{AwayTeamName="chelsea", HomeTeamName="mancity", Date = new DateTime(2017,04,03),Result=new Result{goalsAwayTeam=0,goalsHomeTeam=1}},                },
            });
            Add(new LeagueResponse("Budesliga 1")
            {
                ActualDataStandings = new List<Standing>()
                {
                    new Standing(){Team="schalke", Rank = 1, Points = 55 },
                    new Standing(){Team="cologne", Rank = 2, Points = 54 },
                    new Standing(){Team="fc 1066", Rank = 3, Points = 10 },
                },
                ActualDataResults = new List<Fixture>()
                {
                    new Fixture{AwayTeamName="schalke", HomeTeamName="cologne", Date = new DateTime(2017,02,03),Result=new Result{goalsAwayTeam=0,goalsHomeTeam=1}},
                    new Fixture{AwayTeamName="cologne", HomeTeamName="schalke", Date = new DateTime(2017,04,03),Result=new Result{goalsAwayTeam=0,goalsHomeTeam=1}},
                    new Fixture{AwayTeamName="schalke", HomeTeamName="cologne", Date = new DateTime(2017,05,03),Result=new Result{goalsAwayTeam=3,goalsHomeTeam=1}},                },
                ActualDataFixtures = new List<Fixture>()
                {
                    new Fixture{AwayTeamName="cologne", HomeTeamName="schalke", Date = new DateTime(2017,02,03),Result=new Result{goalsAwayTeam=0,goalsHomeTeam=1}},                },
            });
            Add(new LeagueResponse("Budesliga 2")
            {
                ActualDataStandings = new List<Standing>
                {
                    new Standing(){Team="munich", Rank = 1, Points = 50 },
                    new Standing(){Team="hamburg", Rank = 2, Points = 45 },
                    new Standing(){Team="berlin", Rank = 3, Points = 30 },
                },
                ActualDataResults = new List<Fixture>
                {
                    new Fixture{AwayTeamName="munich", HomeTeamName="berlin", Date = new DateTime(2017,02,03),Result=new Result{goalsAwayTeam=3,goalsHomeTeam=1}},
                    new Fixture{AwayTeamName="munich", HomeTeamName="hamburg", Date = new DateTime(2017,02,03),Result=new Result{goalsAwayTeam=2,goalsHomeTeam=1}},                },
                ActualDataFixtures = new List<Fixture>
                {
                    new Fixture{AwayTeamName="berlin", HomeTeamName="munich", Date = new DateTime(2017,02,04),Result=new Result{goalsAwayTeam=8,goalsHomeTeam=1}},
                    new Fixture{AwayTeamName="hamburg", HomeTeamName="munich", Date = new DateTime(2017,02,05),Result=new Result{goalsAwayTeam=5,goalsHomeTeam=1}},                },
            });
        }

     }
}