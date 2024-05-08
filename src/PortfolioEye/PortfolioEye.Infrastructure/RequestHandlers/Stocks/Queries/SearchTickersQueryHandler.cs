using Mapster;
using MediatR;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Stocks.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Interfaces;

namespace PortfolioEye.Infrastructure.RequestHandlers.Stocks.Queries;

public class SearchTickersQueryHandler(IStockMarketDataProvider dataProvider)
    : IRequestHandler<SearchTickersQuery, IResult<SearchTickersQuery.Response>>
{
    public async Task<IResult<SearchTickersQuery.Response>> Handle(SearchTickersQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await dataProvider.FindTickerAsync(request.Query);
            return await new SearchTickersQuery.Response(result.Adapt<IEnumerable<SearchTickersQuery.Asset>>())
                .ToSuccessResultAsync();
        }
        catch (Exception exc)
        {
            return await Result<SearchTickersQuery.Response>.FailAsync(WellKnown.ErrorCodes.NotFound);
        }
    }
}