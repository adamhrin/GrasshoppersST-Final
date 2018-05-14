using Grasshoppers.Helpers;
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
    public partial class LoginPlayerPage : ContentPage
    {
        public LoginPlayerPage(PlayersViewModel playersViewModel)
        {
            InitializeComponent();
            playersViewModel.PlayerToLogin.Email = Settings.Email;
            playersViewModel.PlayerToLogin.Password = Settings.Password;
            BindingContext = playersViewModel;
        }

        private async void btnGoToRegistrationClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPlayerPage());
        }
    }
}