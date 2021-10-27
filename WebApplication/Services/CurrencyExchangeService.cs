using System;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication.Services.Interfaces;

namespace WebApplication.Services
{
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        private readonly HttpClient _httpClient;

        public CurrencyExchangeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetCurrencyExchangeRateAsync(string from, string to, decimal amount)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://currency-exchange.p.rapidapi.com/exchange?from={from}&to={to}&q={amount}"),
                Headers =
                {
                    {"x-rapidapi-host", "currency-exchange.p.rapidapi.com"},
                    {"x-rapidapi-key", "60b9dea975msh1975a0f39f30371p19f911jsnaa8c5d78568a"},
                }
            };

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            return body;
        }
    }
}