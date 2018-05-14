using Grasshoppers.Extensions;
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
    public class InfoViewModel : INotifyPropertyChanged
    {
        private Info _selectedInfo = new Info();
        private ObservableCollection<Category> _allCategories;

        public InfoViewModel()
        {
            _isBusy = false;
        }

        public bool IsAdmin => Settings.AdminLevel == 1;

        public Info SelectedInfo
        {
            get { return _selectedInfo; }
            set
            {
                if (_selectedInfo != value) // only used when value is changed, not when just set
                {
                    _selectedInfo = value;
                    OnPropertyChanged();
                }
            }
        }

        public void InitializeCategoriesOfSelectedInfo()
        {
            foreach (var category in _allCategories)
            {
                foreach (var infoCategory in SelectedInfo.Categories)
                {
                    if (category.Id == infoCategory.Id)
                    {
                        category.IsChosenForTraining = true;
                    }
                }
            }
        }

        private ObservableCollection<Info> _info;
        public ObservableCollection<Info> Info
        {
            get { return _info; }
            set
            {
                if (value != _info)
                {
                    _info = value;
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

        public async Task InitializeAllCategoriesAsync()
        {
            IsBusy = true;
            AllCategories = await Initializer.InitializeAllCategoriesAsync();
            IsBusy = false;
        }

        public async Task<bool> DeleteSelectedInfoAsync()
        {
            IsBusy = true;
            var infoServices = new InfoServices();
            var success = await infoServices.DeleteInfoAsync(_selectedInfo.Id);
            IsBusy = false;

            return success;
        }

        public async Task InitializeInfo()
        {
            //await Connectivity.DoIfConnectedAndReachable(async () =>
            //{
            var infoServices = new InfoServices();
            var infoHelp = new ObservableCollection<Info>();
            //Info = new ObservableCollection<Info>();
            IsBusy = true;
            //Info.AddRange(await infoServices.GetInfoForMonthAsync(DateTime.Now));
            infoHelp.AddRange(await infoServices.GetInfoForMonthAsync(DateTime.Now));

            if (infoHelp.Count > 0) // aktualizujem info iba ak som nejake vytiahol z db
            {
                this.Info = infoHelp;
            }
            IsBusy = false;
            //});
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

        public Command AddInfoCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var infoServices = new InfoServices();

                    _selectedInfo.Categories = new List<Category>();
                    foreach (var category in AllCategories)
                    {
                        if (category.IsChosenForTraining)
                        {
                            _selectedInfo.Categories.Add(category);
                        }
                    }

                    if (_selectedInfo.Categories.Count == 0)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyberte aspoň jednu kategóriu");
                        return;
                    }

                    IsBusy = true;
                    var success = await infoServices.PushInfoAsync(_selectedInfo);

                    if (success)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Oznam bol úspešne pridaný");
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

        public Command EditInfoCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var infoServices = new InfoServices();

                    _selectedInfo.Categories = new List<Category>();
                    foreach (var category in AllCategories)
                    {
                        if (category.IsChosenForTraining)
                        {
                            _selectedInfo.Categories.Add(category);
                        }
                    }

                    IsBusy = true;
                    var success = await infoServices.PutInfoAsync(_selectedInfo.Id, _selectedInfo);

                    if (success)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Oznam bol úspešne upravený");
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
