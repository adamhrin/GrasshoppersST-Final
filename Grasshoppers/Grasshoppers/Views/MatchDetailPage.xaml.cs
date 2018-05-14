using Grasshoppers.Initializers;
using Grasshoppers.Interfaces;
using Grasshoppers.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MatchDetailPage : ContentPage
	{
		public MatchDetailPage(MatchesViewModel matchesViewModel)
		{
			InitializeComponent();
            BindingContext = matchesViewModel;
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
    }
}