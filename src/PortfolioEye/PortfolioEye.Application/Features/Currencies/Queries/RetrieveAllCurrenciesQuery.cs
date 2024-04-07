using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Currencies.Queries;

public record RetrieveAllCurrenciesQuery() : IRequest<IResult<RetrieveAllCurrenciesQuery.Response>>
{
    public record Response(IEnumerable<Currency> Currencies);
    public record Currency(string Code, bool IsActive);
}