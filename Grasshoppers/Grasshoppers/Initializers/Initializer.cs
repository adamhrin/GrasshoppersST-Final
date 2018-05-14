using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Grasshoppers.Models;
using Grasshoppers.Services;

namespace Grasshoppers.Initializers
{
    public static class Initializer
    {
        public static async Task<ObservableCollection<Location>> InitializeAllLocationsAsync()
        {
            var locationsServices = new LocationsServices();
            return await locationsServices.GetAllComponentsAsync();
        }

        public static async Task<ObservableCollection<Category>> InitializeAllCategoriesAsync()
        {
            var categoriesServices = new CategoriesServices();
            return await categoriesServices.GetAllComponentsAsync();
        }

        public static async Task<ObservableCollection<Position>> InitializeAllPositionsAsync()
        {

            var positionsServices = new PositionsServices();
            return await positionsServices.GetAllComponentsAsync();
        }

        public static async Task<ObservableCollection<League>> InitializeAllLeaguesAsync()
        {
            var leaguesServices = new LeaguesServices();
            return await leaguesServices.GetAllComponentsAsync();
        }

        public  static async Task<ObservableCollection<Player>> InitializeAllPlayersInCategoryOfSelectedMatchAsync(int id)
        {
            var playersServices = new PlayersServices();
            return await playersServices.GetPlayersInCategoryAsync(id);
        }
    }
}
