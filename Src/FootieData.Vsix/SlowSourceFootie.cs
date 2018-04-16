using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using FootieData.Gateway;
using FootieData.Entities;
using System.Collections.ObjectModel;
using FootieData.Entities.ReferenceData;
using FootieData.Common;

namespace FootieData.Vsix
{
    public class SlowSourceFootie : INotifyPropertyChanged
    {       
        public event PropertyChangedEventHandler PropertyChanged;

        public SlowSourceFootie(ExternalLeagueCode externalLeagueCode)
        {
            InitializeCompetitionResultSingletonInstance();
            AddTargetLeagueToLeagueParents(externalLeagueCode);
        }

        private void InitializeCompetitionResultSingletonInstance()
        {
            try
            {
                //below is done in toolwindow1control.xaml.cs too, but is needed in both places
                _competitionResultSingletonInstance = CompetitionResultSingleton.Instance;//This is slow, the rest is fast
            }
            catch (Exception ex)
            {
                //Do nothing - the resultant null _competitionResultSingletonInstance is handled further down the call stack
            }
        }

        #region privates
        private CompetitionResultSingleton _competitionResultSingletonInstance;
        //private volatile string _dataValue = "Initial data";
        //private int id = 1;
        private volatile ObservableCollection<LeagueParent> _leagueParentsValue = new AsyncObservableCollection<LeagueParent>();

        private volatile ObservableCollection<Standing> _standingsValue = new AsyncObservableCollection<Standing>
        {
            new Standing { Team = "Loading..." }
        };

        private volatile ObservableCollection<FixturePast> _fixturePastsValue = new AsyncObservableCollection<FixturePast>
        {
            new FixturePast { HomeName = "Loading..." }
        };

        private volatile ObservableCollection<FixtureFuture> _fixtureFuturesValue = new AsyncObservableCollection<FixtureFuture>
        {
            new FixtureFuture { HomeName = "Loading..." }
        };
        #endregion

        #region getters & setters
        //public string Data
        //{
        //    get
        //    {
        //        return _dataValue;
        //    }
        //    set
        //    {
        //        if (value != _dataValue)
        //        {
        //            _dataValue = value;
        //            OnPropertyChanged(nameof(Data));
        //        }
        //    }
        //}

        public ObservableCollection<LeagueParent> LeagueParents
        {
            get
            {
                return _leagueParentsValue;
            }
            set
            {
                if (value != _leagueParentsValue)
                {
                    _leagueParentsValue = value;
                    OnPropertyChanged(nameof(LeagueParents));
                }
            }
        }

        public ObservableCollection<Standing> Standings
        {
            get
            {
                return _standingsValue;
            }
            set
            {
                if (value != _standingsValue)
                {
                    _standingsValue = value;
                    OnPropertyChanged(nameof(Standings));
                }
            }
        }

        public ObservableCollection<FixturePast> FixturePasts
        {
            get
            {
                return _fixturePastsValue;
            }
            set
            {
                if (value != _fixturePastsValue)
                {
                    _fixturePastsValue = value;
                    OnPropertyChanged(nameof(FixturePasts));
                }
            }
        }

        public ObservableCollection<FixtureFuture> FixtureFutures
        {
            get
            {
                return _fixtureFuturesValue;
            }
            set
            {
                if (value != _fixtureFuturesValue)
                {
                    _fixtureFuturesValue = value;
                    OnPropertyChanged(nameof(FixtureFutures));
                }
            }
        }
        #endregion

        public void FetchNewDataGeneric(ExternalLeagueCode externalLeagueCode, GridType gridType)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                //Thread.Sleep(TimeSpan.FromSeconds(10));
                //string newValue = "Value " + Interlocked.Increment(ref id);
                //Data = newValue;

                var targetLeague = LeagueParents.Single(x => x.ExternalLeagueCode == externalLeagueCode);//gregt try/catch this?

                switch (gridType)
                {
                    case GridType.Standing:
                        var iEnumerableStandings = GetStandings(externalLeagueCode);
                        targetLeague.Standings.Clear();
                        foreach (var standing in iEnumerableStandings)
                        {
                            targetLeague.Standings.Add(standing);
                        }
                        break;
                    case GridType.Result:
                        var iEnumerableFixturePasts = GetFixturePasts(externalLeagueCode);
                        targetLeague.FixturePasts.Clear();
                        foreach (var fixturePast in iEnumerableFixturePasts)
                        {
                            targetLeague.FixturePasts.Add(fixturePast);
                        }
                        break;
                    case GridType.Fixture:
                        var iEnumerableFixtureFutures = GetFixtureFutures(externalLeagueCode);
                        targetLeague.FixtureFutures.Clear();
                        foreach (var fixtureFuture in iEnumerableFixtureFutures)
                        {
                            targetLeague.FixtureFutures.Add(fixtureFuture);
                        }
                        break;
                    default:
                        break;
                }
            });
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region gateway gets
        private IEnumerable<Standing> GetStandings(ExternalLeagueCode externalLeagueCode)
        {
            try
            {
                var gateway = GetFootieDataGateway();
                var result = gateway.GetFromClientStandings(externalLeagueCode.ToString());
                return result;
            }
            catch (Exception ex)
            {
                return new List<Standing> { new Standing { Team = "GetStandings internal error" } };
            }
        }

        private IEnumerable<FixturePast> GetFixturePasts(ExternalLeagueCode externalLeagueCode)
        {
            try
            {
                var gateway = GetFootieDataGateway();
                var result = gateway.GetFromClientFixturePasts(externalLeagueCode.ToString(), $"p{CommonConstants.DaysCount}");
                return result;
            }
            catch (Exception ex)
            {
                return new List<FixturePast> { new FixturePast { HomeName = "GetFixturePasts internal error" } };
            }
        }

        private IEnumerable<FixtureFuture> GetFixtureFutures(ExternalLeagueCode externalLeagueCode)
        {
            try
            {
                var gateway = GetFootieDataGateway();
                var result = gateway.GetFromClientFixtureFutures(externalLeagueCode.ToString(), $"n{CommonConstants.DaysCount}");
                return result;
            }
            catch (Exception ex)
            {
                return new List<FixtureFuture> { new FixtureFuture { HomeName = "GetFixtureFutures internal error" } };
            }
        }

        private FootieDataGateway GetFootieDataGateway()
        {
            return new FootieDataGateway(_competitionResultSingletonInstance);
        }
        #endregion

        private void AddTargetLeagueToLeagueParents(ExternalLeagueCode externalLeagueCode)
        {
            if (LeagueParents.Count(x => x.ExternalLeagueCode == externalLeagueCode) == 0)
            {
                LeagueParents.Add(new LeagueParent
                {
                    ExternalLeagueCode = externalLeagueCode,
                    Standings = Standings,
                    FixturePasts = FixturePasts,
                    FixtureFutures = FixtureFutures,
                });
            }
        }
    }
}







//public async Task<IEnumerable<Standing>> GetStandingsAsync(ExternalLeagueCode externalLeagueCode)
//{
//    try
//    {
//        var theTask = Task.Run(() =>
//        {
//            var gateway = GetFootieDataGateway();
//            var result = gateway.GetFromClientStandings(externalLeagueCode.ToString());
//            return result;
//        });
//        await Task.WhenAll(theTask);
//        return theTask.Result;
//    }
//    catch (Exception)
//    {
//        return new List<Standing> { new Standing { Team = "GetStandingsAsync internal error" } };
//    }
//}



// Debug.WriteLine("Get thread: " + Thread.CurrentThread.ManagedThreadId + " " + nameof(Data));