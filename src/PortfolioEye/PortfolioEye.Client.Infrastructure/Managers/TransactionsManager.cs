using System.Net.Http.Json;
using PortfolioEye.Application.Features.Transactions.Commands;
using PortfolioEye.Application.Features.Transactions.Queries;
using PortfolioEye.Client.Infrastructure.Extensions;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Client.Infrastructure.Managers;

public class TransactionsManager(IHttpClientFactory factory) : ITransactionsManager
{
    public async Task<IResult<RetrieveTransactionsByUserIdQuery.Response>> RetrieveAllMy()
    {
        var response = await factory.MainApiClient().GetAsync($"/api/transactions/my");
        return await response.ToResultAsync<RetrieveTransactionsByUserIdQuery.Response>();
    }

    public async Task<IResult> CreateNew(AddTransactionCommand command)
    {
        var response = await factory.MainApiClient().PostAsJsonAsync($"/api/transactions/my", command);
        return await response.ToResultAsync();
    }
    
    public async Task<IResult> CreateNewStock(AddStockTransactionCommand command)
    {
        var response = await factory.MainApiClient().PostAsJsonAsync($"/api/transactions/my/Stocks", command);
        return await response.ToResultAsync();
    }

    public async Task<IResult> Edit(EditTransactionCommand command)
    {
        var response = await factory.MainApiClient().PutAsJsonAsync($"/api/transactions/my", command);
        return await response.ToResultAsync();
    }

    public async Task<IResult> Delete(Guid id)
    {
        var response = await factory.MainApiClient().DeleteAsync($"/api/transactions/{id}");
        return await response.ToResultAsync();
    }
    
    public async Task<IResult<RetrieveTransactionByIdQuery.Response>> GetById(Guid id)
    {
        var response = await factory.MainApiClient().GetAsync($"/api/transactions/{id}");
        return await response.ToResultAsync<RetrieveTransactionByIdQuery.Response>();
    }
}

public interface ITransactionsManager : IManager
{
    Task<IResult<RetrieveTransactionsByUserIdQuery.Response>> RetrieveAllMy();
    Task<IResult> CreateNew(AddTransactionCommand command);
    Task<IResult> Edit(EditTransactionCommand command);
    Task<IResult> Delete(Guid id);
    Task<IResult<RetrieveTransactionByIdQuery.Response>> GetById(Guid id);
    Task<IResult> CreateNewStock(AddStockTransactionCommand command);
}
