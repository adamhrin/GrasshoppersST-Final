using Grasshoppers.Models;
using Grasshoppers.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditOpponentScorePage : ContentPage
	{
		public EditOpponentScorePage(MatchesViewModel matchesViewModel)
		{
			InitializeComponent();
            matchesViewModel.NewGoal = new Goal();
            Title = "Gól " + matchesViewModel.SelectedMatch.OpponentName;
            BindingContext = matchesViewModel;
        }
	}
}