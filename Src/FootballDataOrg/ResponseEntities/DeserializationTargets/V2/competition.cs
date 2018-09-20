using System;

namespace FootballDataOrg.ResponseEntities.DeserializationTargets.V2.lges
{
    public class Rootobject
    {
        public int count { get; set; }
        public Filters filters { get; set; }
        public Competition[] competitions { get; set; }
    }

    public class Filters
    {
    }

    public class Competition
    {
        public int id { get; set; }
        public Area area { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string plan { get; set; }
        public Currentseason currentSeason { get; set; }
        public int numberOfAvailableSeasons { get; set; }
        public DateTime lastUpdated { get; set; }
    }

    public class Area
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Currentseason
    {
        public int id { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int? currentMatchday { get; set; }
    }
}
