using Grasshoppers.Enums;
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
    public class TrainingsViewModel : INotifyPropertyChanged
    {
        private Training _selectedTraining = new Training();
        private ObservableCollection<Location> _allLocations;
        private ObservableCollection<Category> _allCategories;

        public TrainingsViewModel()
        {
            _isBusy = false;
        }

        public bool IsAdmin => Settings.AdminLevel == 1;

        public INavigation Navigation { private get; set; }

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

        //public void InitializeLocationForSelectedTraining()
        //{
        //    SelectedTrainingLocation = _selectedTraining.Location;
        //}

        public async Task InitializeAllLocationsAsync()
        {
            IsBusy = true;
            AllLocations = await Initializer.InitializeAllLocationsAsync();
            IsBusy = false;
        }

        public void InitializeCategoriesForSelectedTraining()
        {
            foreach (var category in _allCategories)
            {
                foreach (var trainingCategory in SelectedTraining.Categories)
                {
                    if (category.Id == trainingCategory.Id)
                    {
                        category.IsChosenForTraining = true;
                    }
                }
            }
        }

        public async Task InitializeAllCategoriesAsync()
        {
            IsBusy = true;
            AllCategories = await Initializer.InitializeAllCategoriesAsync();
            IsBusy = false;
        }

        public async Task<bool> DeleteSelectedTrainingAsync()
        {
            
            IsBusy = true;
            var trainingServices = new TrainingsServices();
            var success = await trainingServices.DeleteTrainingAsync(_selectedTraining.Id);
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
                    ChangeCategoriesListViewSize();
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

        public Training SelectedTraining
        {
            get { return _selectedTraining; }
            set
            {
                if (_selectedTraining != value) // only used when value is changed, not when just set
                {
                    _selectedTraining = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime SelectedTrainingStartDate
        {
            get { return SelectedTraining.StartDate; }
            set
            {
                SelectedTraining.StartDate = value + SelectedTraining.StartTime;
                SelectedTraining.EndDate = value + SelectedTraining.EndTime;
            }
        }

        public TimeSpan SelectedTrainingStartTime
        {
            get { return SelectedTraining.StartTime; }
            set
            {
                SelectedTraining.StartTime = value;
                SelectedTraining.StartDate = SelectedTraining.StartDate.Date + value;
                SelectedTraining.EndDate = SelectedTraining.EndDate.Date + SelectedTraining.EndTime;
                
                OnPropertyChanged();
            }
        }

        public TimeSpan SelectedTrainingEndTime
        {
            get { return SelectedTraining.EndTime; }
            set
            {
                SelectedTraining.EndTime = value;
                SelectedTraining.EndDate = SelectedTraining.EndDate.Date + value;
                OnPropertyChanged();
            }
        }

        public Location SelectedTrainingLocation
        {
            get { return SelectedTraining.Location; }
            set
            {
                //if (SelectedTraining.Location != value)
                //{
                    SelectedTraining.Location = value;
                    OnPropertyChanged();
                //}
            }
        }

        private int _allCategoriesListViewHeight;
        public int AllCategoriesListViewHeight
        {
            get
            {
                return _allCategoriesListViewHeight;
            }
            set
            {
                if (_allCategoriesListViewHeight != value)
                {
                    _allCategoriesListViewHeight = value;
                    OnPropertyChanged();
                }
            }
        }

        //Bulharska konstanta - velkost 1 itemu v listview kategorii, je to padding + margin + velkost switchu (odhadom 22) + margin + padding
        private const int _categoriesListViewItemHeight = 68;

        private void ChangeCategoriesListViewSize()
        {
            AllCategoriesListViewHeight = AllCategories.Count * _categoriesListViewItemHeight;
        }

        public Command AddTrainingCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var trainingsServices = new TrainingsServices();

                    if (_selectedTraining.Location == null)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyberte miesto konania tréningu");
                        return;
                    }

                    _selectedTraining.Categories = new List<Category>();
                    foreach (var category in AllCategories)
                    {
                        if (category.IsChosenForTraining)
                        {
                            _selectedTraining.Categories.Add(category);
                        }
                    }

                    if (_selectedTraining.Categories.Count == 0)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyberte aspoň jednu kategóriu");
                        return;
                    }

                    IsBusy = true;
                    var success = await trainingsServices.PushTrainingAsync(_selectedTraining);

                    if (success)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Tréning bol úspešne pridaný");
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

        public Command EditTrainingCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var trainingsServices = new TrainingsServices();

                    _selectedTraining.Categories = new List<Category>();
                    foreach (var category in AllCategories)
                    {
                        if (category.IsChosenForTraining)
                        {
                            _selectedTraining.Categories.Add(category);
                        }
                    }

                    IsBusy = true;
                    var success = await trainingsServices.PutTrainingAsync(_selectedTraining.Id, _selectedTraining);

                    if (success)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Tréning bol úspešne upravený");
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

        public Command PlayerAcceptsTrainingCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var trainingsServices = new TrainingsServices();

                    IsBusy = true;
                    _selectedTraining.AcceptedByPlayer = AcceptsTrainingOptions.Accepted;
                    var success = await trainingsServices.PutPlayerOnTrainingAsync(_selectedTraining);

                    if (success)
                    {
                        DependencyService.Get<IMessage>().LongAlert("A nie že neprídeš!");                    }
                    else
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                    }
                    await Navigation.PopAsync();
                    IsBusy = false;
                });
            }
        }

        public Command PlayerDeclinesTrainingCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var trainingsServices = new TrainingsServices();

                    IsBusy = true;
                    _selectedTraining.AcceptedByPlayer = AcceptsTrainingOptions.Declined;
                    var success = await trainingsServices.PutPlayerOnTrainingAsync(_selectedTraining);

                    if (success)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Škoda ťa, mohol si byť dobrý hráč, ale keď netrénuješ...");
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
