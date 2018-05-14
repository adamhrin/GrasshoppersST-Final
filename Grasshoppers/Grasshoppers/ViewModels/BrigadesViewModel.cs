using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Grasshoppers.Helpers;
using Grasshoppers.Initializers;
using Grasshoppers.Interfaces;
using Grasshoppers.Models;
using Grasshoppers.Services;
using Xamarin.Forms;

namespace Grasshoppers.ViewModels
{
    public class BrigadesViewModel : INotifyPropertyChanged
    {
        private Brigade _selectedBrigade = new Brigade();
        private List<Position> _positions;
        private ObservableCollection<Location> _allLocations;
        private ObservableCollection<Position> _allPositions;
        private ObservableCollection<League> _allLeagues;

        public BrigadesViewModel()
        {
            _isBusy = false;
        }

        public bool IsAdmin => Settings.AdminLevel == 1;

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

        public ObservableCollection<Position> AllPositions
        {
            get { return _allPositions; }
            set
            {
                if (_allPositions != value) // only used when value is changed, not when just set
                {
                    _allPositions = value;
                    ChangeNewBrigadePositionsViewSize();
                    OnPropertyChanged();
                }
            }
        }

        public async Task<bool> DeleteSelectedBrigadeAsync()
        {
            IsBusy = true;
            var brigadesServices = new BrigadesServices();
            var success = await brigadesServices.DeleteBrigadeAsync(_selectedBrigade.Id);
            IsBusy = false;

            return success;
        }

        internal void InitializePositionsForSelectedBrigade()
        {
            foreach (var position in _allPositions)
            {
                foreach (var brigadePosition in SelectedBrigade.Positions)
                {
                    if (position.Id == brigadePosition.Id)
                    {
                        position.IsChosenForBrigade = true;
                    }
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

        public Brigade SelectedBrigade
        {
            get { return _selectedBrigade; }
            set
            {
                if (_selectedBrigade != value) // only used when value is changed, not when just set
                {
                    _selectedBrigade = value;
                    OnPropertyChanged();
                }
            }
        }

        public INavigation Navigation { get; set; }
        
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

        public DateTime SelectedBrigadeStartDate
        {
            get { return SelectedBrigade.StartDate; }
            set
            {
                SelectedBrigade.StartDate = value + SelectedBrigade.StartTime;
                SelectedBrigade.EndDate = value + SelectedBrigade.EndTime;
            }
        }

        public TimeSpan SelectedBrigadeStartTime
        {
            get { return SelectedBrigade.StartTime; }
            set
            {
                SelectedBrigade.StartTime = value;
                SelectedBrigade.StartDate = SelectedBrigade.StartDate.Date + value;
                SelectedBrigade.EndDate = SelectedBrigade.EndDate.Date + SelectedBrigade.EndTime;
                
                OnPropertyChanged();
            }
        }

        public TimeSpan SelectedBrigadeEndTime
        {
            get { return SelectedBrigade.EndTime; }
            set
            {
                SelectedBrigade.EndTime = value;
                SelectedBrigade.EndDate = SelectedBrigade.EndDate.Date + value;
                OnPropertyChanged();
            }
        }
        
        //public async Task InitializePositionsAsync()
        //{
        //    IsBusy = true;
        //    var positionsServices = new PositionsServices();
        //    Positions = await positionsServices.GetPositionsForBrigadeAsync(_selectedBrigade);
        //    IsBusy = false;
        //}

        public async Task InitializeAllPositionsAsync()
        {
            IsBusy = true;
            AllPositions = await Initializer.InitializeAllPositionsAsync();
            IsBusy = false;
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

        public Position SelectedPosition { get; set; }

        public Command RegisterPlayerOnPositionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;
                    var positionsServices = new PositionsServices();
                    bool success = await positionsServices.RegisterPlayerOnPositionAsync(SelectedBrigade.Id, SelectedPosition);

                    if (success)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Si úspešne prihlásený");
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                    }

                    await Navigation.PopAsync();
                    IsBusy = false;
                    //return success;
                });
            }
        }

        //public async Task<bool> RegisterPlayerOnPositionAsync(Position position)
        //{
        //    SelectedPosition = position;
        //    //IsBusy = true;
        //    //var positionsServices = new PositionsServices();
        //    //bool success = await positionsServices.RegisterPlayerOnPositionAsync(SelectedBrigade.Id, position);
        //    //await Navigation.PopAsync();
        //    //IsBusy = false;
        //    //return success;
        //}

        public async Task<bool> UnregisterPlayerFromPositionAsync(Position position)
        {
            IsBusy = true;
            var positionsServices = new PositionsServices();
            bool success = await positionsServices.UnregisterPlayerFromPositionAsync(SelectedBrigade.Id, position);
            await Navigation.PopAsync();
            IsBusy = false;
            return success;
        }

        public Location SelectedBrigadeLocation
        {
            get { return SelectedBrigade.Location; }
            set
            {
                if (SelectedBrigade.Location != value)
                {
                    SelectedBrigade.Location = value;
                    OnPropertyChanged();
                }
            }
        }

        public League SelectedBrigadeLeague
        {
            get { return SelectedBrigade.League; }
            set
            {
                if (SelectedBrigade.League != value)
                {
                    SelectedBrigade.League = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<Position> Positions
        {
            get { return _positions; }
            set
            {
                if (_positions != value) // only used when value is changed, not when just set
                {
                    _positions = value;
                    ChangePositionsListViewSize(value.Count);
                    OnPropertyChanged();
                }
            }
        }
        
        public List<Position> SelectedBrigadePositions
        {
            get
            {
                if (SelectedBrigade.Positions != null)
                {
                    ChangePositionsListViewSize(SelectedBrigade.Positions.Count);
                }
                return SelectedBrigade.Positions;
            }
            set
            {
                if (SelectedBrigade.Positions != value) // only used when value is changed, not when just set
                {
                    SelectedBrigade.Positions = value;
                    ChangePositionsListViewSize(value.Count);
                    OnPropertyChanged();
                }
            }
        }

        private int _positionsListViewHeight;
        public int PositionsListViewHeight
        {
            get
            {
                return _positionsListViewHeight;
            }
            set
            {
                if (_positionsListViewHeight != value)
                {
                    _positionsListViewHeight = value;
                    OnPropertyChanged();
                }
            }
        }

        //Bulharska konstanta - velkost 1 itemu v listview pozicii, pri prezerani brigady v BrigadePage
        private const int _positionsListViewItemHeight = 112;
        private void ChangePositionsListViewSize(int count)
        {
            PositionsListViewHeight = count * _positionsListViewItemHeight;
        }

        private int _allPositionsListViewHeight;
        public int AllPositionsListViewHeight
        {
            get
            {
                return _allPositionsListViewHeight;
            }
            set
            {
                if (_allPositionsListViewHeight != value)
                {
                    _allPositionsListViewHeight = value;
                    OnPropertyChanged();
                }
            }
        }

        //Bulharska konstanta - velkost 1 itemu v listview pozicii pri pridavani novej brigady v AddBrigadePage
        private const int _newBrigadePositionsListViewItemHeight = 68;
        private void ChangeNewBrigadePositionsViewSize()
        {
            AllPositionsListViewHeight = AllPositions.Count * _newBrigadePositionsListViewItemHeight;
        }

        public Command EditBrigadeCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var brigadesServices = new BrigadesServices();

                    _selectedBrigade.Positions = new List<Position>();
                    foreach (var position in AllPositions)
                    {
                        if (position.IsChosenForBrigade)
                        {
                            _selectedBrigade.Positions.Add(position);
                        }
                    }

                    IsBusy = true;
                    var success = await brigadesServices.PutBrigadeAsync(_selectedBrigade.Id, _selectedBrigade);

                    if (success)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Brigáda bola úspešne upravená");
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

        public Command AddBrigadeCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var brigadesServices = new BrigadesServices();

                    if (_selectedBrigade.Location == null)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyberte miesto konania brigády");
                        return;
                    }
                    if (_selectedBrigade.League == null)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyberte ligu");
                        return;
                    }

                    _selectedBrigade.Positions = new List<Position>();
                    foreach (var position in AllPositions)
                    {
                        if (position.IsChosenForBrigade)
                        {
                            _selectedBrigade.Positions.Add(position);
                        }
                    }

                    if (_selectedBrigade.Positions.Count == 0)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyberte aspoň jednu pozíciu");
                        return;
                    }

                    IsBusy = true;
                    var success = await brigadesServices.PushBrigadeAsync(_selectedBrigade);

                    if (success)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Brigáda bola úspešne pridaná");
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
