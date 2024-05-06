using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application.Features.StockPrices.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.RequestHandlers.StockPrices.Queries;

public class GetStockPricesInRangeQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetStockPricesInRangeQuery, IResult<GetStockPricesInRangeQuery.Response>>
{
    public async Task<IResult<GetStockPricesInRangeQuery.Response>> Handle(GetStockPricesInRangeQuery request,
        CancellationToken cancellationToken)
    {
        var stockPrice = await dbContext.StockPrices
            .Where(x => x.Ticker.Equals(request.Ticker, StringComparison.CurrentCultureIgnoreCase))
            .ProjectToType<GetStockPricesInRangeQuery.StockPrice>()
            .ToListAsync(cancellationToken);

        return await new GetStockPricesInRangeQuery.Response(request.Ticker, request.From, request.To, stockPrice)
            .ToSuccessResultAsync();
    }
}