using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.StockPrices.Commands;

public enum StockPriceSource
{
    Yahoo = 1
}
public record AddStockPriceCommand(StockPriceSource Source, string Ticker, DateOnly Date, decimal Price) : IRequest<IResult>;