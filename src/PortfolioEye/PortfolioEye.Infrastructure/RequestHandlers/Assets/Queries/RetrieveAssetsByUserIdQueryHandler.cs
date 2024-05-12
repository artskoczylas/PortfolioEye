using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application.Features.Assets.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.RequestHandlers.Assets.Queries;

public class RetrieveAssetsByUserIdQueryHandler(
    ApplicationDbContext dbContext)
    : IRequestHandler<RetrieveAssetsByUserIdQuery, IResult<RetrieveAssetsByUserIdQuery.Response>>
{
    public async Task<IResult<RetrieveAssetsByUserIdQuery.Response>> Handle(
        RetrieveAssetsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var response = new RetrieveAssetsByUserIdQuery.Response(new List<RetrieveAssetsByUserIdQuery.Asset>()
        {
            new RetrieveAssetsByUserIdQuery.Asset(RetrieveAssetsByUserIdQuery.AssetType.Stock, "VWCE.DE", 123, "EUR")
        });
        return await response.ToSuccessResultAsync();
    }
}