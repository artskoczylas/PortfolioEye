namespace PortfolioEye.Infrastructure.Interfaces;

public interface ICurrencyRatesApiService
{
    Task<IEnumerable<DayRate>?> GetRates(string fromCurrency, string toCurrency, DateOnly from, DateOnly to);
}

public record DayRate(DateOnly Date, decimal Rate, string FromCurrency, string ToCurrency)
{
    public override string ToString()
    {
        return $"{Date}: 1 {FromCurrency} = {Rate} {ToCurrency}";
    }
}