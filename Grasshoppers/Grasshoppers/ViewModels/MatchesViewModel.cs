using Grasshoppers.Helpers;
using Grasshoppers.Initializers;
using Grasshoppers.Interfaces;
using Grasshoppers.Models;
using Grasshoppers.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Grasshoppers.ViewModels
{
    public class MatchesViewModel : INotifyPropertyChanged
    {
        private Match _selectedMatch = new Match();
        private Goal _newGoal;
        private ObservableCollection<Location> _allLocations;
        private ObservableCollection<League> _allLeagues;
        private ObservableCollection<Category> _allCategories;
        private ObservableCollection<Player> _allPlayersInCategoryOfSelectedMatch;

        public MatchesViewModel()
        {
            _isBusy = false;
        }

        public bool IsAdmin => Settings.AdminLevel == 1;

        public INavigation Navigation { get; set; }

        public Goal NewGoal
        {
            get { return _newGoal; }
            set
            {
                if (value != null)
                {
                    _newGoal = value;
                }
            }
        }

        public int NewGoalPeriod
        {
            get { return NewGoal.Period; }
            set
            {
                if (NewGoal.Period != value)
                {
                    NewGoal.Period = value;
                    OnPropertyChanged();
                }
            }
        }

        public TimeSpan NewGoalTime
        {
            get { return NewGoal.Time; }
            set
            {
                NewGoal.Time = value;
                OnPropertyChanged();
            }
        }

        public Player NewGoalScorer
        {
            get { return NewGoal.Scorer; }
            set
            {
                if (NewGoal.Scorer != value)
                {
                    NewGoal.Scorer = value;
                    OnPropertyChanged();
                }
            }
        }

        public Player NewGoalAssistent
        {
            get { return NewGoal.Assistent; }
            set
            {
                if (NewGoal.Assistent != value)
                {
                    NewGoal.Assistent = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task InitializeAllPlayersInCategoryOfSelectedMatch()
        {
            IsBusy = true;
            AllPlayersInCategoryOfSelectedMatch = await Initializer.InitializeAllPlayersInCategoryOfSelectedMatchAsync(_selectedMatch.Category.Id);
            IsBusy = false;
        }

        public ObservableCollection<Player> AllPlayersInCategoryOfSelectedMatch
        {
            get { return _allPlayersInCategoryOfSelectedMatch; }
            set
            {
                if (_allPlayersInCategoryOfSelectedMatch != value) // only used when value is changed, not when just set
                {
                    _allPlayersInCategoryOfSelectedMatch = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        
        public async Task RefreshSelectedMatch()
        {
            IsBusy = true;
            var matchesServices = new MatchesServices();
            var match = await matchesServices.GetMatchById(_selectedMatch.Id);
            if (match != null && match.GrassGoals != null && match.OpponentGoals != null)
            {
                SelectedMatch = match;
                SelectedMatchGrassGoals = _selectedMatch.GrassGoals;
                SelectedMatchOpponentGoals = _selectedMatch.OpponentGoals;
            }
            IsBusy = false;
        }

        public async Task<bool> DeleteGoalGrassAsync(Goal goal)
        {
            IsBusy = true;
            var matchesServices = new MatchesServices();
            var success = await matchesServices.DeleteGoalGrassAsync(goal.Id);
            IsBusy = false;
            return success;
        }

        public async Task<bool> DeleteGoalOpponentAsync(Goal goal)
        {
            IsBusy = true;
            var matchesServices = new MatchesServices();
            var success = await matchesServices.DeleteGoalOpponentAsync(goal.Id);
            IsBusy = false;
            return success;
        }

        //public async Task<bool> DeleteGoalOfSelectedMatchAsync(Goal goal)
        //{
        //    IsBusy = true;
        //    var matchesServices = new MatchesServices();
        //    var success = await matchesServices.DeleteGoalOfMatchAsync(_selectedMatch.Id, goal.Id);
        //    IsBusy = false;

        //    return success;
        //}

        public Match SelectedMatch
        {
            get { return _selectedMatch; }
            set
            {
                if (_selectedMatch != value) // only used when value is changed, not when just set
                {
                    _selectedMatch = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task<bool> DeleteSelectedMatchAsync()
        {
            IsBusy = true;
            var matchesServices = new MatchesServices();
            var success = await matchesServices.DeleteMatchAsync(_selectedMatch.Id);
            IsBusy = false;

            return success;
        }

        public ObservableCollection<Category> AllCategories
        {
            get { return _allCategories; }
            set
            {
                if (_allCategories != value) // only used when value is changed, not when just set
                {
                    _allCategories = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Location> AllLocations
        {
            get { return _allLocations; }
            set
            {
                if (_allLocations != value) // only used when value is changed, not when just set
                {
                    _allLocations = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<League> AllLeagues
        {
            get { return _allLeagues; }
            set
            {
                if (_allLeagues != value) // only used when value is changed, not when just set
                {
                    _allLeagues = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime SelectedMatchStartDate
        {
            get { return SelectedMatch.StartDate; }
            set
            {
                SelectedMatch.StartDate = value + SelectedMatch.StartTime;
                SelectedMatch.EndDate = value + SelectedMatch.EndTime;
            }
        }

        public TimeSpan SelectedMatchStartTime
        {
            get { return SelectedMatch.StartTime; }
            set
            {
                SelectedMatch.StartTime = value;
                SelectedMatch.StartDate = SelectedMatch.StartDate.Date + value;
                SelectedMatch.EndDate = SelectedMatch.EndDate.Date + SelectedMatch.EndTime;

                OnPropertyChanged();
            }
        }

        public TimeSpan SelectedMatchEndTime
        {
            get { return SelectedMatch.EndTime; }
            set
            {
                SelectedMatch.EndTime = value;
                SelectedMatch.EndDate = SelectedMatch.EndDate.Date + value;
                OnPropertyChanged();
            }
        }

        public List<Goal> SelectedMatchGrassGoals
        {
            get
            {
                if (SelectedMatch.GrassGoals != null)
                {
                    ChangeGoalsGrassListViewSize(SelectedMatch.GrassGoals.Count);
                }
                return SelectedMatch.GrassGoals;
            }
            set
            {
                if (value != null)
                {
                    SelectedMatch.GrassGoals = value;
                    ChangeGoalsGrassListViewSize(value.Count);
                    OnPropertyChanged();
                }
               
            }
        }

        public List<Goal> SelectedMatchOpponentGoals
        {
            get
            {
                if (SelectedMatch.OpponentGoals != null)
                {
                    ChangeGoalsOpponentListViewSize(SelectedMatch.OpponentGoals.Count);
                }
                return SelectedMatch.OpponentGoals;
            }
            set
            {
                if (value != null)
                {
                    SelectedMatch.OpponentGoals = value;
                    ChangeGoalsOpponentListViewSize(value.Count);
                    OnPropertyChanged();
                }
            }
        }

        public int SelectedMatchGrassScore
        {
            get { return SelectedMatch.GrassScore; }
        }

        public int SelectedMatchOpponentScore
        {
            get { return SelectedMatch.OpponentScore; }
        }

        public Location SelectedMatchLocation
        {
            get { return SelectedMatch.Location; }
            set
            {
                if (SelectedMatch.Location != value)
                {
                    SelectedMatch.Location = value;
                    OnPropertyChanged();
                }
            }
        }

        public League SelectedMatchLeague
        {
            get { return SelectedMatch.League; }
            set
            {
                if (SelectedMatch.League != value)
                {
                    SelectedMatch.League = value;
                    OnPropertyChanged();
                }
            }
        }

        public Category SelectedMatchCategory
        {
            get { return SelectedMatch.Category; }
            set
            {
                if (SelectedMatch.Category != value)
                {
                    SelectedMatch.Category = value;
                    OnPropertyChanged();
                }
            }
        }

        public string GoalsOpponentString
        {
            get
            {
                return "Góly " + SelectedMatch.OpponentName; 
            }
        }

        public async Task InitializeAllLocationsAsync()
        {
            IsBusy = true;
            AllLocations = await Initializer.InitializeAllLocationsAsync();
            IsBusy = false;
        }

        public async Task InitializeAllLeaguesAsync()
        {
            IsBusy = true;
            AllLeagues = await Initializer.InitializeAllLeaguesAsync();
            IsBusy = false;
        }

        public async Task InitializeAllCategoriesAsync()
        {
            IsBusy = true;
            AllCategories = await Initializer.InitializeAllCategoriesAsync();
            IsBusy = false;
        }

        private int _goalsGrassListViewHeight;
        public int GoalsGrassListViewHeight
        {
            get
            {
                return _goalsGrassListViewHeight;
            }
            set
            {
                if (_goalsGrassListViewHeight != value)
                {
                    _goalsGrassListViewHeight = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _goalsOpponentListViewHeight;
        public int GoalsOpponentListViewHeight
        {
            get
            {
                return _goalsOpponentListViewHeight;
            }
            set
            {
                if (_goalsOpponentListViewHeight != value)
                {
                    _goalsOpponentListViewHeight = value;
                    OnPropertyChanged();
                }
            }
        }

        //Bulharska konstanta - velkost 1 itemu v listview golov, pri pozerani priebehu zapasu
        private const int _goalsListViewItemHeight = 86;
        private void ChangeGoalsGrassListViewSize(int count)
        {
            GoalsGrassListViewHeight = count * _goalsListViewItemHeight;
        }

        private void ChangeGoalsOpponentListViewSize(int count)
        {
            GoalsOpponentListViewHeight = count * _goalsListViewItemHeight;
        }

        public Command AddMatchCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var matchesServices = new MatchesServices();

                    if (_selectedMatch.Location == null)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyberte miesto konania zápasu");
                        return;
                    }
                    if (_selectedMatch.League == null)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyberte ligu");
                        return;
                    }
                    if (_selectedMatch.Category == null)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyberte kategóriu");
                        return;
                    }
                    if (string.IsNullOrEmpty(_selectedMatch.OpponentName))
                    {
                        DependencyService.Get<IMessage>().LongAlert("Zadajte názov súpera");
                        return;
                    }
                    if (string.IsNullOrEmpty(_selectedMatch.OpponentAbbreviation))
                    {
                        DependencyService.Get<IMessage>().LongAlert("Zadajte skratku súpera (napr. skratka mesta)");
                        return;
                    }

                    IsBusy = true;
                    var success = await matchesServices.PushMatchAsync(_selectedMatch);

                    if (success)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Zápas bol úspešne pridaný");
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                    }
                    await Navigation.PopAsync();
                    IsBusy = false;
                });
            }
        }

        public Command EditMatchCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var matchesServices = new MatchesServices();

                    IsBusy = true;
                    var success = await matchesServices.PutMatchAsync(_selectedMatch.Id, _selectedMatch);

                    if (success)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Zápas bol úspešne upravený");
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                    }
                    await Navigation.PopAsync();
                    await Navigation.PopAsync();
                    IsBusy = false;
                });
            }
        }

        public Command SaveGrassGoalCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var matchesServices = new MatchesServices();

                    if (_newGoal.Scorer == null)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyberte strelca gólu");
                        return;
                    }

                    TimeSpan ts = new TimeSpan(0, _newGoal.Time.Hours, _newGoal.Time.Minutes);
                    _newGoal.Time = ts;

                    IsBusy = true;
                    var success = await matchesServices.AddGrassGoalToMatchAsync(_newGoal, _selectedMatch.Id);
                    if (success)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Gól bol úspešne pridaný");
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                    }
                    await Navigation.PopAsync();
                    IsBusy = false;
                });
            }
        }

        public Command SaveOpponentGoalCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var matchesServices = new MatchesServices();

                    if (_newGoal.OpponentScorer == null || _newGoal.OpponentScorer == "")
                    {
                        DependencyService.Get<IMessage>().LongAlert("Zadajte strelca gólu");
                        return;
                    }

                    TimeSpan ts = new TimeSpan(0, _newGoal.Time.Hours, _newGoal.Time.Minutes);
                    _newGoal.Time = ts;

                    IsBusy = true;
                    var success = await matchesServices.AddOpponentGoalToMatchAsync(_newGoal, _selectedMatch.Id);
                    if (success)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Gól bol úspešne pridaný");
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                    }
                    await Navigation.PopAsync();
                    IsBusy = false;
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
