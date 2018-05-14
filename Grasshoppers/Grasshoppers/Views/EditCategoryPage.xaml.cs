using Grasshoppers.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grasshoppers.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditCategoryPage : ContentPage
    {
        public EditCategoryPage(CategoriesViewModel categoriesViewModel)
        {
            InitializeComponent();
            categoriesViewModel.Navigation = this.Navigation;
            BindingContext = categoriesViewModel; 
        }
    }
}