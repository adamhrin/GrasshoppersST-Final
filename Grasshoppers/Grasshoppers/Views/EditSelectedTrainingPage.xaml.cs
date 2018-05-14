using System;
using Grasshoppers.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditSelectedTrainingPage : ContentPage
    {
        public EditSelectedTrainingPage(TrainingsViewModel trainingsViewModel)
        {
            InitializeComponent();
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
            var trainingsViewModel = BindingContext as TrainingsViewModel;

            //refreshujem list so vsetkymi miestami treningov
            await trainingsViewModel.InitializeAllLocationsAsync();
            //pckrPlace.SelectedItem = trainingsViewModel.SelectedTraining.Location;
            //trainingsViewModel.InitializeLocationForSelectedTraining();

            //refreshujem list so vsetkymi kategoriami
            await trainingsViewModel.InitializeAllCategoriesAsync();
            trainingsViewModel.InitializeCategoriesForSelectedTraining();
        }
    }
}