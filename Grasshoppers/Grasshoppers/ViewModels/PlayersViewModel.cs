using Grasshoppers.Helpers;
using Grasshoppers.Initializers;
using Grasshoppers.Interfaces;
using Grasshoppers.Models;
using Grasshoppers.Services;
using Grasshoppers.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Grasshoppers.ViewModels
{
    public class PlayersViewModel : INotifyPropertyChanged
    {
        private List<Player> _playersList;
        private Player _selectedPlayer = new Player();
        private ObservableCollection<Category> _allCategories;

        public PlayersViewModel()
        {
            _isBusy = false;
        }

        public bool IsAdmin => Settings.AdminLevel == 1;

        public INavigation Navigation { private get; set; }

        public async Task InitializePlayerAccount()
        {
            var playersServices = new PlayersServices();
            IsBusy = true;
            SelectedPlayer = await playersServices.GetPlayerAsync();
            IsBusy = false;
        }

        public void InitializeCategoriesForSelectedPlayer()
        {
            foreach (var category in _allCategories)
            {
                foreach (var playerCategory in SelectedPlayer.Categories)
                {
                    if (category.Id == playerCategory.Id)
                    {
                        category.IsChosenForPlayer = true;
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

        private Player _playerToRegister = new Player();
        public Player PlayerToRegister { get => _playerToRegister; set => _playerToRegister = value; }

        private Player _playerToLogin = new Player();
        public Player PlayerToLogin { get => _playerToLogin; set => _playerToLogin = value; }

        public List<Player> PlayersList
        {
            get { return _playersList; }
            set
            {
                if (_playersList != value) // only used when value is changed, not when just set
                {
                    _playersList = value;
                    OnPropertyChanged();
                }
            }
        }

        public Player SelectedPlayer
        {
            get { return _selectedPlayer; }
            set
            {
                if (_selectedPlayer != value) // only used when value is changed, not when just set
                {
                    _selectedPlayer = value;
                    OnPropertyChanged();
                }
            }
        }

        /**
         * Besides Command, also Button_Clicked will be performed (in code-behind of AddPlayerPage.xaml)
         */
        public Command PostCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var playersServices = new PlayersServices();

                    var success = await playersServices.PostPlayerAsync(_selectedPlayer);  
                    
                    if (success)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Hráč bol úspešne pridaný");
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                    }
                });
            }
        }

        public Command PutPlayerCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var playersServices = new PlayersServices();

                    _selectedPlayer.Categories = new List<Category>();
                    foreach (var category in AllCategories)
                    {
                        if (category.IsChosenForPlayer)
                        {
                            _selectedPlayer.Categories.Add(category);
                        }
                    }

                    var success = await playersServices.PutPlayerAsync(_selectedPlayer);

                    if (success)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Zmeny uložené");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                    }
                });
            }
        }

        public Command DeleteCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var playersServices = new PlayersServices();

                    var success = await playersServices.DeletePlayerAsync(_selectedPlayer.Id);

                    if (success)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Hráč vymazaný");
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                    }
                });
            }
        }

        public Command RegisterCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (string.IsNullOrEmpty(PlayerToRegister.Email) || string.IsNullOrEmpty(PlayerToRegister.Password))
                    {
                        DependencyService.Get<IMessage>().LongAlert("Zadajte aspoň email a heslo");
                        return;
                    }

                    if (PlayerToRegister.Password != PlayerToRegister.ConfirmPassword)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Heslo a potvrdenie hesla sa nezhodujú");
                        return;
                    }


                    var playersServices = new PlayersServices();
                    IsBusy = true;
                    
                    int result = await playersServices.RegisterPlayerAsync(_playerToRegister);
                    if (result == -1)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Hráč s takými prihlasovacími údajmi už existuje");
                        IsBusy = false;
                        return;
                    }
                    if (result == 1)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Ste úspešne registrovaný");

                        //registracne data usera
                        Settings.Email = PlayerToRegister.Email;
                        Settings.Password = PlayerToRegister.Password;
                        await Navigation.PushAsync(new LoginPlayerPage(this));
                        IsBusy = false;
                    }
                    else if (result == 0)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba.Skúste to znova");
                        IsBusy = false;
                    }
                    IsBusy = false;
                });
            }
        }

        public Command LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (string.IsNullOrEmpty(PlayerToLogin.Email))
                    {
                        DependencyService.Get<IMessage>().LongAlert("Zadajte email");
                        return;
                    }
                    if (string.IsNullOrEmpty(PlayerToLogin.Password))
                    { 
                        DependencyService.Get<IMessage>().LongAlert("Zadajte heslo");
                        return;
                    }
                    
                    var playersServices = new PlayersServices();
                    IsBusy = true;

                    //  id logedInPlayera moze byt:
                    //  jeho realne id - OK 
                    //  0 - ina chyba na serveri
                    // -1 - nenajdeny player, nespravne prihlasovacie udaje
                    Player logedInPlayer = await playersServices.LoginPlayerAsync(_playerToLogin);
                    if (logedInPlayer.Id == 0)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba.Skúste to znova");
                    }
                    else if (logedInPlayer.Id == -1)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Nesprávne prihlasovacie údaje");
                    }
                    else if (logedInPlayer.Id != -2) // OK, -2 znamena chyba v pripojeni
                    {
                        //nastavim hodnoty podla hodnot prihlasovaneho cloveka az ked sa podari prihlasenie
                        Settings.Email = _playerToLogin.Email;
                        Settings.Password = _playerToLogin.Password;
                        Settings.IdPlayer = logedInPlayer.Id;
                        Settings.AdminLevel = logedInPlayer.AdminLevel;
                        DependencyService.Get<IMessage>().LongAlert("Ste úspešne prihlásený");
                        Application.Current.MainPage = new NavigationPage(new BasePage());
                    }
                    IsBusy = false;
                });
            }
        }

        public Command SavePlayersCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var playersServices = new PlayersServices();
                    IsBusy = true;
                    var success = await playersServices.PutPlayersAdminAsync(PlayersList);
                    if (success)
                    {
                        DependencyService.Get<IMessage>().LongAlert("Práva úspešne zmenené");
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

        public async Task InitializePlayersAsync()
        {
            IsBusy = true;
            var playersServices = new PlayersServices();
            PlayersList = await playersServices.GetPlayersAsync();
            IsBusy = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
