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
    public partial class InfoPage : ContentPage
    {
        public InfoPage()
        {
            InitializeComponent();
            var infoViewModel = new InfoViewModel();
            infoViewModel.Navigation = this.Navigation;
            BindingContext = infoViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as InfoViewModel).InitializeInfo();
        }

        private void InfoListView_ItemTapped(object sender, EventArgs e)
        {
            var tappedInfo = InfoListView.SelectedItem as Info;
            if (tappedInfo != null)
            {
                tappedInfo.IsWholeContentVisible = !tappedInfo.IsWholeContentVisible;
            }
        }

        private async void InfoListView_Refreshing(object sender, EventArgs e)
        {
            await (BindingContext as InfoViewModel).InitializeInfo();
            InfoListView.IsRefreshing = false;
        }

        private async void DeleteInfo_Tapped(object sender, EventArgs e)
        {
            var img = sender as Image;

            var info = img.BindingContext as Info;

            var infoViewModel = BindingContext as InfoViewModel;
            infoViewModel.SelectedInfo = info;
            if (await DisplayAlert(null, "Naozaj chceš vymazať tento oznam?", "Vymazať", "Zrušiť"))
            {
                if (await infoViewModel.DeleteSelectedInfoAsync())
                {
                    OnAppearing();
                    DependencyService.Get<IMessage>().LongAlert("Oznam vymazaný");
                }
                else
                {
                    OnAppearing();
                    DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                }
            }
        }

        private async void EditInfo_Tapped(object sender, EventArgs e)
        {
            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                var img = sender as Image;

                var info = img.BindingContext as Info;

                var infoViewModel = this.BindingContext as InfoViewModel;
                infoViewModel.SelectedInfo = info;
                await Navigation.PushAsync(new EditSelectedInfoPage(infoViewModel));
            });
        }
    }
}