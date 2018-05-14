using Grasshoppers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlayerAccountPage : ContentPage
	{
		public PlayerAccountPage ()
		{
			InitializeComponent ();
            (BindingContext as PlayersViewModel).Navigation = this.Navigation;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            PlayersViewModel playersViewModel = BindingContext as PlayersViewModel;

            await playersViewModel.InitializePlayerAccount();

            //refreshujem list so vsetkymi kategoriami
            await playersViewModel.InitializeAllCategoriesAsync();
            playersViewModel.InitializeCategoriesForSelectedPlayer();
        }
    }
}