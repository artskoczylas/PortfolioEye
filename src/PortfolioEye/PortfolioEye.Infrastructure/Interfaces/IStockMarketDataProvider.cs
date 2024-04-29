namespace PortfolioEye.Infrastructure.Interfaces;

public interface IStockMarketDataProvider
{
    Task<IEnumerable<string>> FindTickerAsync(string query);
    Task<HistoricalData> GetHistoricalDataAsync(string ticker, DateOnly from, DateOnly to);
}

public record HistoricalData(string Currency, IEnumerable<HistoricalDay> Days);
public record HistoricalDay(
    DateOnly Day,
    decimal Open,
    decimal Close,
    decimal High,
    decimal Low,
    decimal AdjustedClose);