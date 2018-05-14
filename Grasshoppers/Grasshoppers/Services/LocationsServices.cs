using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Grasshoppers.Models;
using Grasshoppers.RestClient;

namespace Grasshoppers.Services
{
    public class LocationsServices : IComponentsServices<Location>
    {
        private RestClient<Location> _restClient = new RestClient<Location>();

        public async Task<ObservableCollection<Location>> GetAllComponentsAsync()
        {
            _restClient.Resource = "locations";
            var listOfLocations = await _restClient.GetAsync();
            return new ObservableCollection<Location>(listOfLocations as List<Location>);
        }

        public async Task<bool> PostComponentAsync(Location newLocation)
        {
            _restClient.Resource = "location";
            var success = await _restClient.PostAsync(newLocation);
            return success;
        }

        public async Task<bool> PutComponentAsync(int id, Location selectedLocation)
        {
            _restClient.Resource = "location";
            var success = await _restClient.PutAsync(selectedLocation);
            return success;
        }

        public async Task<bool> DeleteComponentAsync(int id)
        {
            _restClient.Resource = "location/";
            var success = await _restClient.DeleteAsync(id);
            return success;
        }
    }
}
