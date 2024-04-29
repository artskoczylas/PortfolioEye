using CsvHelper;
using PortfolioEye.Infrastructure.Interfaces;
using YahooFinanceApi;

namespace PortfolioEye.Infrastructure.Services;

public class YahooStockMarketDataProvider : IStockMarketDataProvider
{
    public Task<IEnumerable<string>> FindTickerAsync(string query)
    {
        throw new NotImplementedException();
    }

    public async Task<HistoricalData> GetHistoricalDataAsync(string ticker, DateOnly from, DateOnly to)
    {
        var data = await Yahoo.GetHistoricalAsync(ticker, from.ToDateTime(new TimeOnly()),
            to.ToDateTime(new TimeOnly()));
        var result = data.Select(item => new HistoricalDay(DateOnly.FromDateTime(item.DateTime), item.Open,
            item.Close, item.High, item.Low, item.AdjustedClose)).ToList();

        var stockData =await Yahoo.Symbols(ticker).Fields(Field.Currency).QueryAsync();
        return new HistoricalData(stockData[ticker].Currency, result);
    }
}