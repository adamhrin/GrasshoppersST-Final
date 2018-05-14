using Grasshoppers.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddInfoPage : ContentPage
	{
		public AddInfoPage(InfoViewModel infoViewModel = null)
		{
			InitializeComponent();
            if (infoViewModel == null)
            {
                infoViewModel = new InfoViewModel();
            }
            infoViewModel.Navigation = this.Navigation;
            BindingContext = infoViewModel;
        }

        private async void btnEditCategories_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditCategoriesPage());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //refreshujem list so vsetkymi kategoriami
            await (BindingContext as InfoViewModel).InitializeAllCategoriesAsync();
        }
    }
}