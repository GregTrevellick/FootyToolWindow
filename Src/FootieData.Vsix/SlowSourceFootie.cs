using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using FootieData.Gateway;
using FootieData.Entities;
using System.Collections.ObjectModel;

namespace FootieData.Vsix
{
    public class SlowSourceFootie : INotifyPropertyChanged
    {
        private volatile string _dataValue = "Initial data";

        //private volatile ObservableCollection<Standing> _standingsValue = new ObservableCollection<Standing>
        private volatile ObservableCollection<Standing> _standingsValue = new AsyncObservableCollection<Standing>
            {
                new Standing { Team = "united", For = 12, Against = 0 },
                new Standing { Team = "rovers", For = 56, Against = 0 },
                new Standing { Team = "city", For = 99, Against = 0 }
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

        public void FetchNewData()
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                Debug.WriteLine("Worker thread: " + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(TimeSpan.FromSeconds(10));
                string newValue = "Value " + Interlocked.Increment(ref id);
                Data = newValue;

                Random r = new Random();
                var randF = r.Next(5, 10);
                var randA = r.Next(1, 5);
                Standings.Add(new Standing { Team = "fetch slow f.c.", For = randF, Against = randA });
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
    }
}
