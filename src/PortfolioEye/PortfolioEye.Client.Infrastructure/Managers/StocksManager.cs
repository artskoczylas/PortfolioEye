using PortfolioEye.Application.Features.Stocks.Queries;
using PortfolioEye.Client.Infrastructure.Extensions;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Client.Infrastructure.Managers;

public class StocksManager(IHttpClientFactory factory) : IStocksManager
{
    public async Task<IResult<SearchTickersQuery.Response>> SearchForTickers(string query)
    {
        var response = await factory.MainApiClient().GetAsync($"api/stocks/search?query={query}");
        return await response.ToResultAsync<SearchTickersQuery.Response>();
    }

    public async Task<IResult<GetStockDetailsQuery.Response>> GetDetails(string ticker)
    {
        var response = await factory.MainApiClient().GetAsync($"api/stocks/details/{ticker}");
        return await response.ToResultAsync<GetStockDetailsQuery.Response>();
    }

    public async Task<IResult<GetStockHistoryQuery.Response>> GetHistory(string ticker, DateOnly from, DateOnly to)
    {
        var response = await factory.MainApiClient().GetAsync($"api/stocks/History/{ticker}?from={from.ToString("yyyy-MM-dd")}&to={to.ToString("yyyy-MM-dd")}");
        return await response.ToResultAsync<GetStockHistoryQuery.Response>();
    }
}

public interface IStocksManager : IManager
{
    Task<IResult<SearchTickersQuery.Response>> SearchForTickers(string query);
    Task<IResult<GetStockDetailsQuery.Response>> GetDetails(string ticker);
    Task<IResult<GetStockHistoryQuery.Response>> GetHistory(string ticker, DateOnly from, DateOnly to);
}