using Grasshoppers.Initializers;
using Grasshoppers.Interfaces;
using Grasshoppers.Models;
using Grasshoppers.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TrainingPage : ContentPage
	{
		public TrainingPage(TrainingsViewModel trainingsViewModel)
		{
			InitializeComponent();
            trainingsViewModel.Navigation = this.Navigation;
            BindingContext = trainingsViewModel;
            SetEditDeleteTrainingVisibility();
		}

        private void SetEditDeleteTrainingVisibility()
        {
            //moznost mazat a editovat trening pre admina
            EditDeleteTrainingGrid.IsVisible = (BindingContext as TrainingsViewModel).IsAdmin;
        }

        private async Task InvitedPlayers_Tapped(object sender, EventArgs e)
        {
            var trainingsViewModel = BindingContext as TrainingsViewModel;

            if (trainingsViewModel != null)
            {
                await Navigation.PushAsync(new InvitedPlayersPage(trainingsViewModel));
            }
        }

        private async Task Categories_Tapped(object sender, EventArgs e)
        {
            var categoriesObservable = new ObservableCollection<Category>();

            var trainingsViewModel = BindingContext as TrainingsViewModel;

            var categories = trainingsViewModel.SelectedTraining.Categories;

            foreach (var item in categories)
            {
                categoriesObservable.Add(item);
            }

            await DisplayActionSheet("Kategórie", "Zrušiť", null, categoriesObservable.Select(category => category.Name).ToArray());
            
        }

        private async void btnEditTraining_Clicked(object sender, EventArgs e)
        {
            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                await Navigation.PushAsync(new EditSelectedTrainingPage(BindingContext as TrainingsViewModel));
            });
            
        }

        private async void btnDeleteTraining_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert(null, "Naozaj chceš vymazať tento tréning?", "Vymazať", "Zrušiť"))
            {
                var trainingsViewModel = BindingContext as TrainingsViewModel;
                if (await trainingsViewModel.DeleteSelectedTrainingAsync())
                {
                    DependencyService.Get<IMessage>().LongAlert("Tréning vymazaný");
                }
                else
                {
                    DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                }
                await Navigation.PopAsync();
            }
        }
    }
}