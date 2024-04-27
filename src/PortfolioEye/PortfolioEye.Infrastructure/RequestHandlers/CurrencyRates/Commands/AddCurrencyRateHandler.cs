using MediatR;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Currencies.Queries;
using PortfolioEye.Application.Features.CurrencyRates.Commands;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Domain.Entities;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.RequestHandlers.CurrencyRates.Commands;

public class AddCurrencyRateHandler(ApplicationDbContext context, IMediator mediator)
    : IRequestHandler<AddCurrencyRate, IResult>
{
    public async Task<IResult> Handle(AddCurrencyRate request, CancellationToken cancellationToken)
    {
        var fromCurrencyResult = await mediator.Send(new RetrieveCurrencyByCodeQuery(request.FromCurrency), cancellationToken);
        if (!fromCurrencyResult.IsSuccess)
            return await Result.FailAsync(fromCurrencyResult.ErrorCode!.Value, "Cannot retrieve from currency");

        var toCurrencyResult = await mediator.Send(new RetrieveCurrencyByCodeQuery(request.ToCurrency), cancellationToken);
        if (!toCurrencyResult.IsSuccess)
            return await Result.FailAsync(toCurrencyResult.ErrorCode!.Value, "Cannot retrieve to currency");

        var rate = context.CurrencyRates.FirstOrDefault(x =>
            x.FromCurrencyId == fromCurrencyResult.Data!.Id && x.ToCurrencyId == toCurrencyResult.Data!.Id &&
            x.Date == request.Date);
        if (rate != null)
            return await Result.FailAsync(WellKnown.ErrorCodes.Conflict);

        rate = new CurrencyRate()
        {
            FromCurrencyId = fromCurrencyResult.Data!.Id,
            ToCurrencyId = toCurrencyResult.Data!.Id,
            Date = request.Date,
            RateValue = request.Value
        };
        await context.AddAsync(rate, cancellationToken);
        await context.SaveChangesAsync(cancellationToken: cancellationToken);
        return await Result.SuccessAsync();
    }
}