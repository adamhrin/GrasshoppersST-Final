using Grasshoppers.Models;
using Grasshoppers.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditGrassScorePage : ContentPage
	{
		public EditGrassScorePage(MatchesViewModel matchesViewModel)
		{
			InitializeComponent();
            matchesViewModel.NewGoal = new Goal();
            BindingContext = matchesViewModel;
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //refreshujem list so vsetkymi sutazami
            await (BindingContext as MatchesViewModel).InitializeAllPlayersInCategoryOfSelectedMatch();
        }
    }
}