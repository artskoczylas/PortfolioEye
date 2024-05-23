using MediatR;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Stocks.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Interfaces;

namespace PortfolioEye.Infrastructure.RequestHandlers.Stocks.Queries;

public class GetStockDetailsQueryHandler(IStockMarketDataProvider dataProvider)
    : IRequestHandler<GetStockDetailsQuery, IResult<GetStockDetailsQuery.Response>>
{
    public async Task<IResult<GetStockDetailsQuery.Response>> Handle(GetStockDetailsQuery request,
        CancellationToken cancellationToken)
    {
        var details = await dataProvider.GetDetailsAsync(request.Ticker);
        if (string.IsNullOrEmpty(details?.Currency))
            return await Result<GetStockDetailsQuery.Response>.FailAsync(WellKnown.ErrorCodes.NotFound);
        return await new GetStockDetailsQuery.Response(details.Currency).ToSuccessResultAsync();
    }
}