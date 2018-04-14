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

        private int id = 1;
        public event PropertyChangedEventHandler PropertyChanged;
        
        public string Data
        {
            get
            {
                Debug.WriteLine("Get thread: " + Thread.CurrentThread.ManagedThreadId);
                return _dataValue;
            }
            set
            {
                Debug.WriteLine("Set thread: " + Thread.CurrentThread.ManagedThreadId);
                if (value != _dataValue)
                {
                    _dataValue = value;
                    OnPropertyChanged(nameof(Data));
                }
            }
        }

        public ObservableCollection<Standing> Standings
        {
            get
            {
                Debug.WriteLine("Get thread: " + Thread.CurrentThread.ManagedThreadId);
                return _standingsValue;
            }
            set
            {
                Debug.WriteLine("Set thread: " + Thread.CurrentThread.ManagedThreadId);
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
                Debug.WriteLine("Get thread: " + Thread.CurrentThread.ManagedThreadId);
                return _fixturePastsValue;
            }
            set
            {
                Debug.WriteLine("Set thread: " + Thread.CurrentThread.ManagedThreadId);
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
                Debug.WriteLine("Get thread: " + Thread.CurrentThread.ManagedThreadId);
                return _fixtureFuturesValue;
            }
            set
            {
                Debug.WriteLine("Set thread: " + Thread.CurrentThread.ManagedThreadId);
                if (value != _fixtureFuturesValue)
                {
                    _fixtureFuturesValue = value;
                    OnPropertyChanged(nameof(FixtureFutures));
                }
            }
        }

        public void FetchNewDataStandings(ExternalLeagueCode externalLeagueCode)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                Debug.WriteLine("Worker thread: " + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(TimeSpan.FromSeconds(5));
                string newValue = "Value " + Interlocked.Increment(ref id);
                Data = newValue;

                try
                {
                    //expensive (calls the rest api in perhaps a call stack that is non-async) - do on a background thread if possible
                    _competitionResultSingletonInstance = CompetitionResultSingleton.Instance;//This is slow, the rest is fast
                }
                catch (Exception)
                {
                    //Do nothing - the resultant null _competitionResultSingletonInstance is handled further down the call stack
                }

                var iEnumerableStandings = GetStandings(externalLeagueCode);
                Standings.Clear();
                foreach (var standing in iEnumerableStandings)
                {
                    Standings.Add(standing);
                }
            });
        }

        public void FetchNewDataFixturePasts(ExternalLeagueCode externalLeagueCode)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                Debug.WriteLine("Worker thread: " + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(TimeSpan.FromSeconds(5));
                string newValue = "Value " + Interlocked.Increment(ref id);
                Data = newValue;

                try
                {
                    //expensive (calls the rest api in perhaps a call stack that is non-async) - do on a background thread if possible
                    _competitionResultSingletonInstance = CompetitionResultSingleton.Instance;//This is slow, the rest is fast
                }
                catch (Exception)
                {
                    //Do nothing - the resultant null _competitionResultSingletonInstance is handled further down the call stack
                }

                var iEnumerableFixturePasts = GetFixturePasts(externalLeagueCode);
                FixturePasts.Clear();
                foreach (var fixturePast in iEnumerableFixturePasts)
                {
                    FixturePasts.Add(fixturePast);
                }
            });
        }


        public void FetchNewDataFixtureFutures(ExternalLeagueCode externalLeagueCode)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                Debug.WriteLine("Worker thread: " + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(TimeSpan.FromSeconds(5));
                string newValue = "Value " + Interlocked.Increment(ref id);
                Data = newValue;

                try
                {
                    //expensive (calls the rest api in perhaps a call stack that is non-async) - do on a background thread if possible
                    _competitionResultSingletonInstance = CompetitionResultSingleton.Instance;//This is slow, the rest is fast
                }
                catch (Exception)
                {
                    //Do nothing - the resultant null _competitionResultSingletonInstance is handled further down the call stack
                }

                var iEnumerableFixtureFutures = GetFixtureFutures(externalLeagueCode);
                FixtureFutures.Clear();
                foreach (var fixtureFuture in iEnumerableFixtureFutures)
                {
                    FixtureFutures.Add(fixtureFuture);
                }
            });
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                Debug.WriteLine("Event thread: " + Thread.CurrentThread.ManagedThreadId);
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
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
            {
                return new List<FixtureFuture> { new FixtureFuture { HomeName = "GetFixtureFutures internal error" } };
            }
        }

        private FootieDataGateway GetFootieDataGateway()
        {
            return new FootieDataGateway(_competitionResultSingletonInstance);
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