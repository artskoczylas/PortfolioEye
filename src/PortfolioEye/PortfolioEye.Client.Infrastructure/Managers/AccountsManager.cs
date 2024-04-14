using System.Net.Http.Json;
using PortfolioEye.Application.Features.Accounts.Commands;
using PortfolioEye.Application.Features.Accounts.Queries;
using PortfolioEye.Client.Infrastructure.Extensions;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Client.Infrastructure.Managers;

public class AccountsManager(IHttpClientFactory factory) : IAccountsManager
{
    public async Task<IResult<RetrieveAccountsByUserId.Response>> RetrieveAllMy()
    {
        var response = await factory.MainApiClient().GetAsync($"/api/accounts/my");
        return await response.ToResultAsync<RetrieveAccountsByUserId.Response>();
    }

    public async Task<IResult> CreateNew(AddAccountCommand command)
    {
        var response = await factory.MainApiClient().PostAsJsonAsync($"/api/accounts/my", command);
        return await response.ToResultAsync();
    }

    public async Task<IResult> Edit(EditAccountCommand command)
    {
        var response = await factory.MainApiClient().PutAsJsonAsync($"/api/accounts/my", command);
        return await response.ToResultAsync();
    }

    public async Task<IResult> Delete(Guid id)
    {
        var response = await factory.MainApiClient().DeleteAsync($"/api/accounts/{id}");
        return await response.ToResultAsync();
    }
    
    public async Task<IResult<RetrieveAccountByIdQuery.Response>> GetById(Guid id)
    {
        var response = await factory.MainApiClient().GetAsync($"/api/accounts/{id}");
        return await response.ToResultAsync<RetrieveAccountByIdQuery.Response>();
    }
}

public interface IAccountsManager : IManager
{
    Task<IResult<RetrieveAccountsByUserId.Response>> RetrieveAllMy();
    Task<IResult> CreateNew(AddAccountCommand command);
    Task<IResult> Edit(EditAccountCommand command);
    Task<IResult> Delete(Guid id);
    Task<IResult<RetrieveAccountByIdQuery.Response>> GetById(Guid id);
}
