using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Grasshoppers.Models;
using Grasshoppers.RestClient;

namespace Grasshoppers.Services
{
    public class CategoriesServices : IComponentsServices<Category>
    {
        private RestClient<Category> _restClient = new RestClient<Category>();

        public async Task<ObservableCollection<Category>> GetAllComponentsAsync()
        {
            _restClient.Resource = "categories";
            var listOfCategories = await _restClient.GetAsync();
            return new ObservableCollection<Category>(listOfCategories as List<Category>);
        }

        public async Task<bool> PostComponentAsync(Category newCategory)
        {
            _restClient.Resource = "category";
            var success = await _restClient.PostAsync(newCategory);
            return success;
        }

        public async Task<bool> PutComponentAsync(int id, Category selectedCategory)
        {
            _restClient.Resource = "category";
            var success = await _restClient.PutAsync(selectedCategory);
            return success;
        }

        public async Task<bool> DeleteComponentAsync(int id)
        {
            _restClient.Resource = "category/";
            var success = await _restClient.DeleteAsync(id);
            return success;
        }
    }
}
