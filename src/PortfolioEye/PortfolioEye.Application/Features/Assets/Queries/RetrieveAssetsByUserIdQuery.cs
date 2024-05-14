using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Assets.Queries;

public record RetrieveAssetsByUserIdQuery(Guid UserId) : IRequest<IResult<RetrieveAssetsByUserIdQuery.Response>>
{
    public record Response(List<Asset> Assets);

    public record Asset(AssetType Type, string Ticker, decimal Value, string Currency);

    public enum AssetType
    {
        Stocks = 0,
        Bonds = 1
    }
}