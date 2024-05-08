using System.Net.Http.Json;
using PortfolioEye.Application.Features.Stocks.Queries;
using PortfolioEye.Application.Features.Transactions.Commands;
using PortfolioEye.Application.Features.Transactions.Queries;
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

  
}

public interface IStocksManager : IManager
{
    Task<IResult<SearchTickersQuery.Response>> SearchForTickers(string query);
}
