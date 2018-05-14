using Grasshoppers.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditLocationPage : ContentPage
	{
        public EditLocationPage(LocationsViewModel locationsViewModel)
        {
            InitializeComponent();
            locationsViewModel.Navigation = this.Navigation;
            BindingContext = locationsViewModel;
        }
    }
}