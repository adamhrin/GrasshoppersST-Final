using Grasshoppers.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPositionPage : ContentPage
    {
        public EditPositionPage(PositionsViewModel positionsViewModel)
        {
            InitializeComponent();
            positionsViewModel.Navigation = this.Navigation;
            BindingContext = positionsViewModel;
        }
    }
}