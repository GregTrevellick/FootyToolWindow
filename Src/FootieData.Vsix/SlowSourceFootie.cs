using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using FootieData.Gateway;
using FootieData.Entities;
using System.Collections.ObjectModel;
using FootieData.Entities.ReferenceData;

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

        public void FetchNewData(ExternalLeagueCode externalLeagueCode)
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

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                Debug.WriteLine("Event thread: " + Thread.CurrentThread.ManagedThreadId);
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public IEnumerable<Standing> GetStandings(ExternalLeagueCode externalLeagueCode)
        {
            try
            {
                var gateway = GetFootieDataGateway();
                var result = gateway.GetFromClientStandings(externalLeagueCode.ToString());
                return result;
            }
            catch (Exception)
            {
                return new List<Standing> { new Standing { Team = "GetStandingsAsync internal error" } };
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