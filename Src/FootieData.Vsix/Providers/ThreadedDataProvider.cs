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

namespace FootieData.Vsix.Providers
{
    public class ThreadedDataProvider : INotifyPropertyChanged
    {       
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly string _zeroFixturePasts = $"No results available for the past {CommonConstants.DaysCount} days";//TODO move to EntityConstants
        private readonly string _zeroFixtureFutures = $"No fixtures available for the next {CommonConstants.DaysCount} days";//TODO move to EntityConstants
        private const string RequestLimitReached = "You reached your request limit. W";

        public ThreadedDataProvider(ExternalLeagueCode externalLeagueCode)
        {
            InitializeCompetitionResultSingletonInstance();
            AddTargetLeagueToLeagueParents(externalLeagueCode);
        }

        private void InitializeCompetitionResultSingletonInstance()
        {
            try
            {
                _competitionResultSingletonInstance = CompetitionResultSingleton.Instance;
                //throw new Exception(); //for debugging
            }
            catch (Exception ex)
            {
                //Do nothing - the resultant null _competitionResultSingletonInstance is handled further down the call stack
            }
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

        public void FetchDataFromGateway(ExternalLeagueCode externalLeagueCode, GridType gridType)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                //Debug.WriteLine("Get thread: " + Thread.CurrentThread.ManagedThreadId);
                //Thread.Sleep(TimeSpan.FromSeconds(10));//for debugging

                var leagueParent = new LeagueParent();

                try
                {
                    //throw new Exception(); //for debugging
                    leagueParent = LeagueParents.Single(x => x.ExternalLeagueCode == externalLeagueCode);

                    switch (gridType)
                    {
                        case GridType.Standing:
                            var iEnumerableStandings = GetStandings(externalLeagueCode);
                            leagueParent.Standings.Clear();
                            var standingsList = iEnumerableStandings.ToList();
                            if (standingsList.Any(x => x.Team != null && x.Team.StartsWith(RequestLimitReached)))
                            {
                                var politeRequestLimitReached = standingsList.First(x => x.Team.StartsWith(RequestLimitReached)).Team.Replace(RequestLimitReached, EntityConstants.PoliteRequestLimitReached);
                                leagueParent.Standings.Add(new Standing { PoliteError = EntityConstants.PoliteRequestLimitReached });
                            }
                            else
                            {
                                if (standingsList.Any(x => x.Team != null && x.Team.StartsWith(EntityConstants.PotentialTimeout)))
                                {
                                    leagueParent.Standings.Add(new Standing { PoliteError = EntityConstants.PotentialTimeout });
                                }
                                else
                                {
                                    foreach (var standing in iEnumerableStandings)
                                    {
                                        leagueParent.Standings.Add(standing);
                                    }
                                }
                            }
                            break;
                        case GridType.Result:
                            var iEnumerableFixturePasts = GetFixturePasts(externalLeagueCode);
                            leagueParent.FixturePasts.Clear();
                            var resultsList = iEnumerableFixturePasts.ToList();
                            if (resultsList.Any())
                            {
                                if (resultsList.Any(x => x.HomeName != null && x.HomeName.StartsWith(RequestLimitReached)))
                                {
                                    var politeRequestLimitReached = resultsList.First(x => x.HomeName.StartsWith(RequestLimitReached)).HomeName.Replace(RequestLimitReached, EntityConstants.PoliteRequestLimitReached);
                                    leagueParent.FixturePasts.Add(new FixturePast { PoliteError = EntityConstants.PoliteRequestLimitReached });
                                }
                                else
                                {
                                    if (resultsList.Any(x => x.HomeName != null && x.HomeName.StartsWith(EntityConstants.PotentialTimeout)))
                                    {
                                        leagueParent.FixturePasts.Add(new FixturePast { PoliteError = EntityConstants.PotentialTimeout });
                                    }
                                    else
                                    {
                                        foreach (var fixturePast in iEnumerableFixturePasts)
                                        {
                                            leagueParent.FixturePasts.Add(fixturePast);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                leagueParent.FixturePasts.Add(new FixturePast { PoliteError = _zeroFixturePasts });
                            }
                            break;
                        case GridType.Fixture:
                            var iEnumerableFixtureFutures = GetFixtureFutures(externalLeagueCode);
                            leagueParent.FixtureFutures.Clear();
                            var fixturesList = iEnumerableFixtureFutures.ToList();
                            if (fixturesList.Any())
                            {
                                if (fixturesList.Any(x => x.HomeName != null && x.HomeName.StartsWith(RequestLimitReached)))
                                {
                                    var politeRequestLimitReached = fixturesList.First(x => x.HomeName.StartsWith(RequestLimitReached)).HomeName.Replace(RequestLimitReached, EntityConstants.PoliteRequestLimitReached);
                                    leagueParent.FixtureFutures.Add(new FixtureFuture { PoliteError = EntityConstants.PoliteRequestLimitReached });
                                }
                                else
                                {
                                    if (fixturesList.Any(x => x.HomeName != null && x.HomeName.StartsWith(EntityConstants.PotentialTimeout)))
                                    {
                                        leagueParent.FixtureFutures.Add(new FixtureFuture { PoliteError = EntityConstants.PotentialTimeout });
                                    }
                                    else
                                    {
                                        foreach (var fixtureFuture in iEnumerableFixtureFutures)
                                        {
                                            leagueParent.FixtureFutures.Add(fixtureFuture);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                leagueParent.FixtureFutures.Add(new FixtureFuture { PoliteError = _zeroFixtureFutures });
                            }
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    leagueParent = LeagueParents.FirstOrDefault();
                    leagueParent.ExternalLeagueCode = externalLeagueCode;
                    switch (gridType)
                    {
                        case GridType.Standing:
                            leagueParent.Standings.Clear();
                            leagueParent.Standings.Add(new Standing { PoliteError = EntityConstants.UnexpectedErrorOccured });
                            break;
                        case GridType.Result:
                            leagueParent.FixturePasts.Clear();
                            leagueParent.FixturePasts.Add(new FixturePast { PoliteError = EntityConstants.UnexpectedErrorOccured });
                            break;
                        case GridType.Fixture:
                            leagueParent.FixtureFutures.Clear();
                            leagueParent.FixtureFutures.Add(new FixtureFuture { PoliteError = EntityConstants.UnexpectedErrorOccured });
                            break;
                    }
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

        #region Private members
        private CompetitionResultSingleton _competitionResultSingletonInstance;
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

        #region Public members
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

        #region Get data from gateway
        private IEnumerable<Standing> GetStandings(ExternalLeagueCode externalLeagueCode)
        {
            try
            {
                //throw new Exception();//for debugging
                var gateway = GetFootieDataGateway();
                var result = gateway.GetFromClientStandings(externalLeagueCode.ToString());
                return result;
            }
            catch (Exception ex)
            {
                return new List<Standing> { new Standing { PoliteError = $"{EntityConstants.UnexpectedErrorOccured} ({nameof(GetStandings)})" } };
            }
        }

        private IEnumerable<FixturePast> GetFixturePasts(ExternalLeagueCode externalLeagueCode)
        {
            try
            {
                //throw new Exception();//for debugging
                var gateway = GetFootieDataGateway();
                var result = gateway.GetFromClientFixturePasts(externalLeagueCode.ToString(), $"p{CommonConstants.DaysCount}");
                return result;
            }
            catch (Exception ex)
            {
                return new List<FixturePast> { new FixturePast { PoliteError = $"{EntityConstants.UnexpectedErrorOccured} ({nameof(GetFixturePasts)})" } };
            }
        }

        private IEnumerable<FixtureFuture> GetFixtureFutures(ExternalLeagueCode externalLeagueCode)
        {
            try
            {
                //throw new Exception();//for debugging
                var gateway = GetFootieDataGateway();
                var result = gateway.GetFromClientFixtureFutures(externalLeagueCode.ToString(), $"n{CommonConstants.DaysCount}");
                return result;
            }
            catch (Exception ex)
            {
                return new List<FixtureFuture> { new FixtureFuture { PoliteError = $"{EntityConstants.UnexpectedErrorOccured} ({nameof(GetFixtureFutures)})" } };
            }
        }

        private FootieDataGateway GetFootieDataGateway()
        {
            return new FootieDataGateway(_competitionResultSingletonInstance);
        }
        #endregion
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
//    }
//}
