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
    public partial class RegisterPlayerPage : ContentPage
    {
        public RegisterPlayerPage()
        {
            InitializeComponent();
            (BindingContext as PlayersViewModel).Navigation = this.Navigation;
        }

        private async void btnGoToLoginClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPlayerPage(BindingContext as PlayersViewModel));
        }
    }
}