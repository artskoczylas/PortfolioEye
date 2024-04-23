using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.CurrencyRates.Queries;

public record GetDayRateForCurrencies(string FromCurrency, string ToCurrency, DateOnly Date) : IRequest<IResult<GetDayRateForCurrencies.Response>>
{
    public record Response(string FromCurrency, string ToCurrency, DateOnly Date, decimal Value);
}