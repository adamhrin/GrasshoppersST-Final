using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grasshoppers.Helpers;
using Grasshoppers.Models;
using Grasshoppers.RestClient;

namespace Grasshoppers.Services
{
    public class TrainingsServices
    {
        private RestClient<Training> _restClient = new RestClient<Training>();
        
        public async Task<List<Training>> GetTrainingsForMonthAsync(DateTime monthDate)
        {
            _restClient.Resource = "player/" + Settings.IdPlayer + "/trainings";

            var listOfTrainings = await _restClient.GetAsync();

            return listOfTrainings;
        }

        public async Task<bool> PutTrainingAsync(int id, Training selectedTraining)
        {
            _restClient.Resource = "training";
            var success = await _restClient.PutAsync(selectedTraining);
            return success;
        }
        
        public async Task<bool> DeleteTrainingAsync(int id)
        {
            _restClient.Resource = "training/";
            var success = await _restClient.DeleteAsync(id);
            return success;
        }

        public async Task<bool> PushTrainingAsync(Training selectedTraining)
        {
            _restClient.Resource = "training";

            var success = await _restClient.PostAsync(selectedTraining);

            return success;
        }

        public async Task<bool> PutPlayerOnTrainingAsync(Training selectedTraining)
        {
            string acceptString = "";
            if (selectedTraining.AcceptedByPlayer == Enums.AcceptsTrainingOptions.Accepted)
            {
                acceptString = "accept";
            }
            else if (selectedTraining.AcceptedByPlayer == Enums.AcceptsTrainingOptions.Declined)
            {
                acceptString = "decline";
            }

            _restClient.Resource = "player/" + Settings.IdPlayer + "/" + acceptString + "/training/" + selectedTraining.Id;

            var success = await _restClient.PutAsync(selectedTraining);

            return success;
        }
    }
}