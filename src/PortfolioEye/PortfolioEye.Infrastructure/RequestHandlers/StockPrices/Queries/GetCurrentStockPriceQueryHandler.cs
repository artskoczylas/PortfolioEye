using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.StockPrices.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.RequestHandlers.StockPrices.Queries;

public class GetCurrentStockPriceQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetCurrentStockPriceQuery, IResult<GetCurrentStockPriceQuery.Response>>
{
    public async Task<IResult<GetCurrentStockPriceQuery.Response>> Handle(GetCurrentStockPriceQuery request,
        CancellationToken cancellationToken)
    {
        var stockPrice = await dbContext.StockPrices
            .Where(x => x.Ticker.Equals(request.Ticker, StringComparison.CurrentCultureIgnoreCase))
            .OrderByDescending(x => x.Date).FirstOrDefaultAsync(cancellationToken);

        if (stockPrice == null)
            return await Result<GetCurrentStockPriceQuery.Response>.FailAsync(WellKnown.ErrorCodes.NotFound);
        return await stockPrice.Adapt<GetCurrentStockPriceQuery.Response>().ToSuccessResultAsync();
    }
}