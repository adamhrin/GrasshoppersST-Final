using Grasshoppers.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InvitedPlayersPage : ContentPage
	{
        public InvitedPlayersPage(TrainingsViewModel trainingsViewModel)
		{
			InitializeComponent();
            BindingContext = trainingsViewModel;
        }
    }
}