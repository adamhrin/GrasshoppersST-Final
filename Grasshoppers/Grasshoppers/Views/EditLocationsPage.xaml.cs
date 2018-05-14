using Grasshoppers.Interfaces;
using Grasshoppers.Models;
using Grasshoppers.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditLocationsPage : ContentPage
	{
		public EditLocationsPage()
		{
			InitializeComponent();
		}

        private async void btnDeleteLocation_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            
            var location = btn.BindingContext as Location;

            var locationsViewModel = BindingContext as LocationsViewModel;
            if (await DisplayAlert(null, "Naozaj chceš vymazať lokáciu " + location.Name + "? Zmažú sa tým aj všetky udalosti spojené s touto lokáciou", "Vymazať", "Zrušiť"))
            {
                if (await DisplayAlert(null, "Naozaj vykonať deštruktívnu operáciu?", "Vymazať", "Zrušiť"))
                {
                    if (await locationsViewModel.DeleteComponentAsync(location))
                    {
                        OnAppearing();
                        DependencyService.Get<IMessage>().LongAlert("Lokácia vymazaná");
                    }
                    else
                    {
                        OnAppearing();
                        DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                    }
                }
            }
        }

        private async void btnEditLocation_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            
            var location = btn.BindingContext as Location;
            var locationsViewModel = this.BindingContext as LocationsViewModel;
            locationsViewModel.SelectedComponent = location;
            await Navigation.PushAsync(new EditLocationPage(locationsViewModel));
        }

        private async void btnAddLocation_Clicked(object sender, EventArgs e)
        {
            var locationsViewModel = BindingContext as LocationsViewModel;

            if (locationsViewModel.NewComponent.Name == null || locationsViewModel.NewComponent.Name == "")
            {
                DependencyService.Get<IMessage>().LongAlert("Vyplňte názov lokácie");
            }
            else
            {
                if (await locationsViewModel.AddComponentAsync(locationsViewModel.NewComponent))
                {
                    OnAppearing();
                    DependencyService.Get<IMessage>().LongAlert("Lokácia pridaná");
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

            //refreshujem list so vsetkymi lokaciami
            await (BindingContext as LocationsViewModel).InitializeAllComponentsAsync();
        }

        private async void LocationsListView_Refreshing(object sender, EventArgs e)
        {
            await (BindingContext as LocationsViewModel).InitializeAllComponentsAsync();
            LocationsListView.IsRefreshing = false;
        }
    }
}