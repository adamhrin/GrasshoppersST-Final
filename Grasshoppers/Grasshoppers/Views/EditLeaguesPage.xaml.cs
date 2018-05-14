using Grasshoppers.Interfaces;
using Grasshoppers.Models;
using Grasshoppers.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditLeaguesPage : ContentPage
    {
        public EditLeaguesPage()
        {
            InitializeComponent();
        }

        private async void btnDeleteLeague_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            
            var league = btn.BindingContext as League;

            var leaguesViewModel = BindingContext as LeaguesViewModel;
            if (await DisplayAlert(null, "Naozaj chceš vymazať súťaž " + league.Name + "? Zmažú sa tým aj všetky udalosti spojené s touto súťažou", "Vymazať", "Zrušiť"))
            {
                if (await DisplayAlert(null, "Naozaj vykonať deštruktívnu operáciu?", "Vymazať", "Zrušiť"))
                {
                    if (await leaguesViewModel.DeleteComponentAsync(league))
                    {
                        OnAppearing();
                        DependencyService.Get<IMessage>().LongAlert("Súťaž vymazaná");
                    }
                    else
                    {
                        OnAppearing();
                        DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                    }
                }
            }
        }

        private async void btnEditLeague_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            
            var league = btn.BindingContext as League;
            var leaguesViewModel = this.BindingContext as LeaguesViewModel;
            leaguesViewModel.SelectedComponent = league;
            await Navigation.PushAsync(new EditLeaguePage(leaguesViewModel));
        }

        private async void btnAddLeague_Clicked(object sender, EventArgs e)
        {
            var leaguesViewModel = BindingContext as LeaguesViewModel;

            if (leaguesViewModel.NewComponent.Name == null || leaguesViewModel.NewComponent.Name == "")
            {
                DependencyService.Get<IMessage>().LongAlert("Vyplňte názov súťaže");
            }
            else
            {
                if (await leaguesViewModel.AddComponentAsync(leaguesViewModel.NewComponent))
                {
                    OnAppearing();
                    DependencyService.Get<IMessage>().LongAlert("Súťaž pridaná");
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

            //refreshujem list so vsetkymi sutazami
            await (BindingContext as LeaguesViewModel).InitializeAllComponentsAsync();
        }

        private async void LeaguesListView_Refreshing(object sender, EventArgs e)
        {
            await (BindingContext as LeaguesViewModel).InitializeAllComponentsAsync();
            LeaguesListView.IsRefreshing = false;
        }
    }
}