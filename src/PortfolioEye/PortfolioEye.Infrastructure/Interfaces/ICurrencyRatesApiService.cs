namespace PortfolioEye.Infrastructure.Interfaces;

public interface ICurrencyRatesApiService
{
    Task<IEnumerable<DayRate>?> GetRates(string currency, DateOnly from, DateOnly to);
}
public record DayRate(DateOnly Date, decimal Rate, string Currency);