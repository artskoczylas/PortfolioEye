using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.StockPrices.Queries;

public record GetStockPricesInRangeQuery(string Ticker, DateOnly From, DateOnly To)
    : IRequest<IResult<GetStockPricesInRangeQuery.Response>>
{
    public record Response(
        string Ticker,
        DateOnly From,
        DateOnly To,
        List<StockPrice> Rates);

    public record StockPrice(DateOnly Date, decimal Value);
}