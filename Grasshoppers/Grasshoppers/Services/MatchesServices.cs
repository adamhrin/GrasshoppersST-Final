using Grasshoppers.Helpers;
using Grasshoppers.Models;
using Grasshoppers.RestClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grasshoppers.Services
{
    public class MatchesServices
    {
        private RestClient<Match> _restClient = new RestClient<Match>();

        public async Task<List<Match>> GetMatchesForMonth(DateTime now)
        {
            _restClient.Resource = "player/" + Settings.IdPlayer + "/matches";

            var listOfMatches = await _restClient.GetAsync();

            return listOfMatches;
        }

        public async Task<Match> GetMatchById(int idMatch)
        {
            _restClient.Resource = "player/" + Settings.IdPlayer + "/match/" + idMatch;

            var match = await _restClient.GetOneAsync();
            
            return match;
        }

        public async Task<bool> DeleteMatchAsync(int id)
        {
            _restClient.Resource = "match/";
            var success = await _restClient.DeleteAsync(id);
            return success;
        }

        public async Task<bool> PushMatchAsync(Match selectedMatch)
        {
            _restClient.Resource = "match";

            var success = await _restClient.PostAsync(selectedMatch);

            return success;
        }

        public async Task<bool> AddGrassGoalToMatchAsync(Goal newGoal, int idMatch)
        {
            RestClient<Goal> restClient = new RestClient<Goal>
            {
                Resource = "match/" + idMatch + "/grassGoal"
            };
            var success = await restClient.PostAsync(newGoal);
            return success;
        }

        public async Task<bool> AddOpponentGoalToMatchAsync(Goal newGoal, int idMatch)
        {
            RestClient<Goal> restClient = new RestClient<Goal>
            {
                Resource = "match/" + idMatch + "/opponentGoal"
            };
            var success = await restClient.PostAsync(newGoal);
            return success;
        }

        public async Task<bool> PutMatchAsync(int id, Match selectedMatch)
        {
            _restClient.Resource = "match";
            var success = await _restClient.PutAsync(selectedMatch);
            return success;
        }

        public async Task<bool> DeleteGoalGrassAsync(int idGoal)
        {
            _restClient.Resource = "grassGoal/";
            var success = await _restClient.DeleteAsync(idGoal);
            return success; 
        }

        public async Task<bool> DeleteGoalOpponentAsync(int idGoal)
        {
            _restClient.Resource = "opponentGoal/";
            var success = await _restClient.DeleteAsync(idGoal);
            return success;
        }
    }
}
