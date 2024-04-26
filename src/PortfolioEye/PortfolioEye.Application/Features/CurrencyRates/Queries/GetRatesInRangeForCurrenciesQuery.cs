using System.Runtime.InteropServices.JavaScript;
using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.CurrencyRates.Queries;

public record GetRatesInRangeForCurrenciesQuery(string FromCurrency, string ToCurrency, DateOnly From, DateOnly To)  : IRequest<IResult<GetRatesInRangeForCurrenciesQuery.Response>>
{
    public record Response(string FromCurrency, string ToCurrency, DateOnly From, DateOnly To,  List<CurrencyRate> Rates);

    public record CurrencyRate(DateOnly Date, decimal Value);
}
