namespace PortfolioEye.Infrastructure.Interfaces;

public interface ICurrencyRateService
{
    Task<decimal> GetRateAsync(string fromCurrency, string toCurrency, DateOnly date);
    Task CheckRatesAsync(string fromCurrency, string toCurrency, DateOnly fromDate, DateOnly toDate);
}