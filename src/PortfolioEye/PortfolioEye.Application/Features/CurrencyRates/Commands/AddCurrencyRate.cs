using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.CurrencyRates.Commands;

public record AddCurrencyRate(string FromCurrency, string ToCurrency, DateOnly Date, decimal Value)  : IRequest<IResult>;
