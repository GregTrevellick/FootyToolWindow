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
        private CompetitionResultSingleton _competitionResultSingletonInstance;
        private volatile string _dataValue = "Initial data";
        private int id = 1;
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
            catch (Exception)
            {
                //Do nothing - the resultant null _competitionResultSingletonInstance is handled further down the call stack
            }
        }

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

        public string Data
        {
            get
            {
                return _dataValue;
            }
            set
            {
                if (value != _dataValue)
                {
                    _dataValue = value;
                    OnPropertyChanged(nameof(Data));
                }
            }
        }

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

        public void FetchNewDataGeneric(ExternalLeagueCode externalLeagueCode, GridType gridType)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                //////Thread.Sleep(TimeSpan.FromSeconds(10));
                string newValue = "Value " + Interlocked.Increment(ref id);
                Data = newValue;

                var targetLeague = LeagueParents.Single(x => x.ExternalLeagueCode == externalLeagueCode);//gregt try/catch this?

                switch (gridType)
                {
                    case GridType.Unknown:
                        break;
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






//public void FetchNewDataStandings(ExternalLeagueCode externalLeagueCode, GridType gridType)
//{
//    ThreadPool.QueueUserWorkItem(delegate
//    {
//        Debug.WriteLine("Worker thread: " + Thread.CurrentThread.ManagedThreadId + " " + nameof(FetchNewDataStandings));
//        Thread.Sleep(TimeSpan.FromSeconds(5));
//        string newValue = "Value " + Interlocked.Increment(ref id);
//        Data = newValue;
//        //try
//        //{
//        //    //expensive (calls the rest api in perhaps a call stack that is non-async) - do on a background thread if possible
//        //    _competitionResultSingletonInstance = CompetitionResultSingleton.Instance;//This is slow, the rest is fast
//        //}
//        //catch (Exception)
//        //{
//        //    //Do nothing - the resultant null _competitionResultSingletonInstance is handled further down the call stack
//        //}
//        var iEnumerableStandings = GetStandings(externalLeagueCode);
//        IfLeagueParentsNotContainsThisLeagueThenAddIt(externalLeagueCode);
//        LeagueParents.FirstOrDefault(x => x.ExternalLeagueCode == externalLeagueCode).Standings.Clear();
//        foreach (var standing in iEnumerableStandings)
//        {
//            LeagueParents.Single(x => x.ExternalLeagueCode == externalLeagueCode).Standings.Add(standing);
//        }
//    });
//}

//public void FetchNewDataFixturePasts(ExternalLeagueCode externalLeagueCode, GridType gridType)
//{
//    ThreadPool.QueueUserWorkItem(delegate
//    {
//        Debug.WriteLine("Worker thread: " + Thread.CurrentThread.ManagedThreadId + " " + nameof(FetchNewDataFixturePasts));
//        Thread.Sleep(TimeSpan.FromSeconds(5));
//        string newValue = "Value " + Interlocked.Increment(ref id);
//        Data = newValue;
//        //try
//        //{
//        //    //expensive (calls the rest api in perhaps a call stack that is non-async) - do on a background thread if possible
//        //    _competitionResultSingletonInstance = CompetitionResultSingleton.Instance;//This is slow, the rest is fast
//        //}
//        //catch (Exception)
//        //{
//        //    //Do nothing - the resultant null _competitionResultSingletonInstance is handled further down the call stack
//        //}
//        var iEnumerableFixturePasts = GetFixturePasts(externalLeagueCode);
//        IfLeagueParentsNotContainsThisLeagueThenAddIt(externalLeagueCode);
//        LeagueParents.FirstOrDefault(x => x.ExternalLeagueCode == externalLeagueCode).FixturePasts.Clear();
//        foreach (var fixturePast in iEnumerableFixturePasts)
//        {
//            LeagueParents.Single(x => x.ExternalLeagueCode == externalLeagueCode).FixturePasts.Add(fixturePast);
//        }
//    });
//}

//public void FetchNewDataFixtureFutures(ExternalLeagueCode externalLeagueCode, GridType gridType)
//{
//    ThreadPool.QueueUserWorkItem(delegate
//    {
//        Debug.WriteLine("Worker thread: " + Thread.CurrentThread.ManagedThreadId + " " + nameof(FetchNewDataFixtureFutures));
//        Thread.Sleep(TimeSpan.FromSeconds(5));
//        string newValue = "Value " + Interlocked.Increment(ref id);
//        Data = newValue;
//        //try
//        //{
//        //    //expensive (calls the rest api in perhaps a call stack that is non-async) - do on a background thread if possible
//        //    _competitionResultSingletonInstance = CompetitionResultSingleton.Instance;//This is slow, the rest is fast
//        //}
//        //catch (Exception)
//        //{
//        //    //Do nothing - the resultant null _competitionResultSingletonInstance is handled further down the call stack
//        //}
//        var iEnumerableFixtureFutures = GetFixtureFutures(externalLeagueCode);
//        IfLeagueParentsNotContainsThisLeagueThenAddIt(externalLeagueCode);
//        LeagueParents.FirstOrDefault(x => x.ExternalLeagueCode == externalLeagueCode).FixtureFutures.Clear();
//        foreach (var fixtureFuture in iEnumerableFixtureFutures)
//        {
//            LeagueParents.Single(x => x.ExternalLeagueCode == externalLeagueCode).FixtureFutures.Add(fixtureFuture);
//        }
//    });
//}




//                Debug.WriteLine("Get thread: " + Thread.CurrentThread.ManagedThreadId + " " + nameof(Data));




//LeagueParents.Add(new LeagueParent
//                {
//                    ExternalLeagueCode = externalLeagueCode,
//                    Standings = Standings,/////////////////////////////////////////////////////////////////////////////////////////////_standingsValue,
//                    FixturePasts = FixturePasts,///////////////////////////////////////////////////////////////////////////////////////_fixturePastsValue,
//                    FixtureFutures = FixtureFutures,///////////////////////////////////////////////////////////////////////////////////_fixtureFuturesValue,
//                });