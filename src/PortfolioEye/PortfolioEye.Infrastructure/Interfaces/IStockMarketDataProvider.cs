using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;

namespace PortfolioEye.Infrastructure.Interfaces;

public interface IStockMarketDataProvider
{
    Task<IEnumerable<FindResult>> FindTickerAsync(string query);
    Task<HistoricalData> GetHistoricalDataAsync(string ticker, DateOnly from, DateOnly to);
}

public record FindResult(string Ticker, string Name, string Type, string Exchange);

public record HistoricalData(string Currency, IEnumerable<HistoricalDay> Days);
public record HistoricalDay(
    DateOnly Day,
    decimal Open,
    decimal Close,
    decimal High,
    decimal Low,
    decimal AdjustedClose);