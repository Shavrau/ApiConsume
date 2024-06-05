using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace CurrencyConverter
{
    public class CurrencyService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        private static readonly string BaseUrl = "https://v6.exchangerate-api.com/v6/";

        public CurrencyService(string apiKey)
        {
            _apiKey = apiKey;
            _httpClient = new HttpClient();
        }

        public async Task<decimal> ConvertCurrency(decimal amount, string fromCurrency, string toCurrency)
        {
            string url = $"{BaseUrl}{_apiKey}/latest/{fromCurrency}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);

            if (json["conversion_rates"][toCurrency] == null)
            {
                throw new Exception("Moeda de destino não encontrada.");
            }

            decimal rate = json["conversion_rates"][toCurrency].Value<decimal>();
            return amount * rate;
        }

        public async Task<decimal> GetExchangeRate(string fromCurrency, string toCurrency)
        {
            string url = $"{BaseUrl}{_apiKey}/latest/{fromCurrency}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);

            if (json["conversion_rates"][toCurrency] == null)
            {
                throw new Exception("Moeda não encontrada.");
            }

            return json["conversion_rates"][toCurrency].Value<decimal>();
        }
    }
}
