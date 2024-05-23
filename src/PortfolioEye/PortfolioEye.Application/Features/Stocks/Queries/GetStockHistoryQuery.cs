using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Stocks.Queries;

public record GetStockHistoryQuery(string Ticker, DateOnly From, DateOnly To) : IRequest<IResult<GetStockHistoryQuery.Response>>
{
    public record Response(IEnumerable<StockPrice> StockPrices);

    public record StockPrice(
        DateOnly Day,
        decimal Open,
        decimal Close,
        decimal AdjustedClose,
        string Currency);
}