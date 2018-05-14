using Grasshoppers.Interfaces;
using Grasshoppers.Models;
using Grasshoppers.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCategoriesPage : ContentPage
    {
        public EditCategoriesPage()
        {
            InitializeComponent();
        }

        private async void btnDeleteCategory_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            
            var category = btn.BindingContext as Category;

            var categoriesViewModel = BindingContext as CategoriesViewModel;
            if (await DisplayAlert(null, "Naozaj chceš vymazať kategóriu " + category.Name + "? Zmažú sa tým aj všetky udalosti spojené s touto kategóriou", "Vymazať", "Zrušiť"))
            {
                if (await DisplayAlert(null, "Naozaj vykonať deštruktívnu operáciu?", "Vymazať", "Zrušiť"))
                {
                    if (await categoriesViewModel.DeleteComponentAsync(category))
                    {
                        OnAppearing();
                        DependencyService.Get<IMessage>().LongAlert("Kategória vymazaná");
                    }
                    else
                    {
                        OnAppearing();
                        DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                    }
                }
            }
        }

        private async void btnEditCategory_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            
            var category = btn.BindingContext as Category;

            var categoriesViewModel = this.BindingContext as CategoriesViewModel;
            categoriesViewModel.SelectedComponent = category;
            await Navigation.PushAsync(new EditCategoryPage(categoriesViewModel));
        }

        private async void btnAddCategory_Clicked(object sender, EventArgs e)
        {
            var categoriesViewModel = BindingContext as CategoriesViewModel;

            if (categoriesViewModel.NewComponent.Name == null || categoriesViewModel.NewComponent.Name == "")
            {
                DependencyService.Get<IMessage>().LongAlert("Vyplňte názov kategórie");
            }
            else
            {
                if (await categoriesViewModel.AddComponentAsync(categoriesViewModel.NewComponent))
                {
                    OnAppearing();
                    DependencyService.Get<IMessage>().LongAlert("Kategória pridaná");
                }
                else
                {
                    OnAppearing();
                    DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                }
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //refreshujem list so vsetkymi kategoriami
            await (BindingContext as CategoriesViewModel).InitializeAllComponentsAsync();
        }

        private async void CategoriesListView_Refreshing(object sender, EventArgs e)
        {
            await (BindingContext as CategoriesViewModel).InitializeAllComponentsAsync();
            CategoriesListView.IsRefreshing = false;
        }
    }
}