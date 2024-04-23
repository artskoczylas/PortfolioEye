using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.CurrencyRates.Queries;

public record GetAllRatesForCurrenciesQuery(string FromCurrency, string ToCurrency)  : IRequest<IResult<GetAllRatesForCurrenciesQuery.Response>>
{
    public record Response(string FromCurrency, string ToCurrency, List<CurrencyRate> Rates);

    public record CurrencyRate(DateOnly Date, decimal Value);
}
