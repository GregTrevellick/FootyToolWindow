using FootieData.Entities.ReferenceData;
using System.Collections.ObjectModel;

namespace FootieData.Entities
{
    public class LeagueParent
    {
        public volatile ExternalLeagueCode ExternalLeagueCode;// { get; set; }
        public volatile ObservableCollection<Standing> Standings;// { get; set; }
        public volatile ObservableCollection<FixturePast> FixturePasts;// { get; set; }
        public volatile ObservableCollection<FixtureFuture> FixtureFutures;// { get; set; }
    }
}
