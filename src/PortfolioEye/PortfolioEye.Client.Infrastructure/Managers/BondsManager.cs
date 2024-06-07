using PortfolioEye.Application.Features.Bonds.Queries;
using PortfolioEye.Application.Features.Stocks.Queries;
using PortfolioEye.Client.Infrastructure.Extensions;
using PortfolioEye.Common.Wrappers;
using BondKind = PortfolioEye.Application.Features.Transactions.Commands.BondKind;

namespace PortfolioEye.Client.Infrastructure.Managers;

public class BondsManager(IHttpClientFactory factory) : IBondsManager
{
    public async Task<IResult<RetrieveBondEmissionByBuyDateQuery.Response>> GetEmission(BondKind kind, DateOnly buyDate)
    {
        var response = await factory.MainApiClient().GetAsync($"api/bonds/search?kind={kind}&buyDate={buyDate.ToString("yyyy-MM-dd")}");
        return await response.ToResultAsync<RetrieveBondEmissionByBuyDateQuery.Response>();
    }
}

public interface IBondsManager : IManager
{
    Task<IResult<RetrieveBondEmissionByBuyDateQuery.Response>> GetEmission(BondKind kind, DateOnly buyDate);
}