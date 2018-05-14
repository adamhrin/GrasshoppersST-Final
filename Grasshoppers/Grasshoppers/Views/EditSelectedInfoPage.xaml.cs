using Grasshoppers.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditSelectedInfoPage : ContentPage
	{
		public EditSelectedInfoPage(InfoViewModel infoViewModel)
		{
			InitializeComponent();
            BindingContext = infoViewModel;
		}

        private async void btnEditCategories_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditCategoriesPage());
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var infoViewModel = BindingContext as InfoViewModel;

            //refreshujem list so vsetkymi kategoriami
            await infoViewModel.InitializeAllCategoriesAsync();
            infoViewModel.InitializeCategoriesOfSelectedInfo();
        }
    }
}