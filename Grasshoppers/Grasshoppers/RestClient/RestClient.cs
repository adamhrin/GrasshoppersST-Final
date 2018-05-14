using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Grasshoppers.Helpers;
using Grasshoppers.Initializers;
using Grasshoppers.Models;
using Newtonsoft.Json;

namespace Grasshoppers.RestClient
{
    /// <summary>
    /// RestClient implements methods for calling CRUD operations
    /// using HTTP.
    /// </summary>
    public class RestClient<T> // connection between web app and mobile app 
        where T : new()
    {
        /// private const string WebServiceUrl = "http://grasshoppersdirectory.azurewebsites.net/api/Players/";
        /// private const string WebServiceUrl = "http://localhost:51028/api/Players/";
        

        public string Resource { get; set; }
        
        public string SecuredWebServiceUrl
        { 
            get { return Connectivity.SECURED_URL_ADDRESS + Resource; }
        }

        public string NotSecuredWebServiceUrl
        {
            get { return Connectivity.NOT_SECURED_URL_ADDRESS + Resource; }
        }

        public void FillAuthHeader(HttpClient httpClient, string email = "", string password = "")
        {
            if (string.IsNullOrEmpty(email))
            {
                email = Settings.Email;
            }
            if (string.IsNullOrEmpty(password))
            {
                password = Settings.Password;
            }
            string toEncryptString = email + ":" + password;
            byte[] bytes = Encoding.UTF8.GetBytes(toEncryptString);
            string encodedString = Convert.ToBase64String(bytes);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedString);
        }

        public async Task<List<T>> GetAsync()
        {
            var listOfT = new List<T>();
            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                var httpClient = new HttpClient();

                this.FillAuthHeader(httpClient);

                var json = await httpClient.GetStringAsync(SecuredWebServiceUrl);

                listOfT = JsonConvert.DeserializeObject<List<T>>(json);
            });
            return listOfT;
        }

        public async Task<T> GetOneAsync()
        {
            var t = new T();
            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                var httpClient = new HttpClient();

                this.FillAuthHeader(httpClient);

                var json = await httpClient.GetStringAsync(SecuredWebServiceUrl);

                t = JsonConvert.DeserializeObject<T>(json);
            });
            return t;
        }

        public async Task<HttpResponseMessage> RegisterAsync(T t)
        {

            HttpResponseMessage response = null;
            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                var httpClient = new HttpClient();

                var json = JsonConvert.SerializeObject(t);

                HttpContent httpContent = new StringContent(json);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                response = await httpClient.PostAsync(NotSecuredWebServiceUrl, httpContent);
            });
            return response;
        }
        
        public async Task<HttpResponseMessage> LoginAsync(Player playerToLogin)
        {
            HttpResponseMessage response = null;

            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                var httpClient = new HttpClient();

                //this.FillAuthHeader(httpClient, playerToLogin.Email, playerToLogin.Password);

                var jsonToSend = JsonConvert.SerializeObject(playerToLogin);

                HttpContent httpContent = new StringContent(jsonToSend);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                response = await httpClient.PostAsync(NotSecuredWebServiceUrl, httpContent);
            });
            return response;
        }

        public async Task<bool> PostAsync(T t)
        {
            bool toReturn = false;
            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                var httpClient = new HttpClient();

                this.FillAuthHeader(httpClient);

                var json = JsonConvert.SerializeObject(t);

                HttpContent httpContent = new StringContent(json);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var result = await httpClient.PostAsync(SecuredWebServiceUrl, httpContent);

                toReturn = result.IsSuccessStatusCode;
            });
            return toReturn;
        }

        public async Task<bool> PutAsync(T t)
        {
            bool toReturn = false;
            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                var httpClient = new HttpClient();

                this.FillAuthHeader(httpClient);

                var json = JsonConvert.SerializeObject(t);

                HttpContent httpContent = new StringContent(json);

                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var result = await httpClient.PutAsync(SecuredWebServiceUrl, httpContent);

                toReturn = result.IsSuccessStatusCode;
            });
            return toReturn;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool toReturn = false;
            await Connectivity.DoIfConnectedAndReachable(async () =>
            {
                var httpClient = new HttpClient();

                this.FillAuthHeader(httpClient);

                var response = await httpClient.DeleteAsync(SecuredWebServiceUrl + id);

                toReturn = response.IsSuccessStatusCode;
            });
            return toReturn;
        }

        public async Task<List<T>> SearchAsync(string keyword)
        {
            var httpClient = new HttpClient();

            this.FillAuthHeader(httpClient);

            var json = await httpClient.GetStringAsync(SecuredWebServiceUrl + "Search/" + keyword);

            var foundPlayers = JsonConvert.DeserializeObject<List<T>>(json);

            return foundPlayers;
        }
    }
}
