using Grasshoppers.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditLeaguePage : ContentPage
    {
        public EditLeaguePage(LeaguesViewModel leaguesViewModel)
        {
            InitializeComponent();
            leaguesViewModel.Navigation = this.Navigation;
            BindingContext = leaguesViewModel;
        }
    }
}