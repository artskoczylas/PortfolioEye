using System.Net.Http.Json;
using PortfolioEye.Application.Features.Currencies.Commands;
using PortfolioEye.Application.Features.Currencies.Queries;
using PortfolioEye.Application.Features.Portfolios.Queries;
using PortfolioEye.Client.Infrastructure.Extensions;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Client.Infrastructure.Managers;

public class CurrenciesManager(IHttpClientFactory factory) : ICurrenciesManager
{
    public async Task<IResult<RetrieveAllCurrenciesQuery.Response>> RetrieveAll()
    {
        var response = await factory.MainApiClient().GetAsync($"/api/currencies/");
        return await response.ToResultAsync<RetrieveAllCurrenciesQuery.Response>();
    }

    public async Task<IResult<RetrieveActiveCurrenciesQuery.Response>> RetrieveActive()
    {
        var response = await factory.MainApiClient().GetAsync($"/api/currencies/active/");
        return await response.ToResultAsync<RetrieveActiveCurrenciesQuery.Response>();
    }

    public async Task<IResult> AddNewCurrency(string code)
    {
        var response = await factory.MainApiClient().PostAsJsonAsync($"/api/currencies/", new AddCurrencyCommand(code, true));
        return await response.ToResultAsync();
    }
}

public interface ICurrenciesManager : IManager
{
    Task<IResult<RetrieveAllCurrenciesQuery.Response>> RetrieveAll();
    Task<IResult<RetrieveActiveCurrenciesQuery.Response>> RetrieveActive();
    Task<IResult> AddNewCurrency(string code);
}
