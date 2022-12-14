using FinanceManager.Domain.Models;
using FinanceManager.FinancialModelAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.FinancialModelAPI
{
    public class FinancialModelAPIHttpClient
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;

        // get the client and setup from dependecy injection (dependecy injection will manage the life
        // of the client and keep us from spooling up many sockets)
        public FinancialModelAPIHttpClient(HttpClient client, FinancialModelAPIKey apiKey) 
        {
            _client = client;
            _apiKey = apiKey.Key;
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            // given a uri, return a json object with the data so we can deserialize it to a class
            await Task.Delay(600);
            HttpResponseMessage response = await _client.GetAsync($"{uri}?apikey={_apiKey}");
            string jsonResponse = await response.Content.ReadAsStringAsync();
            T deserializedObject = default(T);
            try
            {
                deserializedObject = JsonConvert.DeserializeObject<T>(jsonResponse);
            }
            catch (Exception ex) 
            {
                ex.ToString();
            }
            return deserializedObject;
        }
    }
}
