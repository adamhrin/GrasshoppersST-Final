using Grasshoppers.Helpers;
using Grasshoppers.Initializers;
using Grasshoppers.Models;
using Grasshoppers.Services;
using Grasshoppers.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BasePage : TabbedPage
    {
        public EventsViewModel EventsViewModel { get; set; }
        public BasePage()
        {
            InitializeComponent();
            //Children.Add(new RegisterPlayerPage());
            EventsViewModel = new EventsViewModel();
            Children.Add(new CalendarPage(EventsViewModel));
            Children.Add(new InfoPage());
            CurrentPage = null;
            InitializeToolbar();
        }

        private void InitializeToolbar()
        {
            if (Settings.AdminLevel == 1)
            {
                //toolbar len pre admina
                var tbItemAddEvent = new ToolbarItem { Order = ToolbarItemOrder.Primary };

                if (Device.RuntimePlatform == Device.iOS)
                {
                    tbItemAddEvent.Icon = "ic_add_circle_outline_white.png";
                }
                else if (Device.RuntimePlatform == Device.Android)
                {
                    tbItemAddEvent.Icon = "ic_add_circle_outline_white_24dp.png";
                }
                else if (Device.RuntimePlatform == Device.UWP || Device.RuntimePlatform == Device.WinPhone)
                {
                    tbItemAddEvent.Icon = "Images/ic_add_circle_outline_white.png";
                }
                tbItemAddEvent.Clicked += tbItemAddEvent_Clicked;
                ToolbarItems.Add(tbItemAddEvent);

                var tbItemAdministrateCategories = new ToolbarItem { Order = ToolbarItemOrder.Secondary };
                tbItemAdministrateCategories.Text = "Správa kategórií";
                tbItemAdministrateCategories.Clicked += tbItemAdministrateCategories_Clicked;
                ToolbarItems.Add(tbItemAdministrateCategories);

                var tbItemAdministrateLocations = new ToolbarItem { Order = ToolbarItemOrder.Secondary };
                tbItemAdministrateLocations.Text = "Správa lokácií";
                tbItemAdministrateLocations.Clicked += tbItemAdministrateLocations_Clicked;
                ToolbarItems.Add(tbItemAdministrateLocations);

                var tbItemAdministrateLeagues = new ToolbarItem { Order = ToolbarItemOrder.Secondary };
                tbItemAdministrateLeagues.Text = "Správa súťaží";
                tbItemAdministrateLeagues.Clicked += tbItemAdministrateLeagues_Clicked;
                ToolbarItems.Add(tbItemAdministrateLeagues);

                var tbItemAdministrateBrigadePositions = new ToolbarItem { Order = ToolbarItemOrder.Secondary };
                tbItemAdministrateBrigadePositions.Text = "Správa pozícií brigád";
                tbItemAdministrateBrigadePositions.Clicked += tbItemAdministrateBrigadePositions_Clicked;
                ToolbarItems.Add(tbItemAdministrateBrigadePositions);

                var tbItemAdministratePlayers = new ToolbarItem { Order = ToolbarItemOrder.Secondary };
                tbItemAdministratePlayers.Text = "Správa používateľov";
                tbItemAdministratePlayers.Clicked += tbItemAdministratePlayers_Clicked;
                ToolbarItems.Add(tbItemAdministratePlayers);
            }

            // aj pre obycajneho usera
            var tbItemAdministrateAccount = new ToolbarItem { Order = ToolbarItemOrder.Secondary };
            tbItemAdministrateAccount.Text = "Správa účtu";
            tbItemAdministrateAccount.Clicked += tbItemAdministrateAccount_Clicked;
            ToolbarItems.Add(tbItemAdministrateAccount);

            var tbItemLogout = new ToolbarItem { Order = ToolbarItemOrder.Secondary };
            tbItemLogout.Text = "Odhlásiť";
            tbItemLogout.Clicked += tbItemLogout_Clicked;
            ToolbarItems.Add(tbItemLogout);
        }

        private async void tbItemAdministratePlayers_Clicked(object sender, EventArgs e)
        {
            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                await Navigation.PushAsync(new PlayersPage());
            });
        }

        private async void tbItemLogout_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert(null, "Naozaj odhlásiť?", "Odhlásiť", "Zrušiť"))
            {
                Settings.IdPlayer = 0;
                Application.Current.MainPage = new NavigationPage(new LoginPlayerPage(new ViewModels.PlayersViewModel()));
            }  
        }

        private async void tbItemAdministrateLeagues_Clicked(object sender, EventArgs e)
        {
            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                await Navigation.PushAsync(new EditLeaguesPage());
            });
        }

        private async void tbItemAdministrateBrigadePositions_Clicked(object sender, EventArgs e)
        {
            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                await Navigation.PushAsync(new EditPositionsPage());
            });
        }

        private async void tbItemAdministrateCategories_Clicked(object sender, EventArgs e)
        {
            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                await Navigation.PushAsync(new EditCategoriesPage());
            });
        }

        private async void tbItemAdministrateLocations_Clicked(object sender, EventArgs e)
        {
            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                await Navigation.PushAsync(new EditLocationsPage());
            });
        }

        private async void tbItemAdministrateAccount_Clicked(object sender, EventArgs e)
        {
            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                await Navigation.PushAsync(new PlayerAccountPage());
            });
        }

        private async void tbItemAddEvent_Clicked(object sender, EventArgs e)
        {
            string chosen = await DisplayActionSheet(null, "Zrušiť", null, "Pridať tréning", "Pridať zápas", "Pridať brigádu", "Pridať oznam");

            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                if (chosen == "Pridať tréning")
                {
                    await Navigation.PushAsync(new AddTrainingPage());
                }
                else if (chosen == "Pridať zápas")
                {
                    await Navigation.PushAsync(new AddMatchPage());
                }
                else if (chosen == "Pridať brigádu")
                {
                    await Navigation.PushAsync(new AddBrigadePage());
                }
                else if (chosen == "Pridať oznam")
                {
                    await Navigation.PushAsync(new AddInfoPage());
                }
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await EventsViewModel.InitializeEvents();
        }
    }
}