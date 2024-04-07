using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Currencies.Queries;

public class RetrieveActiveCurrenciesQuery: IRequest<IResult<RetrieveActiveCurrenciesQuery.Response>>
{
    public record Response(IEnumerable<Currency> Currencies);

    public record Currency(string Code);
}