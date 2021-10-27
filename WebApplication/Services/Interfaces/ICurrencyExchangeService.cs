using System.Threading.Tasks;

namespace WebApplication.Services.Interfaces
{
    public interface ICurrencyExchangeService
    {
        Task<string> GetCurrencyExchangeRateAsync(string from, string to, decimal amount);
    }
}