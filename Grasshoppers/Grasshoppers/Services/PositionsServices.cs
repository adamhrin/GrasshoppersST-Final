using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Grasshoppers.Helpers;
using Grasshoppers.Models;
using Grasshoppers.RestClient;

namespace Grasshoppers.Services
{
    public class PositionsServices : IComponentsServices<Position>
    {
        private RestClient<Position> _restClient = new RestClient<Position>();

        public async Task<bool> RegisterPlayerOnPositionAsync(int selectedBrigadeId, Position position)
        {
            _restClient.Resource = "player/" + Settings.IdPlayer + "/brigade/" + selectedBrigadeId + "/position/" + position.Id + "/registration";
            var success = await _restClient.PutAsync(position);
            return success;
        }

        public async Task<bool> UnregisterPlayerFromPositionAsync(int selectedBrigadeId, Position position)
        {
            _restClient.Resource = "player/" + Settings.IdPlayer + "/brigade/" + selectedBrigadeId + "/position/" + position.Id + "/unregistration";
            var success = await _restClient.PutAsync(position);
            return success;
        }

        public async Task<ObservableCollection<Position>> GetAllComponentsAsync()
        {
            _restClient.Resource = "positions";
            var listOfPositions = await _restClient.GetAsync();
            return new ObservableCollection<Position>(listOfPositions as List<Position>);
        }

        public async Task<bool> PostComponentAsync(Position newPosition)
        {
            _restClient.Resource = "position";
            var success = await _restClient.PostAsync(newPosition);
            return success;
        }

        public async Task<bool> PutComponentAsync(int id, Position selectedPosition)
        {
            _restClient.Resource = "position";
            var success = await _restClient.PutAsync(selectedPosition);
            return success;
        }

        public async Task<bool> DeleteComponentAsync(int id)
        {
            _restClient.Resource = "position/";
            var success = await _restClient.DeleteAsync(id);
            return success;
        }
    }
}
