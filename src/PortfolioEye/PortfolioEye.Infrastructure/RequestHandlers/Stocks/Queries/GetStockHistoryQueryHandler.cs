using Mapster;
using MediatR;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Stocks.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Interfaces;

namespace PortfolioEye.Infrastructure.RequestHandlers.Stocks.Queries;

public class GetStockHistoryQueryHandler(IStockMarketDataProvider dataProvider)
    : IRequestHandler<GetStockHistoryQuery, IResult<GetStockHistoryQuery.Response>>
{
    public async Task<IResult<GetStockHistoryQuery.Response>> Handle(GetStockHistoryQuery request,
        CancellationToken cancellationToken)
    {
        var history = await dataProvider.GetHistoricalDataAsync(request.Ticker, request.From, request.To);
        if (!history.Days.Any())
            return await Result<GetStockHistoryQuery.Response>.FailAsync(WellKnown.ErrorCodes.NotFound);

        var daysHistory = history.Days.Select(x => x.Adapt<GetStockHistoryQuery.StockPrice>());
        return await  new GetStockHistoryQuery.Response(daysHistory).ToSuccessResultAsync();
    }
}