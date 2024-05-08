using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Stocks.Queries;

public record SearchTickersQuery(string Query) : IRequest<IResult<SearchTickersQuery.Response>>
{
    public record Response(IEnumerable<Asset> Assets);

    public record Asset(string Ticker, string Name, string Type, string Exchange);
}