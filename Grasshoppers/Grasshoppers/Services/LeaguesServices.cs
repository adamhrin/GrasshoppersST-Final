using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Grasshoppers.Models;
using Grasshoppers.RestClient;

namespace Grasshoppers.Services
{
    public class LeaguesServices : IComponentsServices<League>
    {
        private RestClient<League> _restClient = new RestClient<League>();

        public async Task<ObservableCollection<League>> GetAllComponentsAsync()
        {
            _restClient.Resource = "leagues";
            var listOfLeagues = await _restClient.GetAsync();
            return new ObservableCollection<League>(listOfLeagues as List<League>);
        }

        public async Task<bool> PostComponentAsync(League newLeague)
        {
            _restClient.Resource = "league";
            var success = await _restClient.PostAsync(newLeague);
            return success;
        }

        public async Task<bool> PutComponentAsync(int id, League selectedLeague)
        {
            _restClient.Resource = "league";
            var success = await _restClient.PutAsync(selectedLeague);
            return success;
        }

        public async Task<bool> DeleteComponentAsync(int id)
        {
            _restClient.Resource = "league/";
            var success = await _restClient.DeleteAsync(id);
            return success;
        }
    }
}
