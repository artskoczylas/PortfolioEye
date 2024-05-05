using MediatR;
using PortfolioEye.Application.Features.StockPrices.Commands;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.StockPrices.Queries;

public record GetCurrentStockPriceQuery(string Ticker) : IRequest<IResult<GetCurrentStockPriceQuery.Response>>
{
    public record Response(string Ticker, DateOnly Date, decimal Value, StockPriceSource Source);
}