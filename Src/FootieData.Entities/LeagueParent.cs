using FootieData.Entities.ReferenceData;
using System.Collections.ObjectModel;

namespace FootieData.Entities
{
    public class LeagueParent
    {
        public volatile ExternalLeagueCode ExternalLeagueCode;
        public volatile ObservableCollection<Standing> Standings;
        public volatile ObservableCollection<FixturePast> FixturePasts;
        public volatile ObservableCollection<FixtureFuture> FixtureFutures;
    }
}
