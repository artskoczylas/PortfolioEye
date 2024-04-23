using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Currencies.Queries;

public record RetrieveCurrencyByCodeQuery(string CurrencyCode) : IRequest<IResult<RetrieveCurrencyByCodeQuery.Response>>
{
    public record Response(int Id, string Code);
}