using Grasshoppers.Initializers;
using Grasshoppers.Interfaces;
using Grasshoppers.Models;
using Grasshoppers.ViewModels;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MatchPage : ContentPage
	{
		public MatchPage(MatchesViewModel matchesViewModel)
		{
			InitializeComponent();
            matchesViewModel.Navigation = this.Navigation;
            BindingContext = matchesViewModel;
            SetAddGoalBtnsVisibility();
            SetEditDeleteMatchVisibility();
        }

        private void SetAddGoalBtnsVisibility()
        {
            btnAddGoalGrass.IsVisible = (BindingContext as MatchesViewModel).IsAdmin;
            btnAddGoalOpponent.IsVisible = (BindingContext as MatchesViewModel).IsAdmin;
        }

        private void SetEditDeleteMatchVisibility()
        {
            EditDeleteMatchGrid.IsVisible = (BindingContext as MatchesViewModel).IsAdmin;
        }

        private async void btnEditMatch_Clicked(object sender, EventArgs e)
        {
            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                await Navigation.PushAsync(new EditSelectedMatchPage(BindingContext as MatchesViewModel));
            });
        }

        private async void btnDeleteMatch_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert(null, "Naozaj chceš vymazať tento zápas?", "Vymazať", "Zrušiť"))
            {
                var matchesViewModel = BindingContext as MatchesViewModel;
                if (await matchesViewModel.DeleteSelectedMatchAsync())
                {
                    DependencyService.Get<IMessage>().LongAlert("Zápas vymazaný");
                }
                else
                {
                    DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                }
                await Navigation.PopAsync();
            }
        }

        private async Task btnAddGoalGrass_Clicked(object sender, EventArgs e)
        {
            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                await Navigation.PushAsync(new EditGrassScorePage(BindingContext as MatchesViewModel));
            });
        }

        private async Task btnAddGoalOpponent_Clicked(object sender, EventArgs e)
        {
            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                await Navigation.PushAsync(new EditOpponentScorePage(BindingContext as MatchesViewModel));
            });
        }

        private async void GrassGoalsListView_Refreshing(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            GrassGoalsListView.IsRefreshing = false;
        }

        private async void OpponentGoalsListView_Refreshing(object sender, EventArgs e)
        {
            await Task.Delay(1000);
            OpponentGoalsListView.IsRefreshing = false;
        }

        private async Task MatchDetail_Tapped(object sender, EventArgs e)
        {
            var matchesViewModel = BindingContext as MatchesViewModel;
            await Navigation.PushAsync(new MatchDetailPage(matchesViewModel));
        }

        private async void DeleteGoalGrass_Tapped(object sender, EventArgs e)
        {
            await this.DeleteGoal(sender, e, "grass");
        }

        private async void DeleteGoalOpponent_Tapped(object sender, EventArgs e)
        {
            await this.DeleteGoal(sender, e, "opponent");
        }

        private async Task DeleteGoal(object sender, EventArgs e, string type)
        {
            var img = sender as Image;

            var goal = img.BindingContext as Goal;

            var matchesViewModel = BindingContext as MatchesViewModel;
            if (await DisplayAlert(null, "Naozaj chceš vymazať tento gól?", "Vymazať", "Zrušiť"))
            {
                bool success = false;
            
                if (type.Contains("grass"))
                {
                    success = await matchesViewModel.DeleteGoalGrassAsync(goal);
                }
                else if (type.Contains("opponent"))
                {
                    success = await matchesViewModel.DeleteGoalOpponentAsync(goal);
                }

                if (success)
                {
                    OnAppearing();
                    DependencyService.Get<IMessage>().LongAlert("Gól vymazaný");
                }
                else
                {
                    OnAppearing();
                    DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                }
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var matchesViewModel = BindingContext as MatchesViewModel;
            await matchesViewModel.RefreshSelectedMatch();
        }
    }
}