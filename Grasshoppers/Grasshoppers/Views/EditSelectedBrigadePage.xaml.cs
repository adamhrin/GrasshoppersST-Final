using Grasshoppers.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditSelectedBrigadePage : ContentPage
    {
        public EditSelectedBrigadePage(BrigadesViewModel brigadesViewModel)
        {
            InitializeComponent();
            BindingContext = brigadesViewModel;
        }

        private async void btnEditLeagues_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditLeaguesPage());
        }

        private async void btnEditPositions_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditPositionsPage());
        }

        private async void btnEditLocations_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditLocationsPage());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var brigadesViewModel = BindingContext as BrigadesViewModel;

            //refreshujem list so vsetkymi miestami brigad
            await brigadesViewModel.InitializeAllLocationsAsync();
            pckrLocation.SelectedItem = brigadesViewModel.SelectedBrigade.Location;

            //refreshujem list so vsetkymi sutazami
            await brigadesViewModel.InitializeAllLeaguesAsync();
            pckrLeague.SelectedItem = brigadesViewModel.SelectedBrigade.League;

            //refreshujem list so vsetkymi poziciami
            await brigadesViewModel.InitializeAllPositionsAsync();
            brigadesViewModel.InitializePositionsForSelectedBrigade();
        }
    }
}