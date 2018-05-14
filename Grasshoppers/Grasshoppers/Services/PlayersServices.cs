using Grasshoppers.Helpers;
using Grasshoppers.Models;
using Grasshoppers.RestClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Grasshoppers.Services
{
    public class PlayersServices // responsible for getting and creating players
    {
        private RestClient<Player> _restClient = new RestClient<Player>();

        public async Task<List<Player>> GetPlayersAsync()
        {
            _restClient.Resource = "players";

            var listOfPlayers = await _restClient.GetAsync();

            return listOfPlayers;
        }

        public async Task<bool> PostPlayerAsync(Player player)
        {
            RestClient<Player> restClient = new RestClient<Player>();

            var success = await restClient.PostAsync(player);

            return success;
        }

        public async Task<bool> PutPlayerAsync(Player player)
        {
            _restClient.Resource = "player";

            var success = await _restClient.PutAsync(player);

            return success;
        }

        public async Task<bool> DeletePlayerAsync(int id)
        {
            RestClient<Player> restClient = new RestClient<Player>();

            var success = await restClient.DeleteAsync(id);

            return success;
        }

        public async Task<List<Player>> SearchPlayersAsync(string keyword)
        {
            RestClient<Player> restClient = new RestClient<Player>();

            var listOfFoundPlayers = await restClient.SearchAsync(keyword);

            return listOfFoundPlayers;
        }

        public async Task<ObservableCollection<Player>> GetPlayersInCategoryAsync(int id)
        { 
            _restClient.Resource = "category/" + id + "/players";

            var listOfPlayers = await _restClient.GetAsync();

            return new ObservableCollection<Player>(listOfPlayers as List<Player>);
        }

        public async Task<int> RegisterPlayerAsync(Player playerToRegister)
        {
            _restClient.Resource = "player/registration";

            var response = await _restClient.RegisterAsync(playerToRegister);
            if (response != null)
            {
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.Conflict) // 409 - Conflict
                    {
                        return -1; // taky player s takym emailom a pwd uz existuje
                    }
                    else
                    {
                        return 0; // ina chyba na serveri
                    }
                }
                else
                {
                    return 1; // OK
                }
            }
            return -2; // chyba v pripojeni
        }

        public async Task<Player> LoginPlayerAsync(Player playerToLogin) //ma v sebe email a pwd prihlasovaneho
        {
            _restClient.Resource = "player/login";

            HttpResponseMessage response = await _restClient.LoginAsync(playerToLogin);

            Player playerReceived = new Player();
            if (response != null)
            {
                // response moze byt:
                // OK - idPlayera nastavena na ............................................. jeho realne id
                // ina chyba na serveri - idPlayera nastavena na ........................... 0
                // nenajdeny player, zle prihlasovacie udaje - idPlayera nastavena na ..... -1

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NotAcceptable) // player nenajdeny v db (-1) // chyba 406
                    {
                        playerReceived.Id = -1;
                    }
                    else // ina chyba na serveri
                    {
                        playerReceived.Id = 0;
                    }
                }
                else // OK
                {
                    var r = await response.Content.ReadAsStringAsync();
                    playerReceived = JsonConvert.DeserializeObject<Player>(r);
                }
            } 
            else
            {
                playerReceived.Id = -2; //chyba v pripojeni
            }
            return playerReceived;
        }

        public async Task<Player> GetPlayerAsync()
        {
            _restClient.Resource = "player/" + Settings.IdPlayer;

            Player player = await _restClient.GetOneAsync();

            return player;
        }

        public async Task<bool> PutPlayersAdminAsync(List<Player> playersList)
        {
            RestClient<List<Player>> rest = new RestClient<List<Player>>();
            rest.Resource = "players";
            var success = await rest.PutAsync(playersList);
            return success;
        }
    }
}
