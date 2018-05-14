using Grasshoppers.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddTrainingPage : ContentPage
	{
		public AddTrainingPage(TrainingsViewModel trainingsViewModel = null)
		{
			InitializeComponent();
            if (trainingsViewModel == null)
            {
                trainingsViewModel = new TrainingsViewModel();
            }
            trainingsViewModel.Navigation = this.Navigation;
            BindingContext = trainingsViewModel;
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
            //refreshujem list so vsetkymi miestami treningov
            await (BindingContext as TrainingsViewModel).InitializeAllLocationsAsync();
            //refreshujem list so vsetkymi kategoriami
            await (BindingContext as TrainingsViewModel).InitializeAllCategoriesAsync();
        }
    }
}