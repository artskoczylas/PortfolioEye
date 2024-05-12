using PortfolioEye.Application.Features.Assets.Queries;
using PortfolioEye.Client.Infrastructure.Extensions;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Client.Infrastructure.Managers;

public class AssetsManager(IHttpClientFactory factory) : IAssetsManager
{
    public async Task<IResult<RetrieveAssetsByUserIdQuery.Response>> RetrieveAllMy()
    {
        var response = await factory.MainApiClient().GetAsync($"/api/assets/my");
        return await response.ToResultAsync<RetrieveAssetsByUserIdQuery.Response>();
    }
}

public interface IAssetsManager: IManager
{
    Task<IResult<RetrieveAssetsByUserIdQuery.Response>> RetrieveAllMy();
}