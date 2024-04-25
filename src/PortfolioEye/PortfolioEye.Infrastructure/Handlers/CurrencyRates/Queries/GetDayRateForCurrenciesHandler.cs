using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Currencies.Queries;
using PortfolioEye.Application.Features.CurrencyRates.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.Handlers.CurrencyRates.Queries;

public class GetDayRateForCurrenciesHandler(ApplicationDbContext context, IMediator mediator)
    : IRequestHandler<GetDayRateForCurrencies, IResult<GetDayRateForCurrencies.Response>>
{
    public async Task<IResult<GetDayRateForCurrencies.Response>> Handle(GetDayRateForCurrencies request,
        CancellationToken cancellationToken)
    {
        var fromCurrencyResult =
            await mediator.Send(new RetrieveCurrencyByCodeQuery(request.FromCurrency), cancellationToken);
        if (!fromCurrencyResult.IsSuccess)
            return await Result<GetDayRateForCurrencies.Response>.FailAsync(fromCurrencyResult.ErrorCode!.Value,
                "Cannot retrieve from currency");

        var toCurrencyResult =
            await mediator.Send(new RetrieveCurrencyByCodeQuery(request.ToCurrency), cancellationToken);
        if (!toCurrencyResult.IsSuccess)
            return await Result<GetDayRateForCurrencies.Response>.FailAsync(toCurrencyResult.ErrorCode!.Value,
                "Cannot retrieve to currency");

        var response = (await context.CurrencyRates
                .FirstOrDefaultAsync(x =>
                    x.FromCurrencyId == fromCurrencyResult.Data!.Id && x.ToCurrencyId == toCurrencyResult.Data!.Id &&
                    x.Date == request.Date, cancellationToken: cancellationToken)
            ).Adapt<GetDayRateForCurrencies.Response?>();

        if (response == null)
            return await Result<GetDayRateForCurrencies.Response>.FailAsync(WellKnown.ErrorCodes.NotFound);
        return await response.ToSuccessResultAsync();
    }
}