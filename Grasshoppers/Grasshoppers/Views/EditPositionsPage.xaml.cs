using Grasshoppers.Interfaces;
using Grasshoppers.Models;
using Grasshoppers.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPositionsPage : ContentPage
    {
        public EditPositionsPage()
        {
            InitializeComponent();
        }

        private async void btnDeletePosition_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            
            var position = btn.BindingContext as Position;

            var locationsViewModel = BindingContext as PositionsViewModel;
            if (await DisplayAlert(null, "Naozaj chceš vymazať pozíciu " + position.Name + "? Zmažú sa tým aj všetky udalosti spojené s touto pozíciou", "Vymazať", "Zrušiť"))
            {
                if (await DisplayAlert(null, "Naozaj vykonať deštruktívnu operáciu?", "Vymazať", "Zrušiť"))
                {
                    if (await locationsViewModel.DeleteComponentAsync(position))
                    {
                        OnAppearing();
                        DependencyService.Get<IMessage>().LongAlert("Pozícia vymazaná");
                    }
                    else
                    {
                        OnAppearing();
                        DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                    }
                }
            }
        }

        private async void btnEditPosition_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            
            var position = btn.BindingContext as Position;
            var positionsViewModel = this.BindingContext as PositionsViewModel;
            positionsViewModel.SelectedComponent = position;
            await Navigation.PushAsync(new EditPositionPage(positionsViewModel));
        }

        private async void btnAddPosition_Clicked(object sender, EventArgs e)
        {
            var positionsViewModel = BindingContext as PositionsViewModel;

            if (positionsViewModel.NewComponent.Name == null || positionsViewModel.NewComponent.Name == "")
            {
                DependencyService.Get<IMessage>().LongAlert("Vyplňte názov pozície");
            }
            else
            {
                if (await positionsViewModel.AddComponentAsync(positionsViewModel.NewComponent))
                {
                    OnAppearing();
                    DependencyService.Get<IMessage>().LongAlert("Pozícia pridaná");
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

            //refreshujem list so vsetkymi poziciami
            await (BindingContext as PositionsViewModel).InitializeAllComponentsAsync();
        }

        private async void PositionsListView_Refreshing(object sender, EventArgs e)
        {
            await (BindingContext as PositionsViewModel).InitializeAllComponentsAsync();
            PositionsListView.IsRefreshing = false;
        }
    }
}