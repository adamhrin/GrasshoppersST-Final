using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grasshoppers.Helpers;
using Grasshoppers.Models;
using Grasshoppers.RestClient;

namespace Grasshoppers.Services
{
    public class BrigadesServices
    {
        private RestClient<Brigade> _restClient = new RestClient<Brigade>();

        public async Task<List<Brigade>> GetBrigadesForMonth(DateTime now)
        {
            _restClient.Resource = "player/" + Settings.IdPlayer + "/brigades";

            var listOfBrigades = await _restClient.GetAsync();

            return listOfBrigades;
        }

        public async Task<bool> DeleteBrigadeAsync(int id)
        {
            _restClient.Resource = "brigade/";
            var success = await _restClient.DeleteAsync(id);
            return success;
        }

        public async Task<bool> PushBrigadeAsync(Brigade selectedBrigade)
        {
            _restClient.Resource = "brigade";

            var success = await _restClient.PostAsync(selectedBrigade);

            return success;
        }

        public async Task<bool> PutBrigadeAsync(int id, Brigade selectedBrigade)
        {
            _restClient.Resource = "brigade";
            var success = await _restClient.PutAsync(selectedBrigade);
            return success;
        }
    }
}
