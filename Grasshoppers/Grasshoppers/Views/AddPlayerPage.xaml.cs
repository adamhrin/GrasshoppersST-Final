using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPlayerPage : ContentPage
    {
        public AddPlayerPage()
        {
            InitializeComponent();
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