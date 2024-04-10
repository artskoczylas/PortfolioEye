using System.Net.Http.Json;
using PortfolioEye.Application.Features.Currencies.Commands;
using PortfolioEye.Application.Features.Currencies.Queries;
using PortfolioEye.Application.Features.Portfolios.Commands;
using PortfolioEye.Application.Features.Portfolios.Queries;
using PortfolioEye.Client.Infrastructure.Extensions;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Client.Infrastructure.Managers;

public class PortfoliosManager(IHttpClientFactory factory) : IPortfoliosManager
{
    public async Task<IResult<IEnumerable<RetrievePortfoliosByUserId.Response>>> RetrieveAllMy()
    {
        var response = await factory.MainApiClient().GetAsync($"/api/portfolios/my");
        return await response.ToResultAsync<IEnumerable<RetrievePortfoliosByUserId.Response>>();
    }

    public async Task<IResult> CreateNew(AddPortfolioCommand command)
    {
        var response = await factory.MainApiClient().PostAsJsonAsync($"/api/portfolios/my", command);
        return await response.ToResultAsync();
    }

    public async Task<IResult> Edit(EditPortfolioCommand command)
    {
        var response = await factory.MainApiClient().PutAsJsonAsync($"/api/portfolios/my", command);
        return await response.ToResultAsync();
    }
}

public interface IPortfoliosManager : IManager
{
    Task<IResult<IEnumerable<RetrievePortfoliosByUserId.Response>>> RetrieveAllMy();
    Task<IResult> CreateNew(AddPortfolioCommand command);
    Task<IResult> Edit(EditPortfolioCommand command);
}
