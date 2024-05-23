using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Stocks.Queries;

public record GetStockDetailsQuery(string Ticker) : IRequest<IResult<GetStockDetailsQuery.Response>>
{
    public record Response(string Currency);
}