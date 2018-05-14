using Grasshoppers.ViewModels;
using System;

using Xamarin.Forms;

namespace Grasshoppers.Views
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditPlayerPage : ContentPage
	{
		public EditPlayerPage(PlayersViewModel playersViewModel)
		{
			InitializeComponent();

            BindingContext = playersViewModel;
		}

       /**
       * Besides Button_Clicked, also Command will be performed (in PlayerViewModel.cs class)
       */
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}