using Grasshoppers.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditSelectedMatchPage : ContentPage
	{
		public EditSelectedMatchPage(MatchesViewModel matchesViewModel)
		{
			InitializeComponent ();
            BindingContext = matchesViewModel;
		}

        private async void btnEditLeagues_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditLeaguesPage());
        }

        private async void btnEditCategories_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditCategoriesPage());
        }

        private async void btnEditLocations_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditLocationsPage());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var matchesViewModel = BindingContext as MatchesViewModel;

            //refreshujem list so vsetkymi miestami
            await matchesViewModel.InitializeAllLocationsAsync();
            pckrLocation.SelectedItem = matchesViewModel.SelectedMatch.Location;

            //refreshujem list so vsetkymi sutazami
            await matchesViewModel.InitializeAllLeaguesAsync();
            pckrLeague.SelectedItem = matchesViewModel.SelectedMatch.League;

            //refreshujem list so vsetkymi kategoriami
            await matchesViewModel.InitializeAllCategoriesAsync();
            pckrCategory.SelectedItem = matchesViewModel.SelectedMatch.Category;
        }
	}
}