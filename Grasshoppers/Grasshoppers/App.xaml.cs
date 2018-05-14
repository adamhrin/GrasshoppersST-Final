
using Grasshoppers.Helpers;
using Grasshoppers.Models;
using Grasshoppers.Services;
using Xamarin.Forms;

namespace Grasshoppers
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            
            SetMainPage();
            //MainPage = new NavigationPage(new Grasshoppers.Views.RegisterPlayerPage());
            //MainPage = new NavigationPage(new Grasshoppers.Views.BasePage());
        }

        private void SetMainPage()
        {
            if (Settings.IdPlayer != 0)
            {
                MainPage = new NavigationPage(new Grasshoppers.Views.BasePage());
            }
            else if (!string.IsNullOrEmpty(Settings.Email) && !string.IsNullOrEmpty(Settings.Password))
            {
                MainPage = new NavigationPage(new Grasshoppers.Views.LoginPlayerPage(new ViewModels.PlayersViewModel()));
            }
            else
            {
                MainPage = new NavigationPage(new Grasshoppers.Views.RegisterPlayerPage());
            }
        }

        protected override async void OnStart()
        {
            if (Settings.IdPlayer != 0)
            {
                PlayersServices ps = new PlayersServices();
                Player player = await ps.GetPlayerAsync();
                Settings.AdminLevel = player.AdminLevel;
            }
           
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
