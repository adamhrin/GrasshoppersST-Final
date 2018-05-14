using Grasshoppers.Initializers;
using Grasshoppers.Interfaces;
using Grasshoppers.Models;
using Grasshoppers.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrigadePage : ContentPage
    {
        public BrigadePage(BrigadesViewModel brigadesViewModel)
        {
            InitializeComponent();
            brigadesViewModel.Navigation = this.Navigation;
            BindingContext = brigadesViewModel;
            SetEditDeleteBrigadeVisibility();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void SetEditDeleteBrigadeVisibility()
        {
            //moznost mazat a editovat brigadu pre admina
            EditDeleteBrigadeGrid.IsVisible = (BindingContext as BrigadesViewModel).IsAdmin;
        }

        private async void btnEditBrigade_Clicked(object sender, EventArgs e)
        {
            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                await Navigation.PushAsync(new EditSelectedBrigadePage(BindingContext as BrigadesViewModel));
            });
        }

        private async void btnDeleteBrigade_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert(null, "Naozaj chceš vymazať túto brigádu?", "Vymazať", "Zrušiť"))
            {
                var brigadesViewModel = BindingContext as BrigadesViewModel;
                if (await brigadesViewModel.DeleteSelectedBrigadeAsync())
                {
                    DependencyService.Get<IMessage>().LongAlert("Brigáda vymazaná");
                }
                else
                {
                    DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                }
                await Navigation.PopAsync();
            }
        }

        private void btnRegisterPosition_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            
            var position = btn.BindingContext as Position;

            var brigadesViewModel = BindingContext as BrigadesViewModel;
            brigadesViewModel.SelectedPosition = position;
            Command cmd = brigadesViewModel.RegisterPlayerOnPositionCommand;
            cmd.Execute(null);

            //if (await brigadesViewModel.RegisterPlayerOnPositionAsync(position))
            //{
            //    DependencyService.Get<IMessage>().LongAlert("Si úspešne prihlásený");
            //}
            //else
            //{
            //    DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
            //}
            //await Navigation.PopAsync();
        }

        private async void btnUnregisterPosition_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            
            var position = btn.BindingContext as Position;

            var brigadesViewModel = BindingContext as BrigadesViewModel;
            if (await brigadesViewModel.UnregisterPlayerFromPositionAsync(position))
            {
                DependencyService.Get<IMessage>().LongAlert("Si úspešne odhlásený");
            }
            else
            {
                DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
            }
            //await Navigation.PopAsync();
        }
    }
}