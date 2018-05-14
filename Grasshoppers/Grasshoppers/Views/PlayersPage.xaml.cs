using Grasshoppers.Views;
using System;
using Xamarin.Forms;
using Grasshoppers.Models;
using Grasshoppers.ViewModels;

namespace Grasshoppers
{
    public partial class PlayersPage : ContentPage
    {
        public PlayersPage()
        {
            InitializeComponent();
            (BindingContext as PlayersViewModel).Navigation = this.Navigation;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPlayerPage());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as PlayersViewModel).InitializePlayersAsync();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tappedPlayer = PlayersListView.SelectedItem as Player;

            if (tappedPlayer != null)
            {
                var playersViewModel = BindingContext as PlayersViewModel;

                if (playersViewModel != null)
                {
                    playersViewModel.SelectedPlayer = tappedPlayer;
                }
            }
        }

        private async void Search_Button_Clicked(object sender, EventArgs e)
        {

            var playersViewModel = BindingContext as PlayersViewModel;

            if (playersViewModel != null)
            {
                await Navigation.PushAsync(new SearchPlayersPage(playersViewModel));
            }
        }
    }
}
