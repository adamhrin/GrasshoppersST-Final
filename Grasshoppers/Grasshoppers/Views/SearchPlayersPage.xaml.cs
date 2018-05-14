
using Xamarin.Forms;
using Grasshoppers.ViewModels;

namespace Grasshoppers.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPlayersPage : ContentPage
    {
        public SearchPlayersPage(PlayersViewModel playersViewModel)
        {
            InitializeComponent();

            BindingContext = playersViewModel;
        }
    }
}