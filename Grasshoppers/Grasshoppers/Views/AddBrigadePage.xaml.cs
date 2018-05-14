using Grasshoppers.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddBrigadePage : ContentPage
    {
        public AddBrigadePage(BrigadesViewModel brigadesViewModel = null)
        {
            InitializeComponent();
            if (brigadesViewModel == null)
            {
                brigadesViewModel = new BrigadesViewModel();
            }
            brigadesViewModel.Navigation = this.Navigation;
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
            //refreshujem list so vsetkymi miestami treningov
            await (BindingContext as BrigadesViewModel).InitializeAllLocationsAsync();
            //refreshujem list so vsetkymi poziciami
            await (BindingContext as BrigadesViewModel).InitializeAllPositionsAsync();
            //refreshujem list so vsetkymi sutazami
            await (BindingContext as BrigadesViewModel).InitializeAllLeaguesAsync();

        }
    }
}