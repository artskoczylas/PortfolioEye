﻿using System.Net.Http.Json;
using PortfolioEye.Application.Features.Currencies.Commands;
using PortfolioEye.Application.Features.Currencies.Queries;
using PortfolioEye.Application.Features.Portfolios.Commands;
using PortfolioEye.Application.Features.Portfolios.Queries;
using PortfolioEye.Client.Infrastructure.Extensions;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Client.Infrastructure.Managers;

public class PortfoliosManager(IHttpClientFactory factory) : IPortfoliosManager
{
    public async Task<IResult<RetrievePortfoliosByUserId.Response>> RetrieveAllMy()
    {
        var response = await factory.MainApiClient().GetAsync($"/api/portfolios/my");
        return await response.ToResultAsync<RetrievePortfoliosByUserId.Response>();
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

    public async Task<IResult> Delete(Guid id)
    {
        var response = await factory.MainApiClient().DeleteAsync($"/api/portfolios/{id}");
        return await response.ToResultAsync();
    }
    
    public async Task<IResult<RetrievePortfolioByIdQuery.Response>> GetById(Guid id)
    {
        var response = await factory.MainApiClient().GetAsync($"/api/portfolios/{id}");
        return await response.ToResultAsync<RetrievePortfolioByIdQuery.Response>();
    }
}

public interface IPortfoliosManager : IManager
{
    Task<IResult<RetrievePortfoliosByUserId.Response>> RetrieveAllMy();
    Task<IResult> CreateNew(AddPortfolioCommand command);
    Task<IResult> Edit(EditPortfolioCommand command);
    Task<IResult> Delete(Guid id);
    Task<IResult<RetrievePortfolioByIdQuery.Response>> GetById(Guid id);
}
