using Grasshoppers.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMatchPage : ContentPage
    {
        public AddMatchPage(MatchesViewModel matchesViewModel = null)
        {
            InitializeComponent();
            if (matchesViewModel == null)
            {
                matchesViewModel = new MatchesViewModel();
            }
            matchesViewModel.Navigation = this.Navigation;
            BindingContext = matchesViewModel;
        }

        private async void btnEditLeagues_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditLeaguesPage());
        }

        private async void btnEditLocations_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditLocationsPage());
        }

        private async void btnEditCategories_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditCategoriesPage());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //refreshujem list so vsetkymi miestami treningov
            await (BindingContext as MatchesViewModel).InitializeAllLocationsAsync();
            //refreshujem list so vsetkymi sutazami
            await (BindingContext as MatchesViewModel).InitializeAllLeaguesAsync();
            //refreshujem list so vsetkymi kategoriami
            await (BindingContext as MatchesViewModel).InitializeAllCategoriesAsync();

        }
    }
}