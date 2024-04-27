using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application.Features.Currencies.Queries;
using PortfolioEye.Application.Features.CurrencyRates.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.RequestHandlers.CurrencyRates.Queries;

public class GetAllRatesForCurrenciesQueryHandler(ApplicationDbContext context, IMediator mediator)
    : IRequestHandler<GetAllRatesForCurrenciesQuery, IResult<GetAllRatesForCurrenciesQuery.Response>>
{
    public async Task<IResult<GetAllRatesForCurrenciesQuery.Response>> Handle(GetAllRatesForCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var fromCurrencyResult = await mediator.Send(new RetrieveCurrencyByCodeQuery(request.FromCurrency), cancellationToken);
        if (!fromCurrencyResult.IsSuccess)
            return await Result<GetAllRatesForCurrenciesQuery.Response>.FailAsync(fromCurrencyResult.ErrorCode!.Value, "Cannot retrieve from currency");

        var toCurrencyResult = await mediator.Send(new RetrieveCurrencyByCodeQuery(request.ToCurrency), cancellationToken);
        if (!toCurrencyResult.IsSuccess)
            return await Result<GetAllRatesForCurrenciesQuery.Response>.FailAsync(toCurrencyResult.ErrorCode!.Value, "Cannot retrieve to currency");
        
        var rates = await context.CurrencyRates
            .Where(x => x.FromCurrencyId == fromCurrencyResult.Data!.Id && x.ToCurrencyId == toCurrencyResult.Data!.Id)
            .ProjectToType<GetAllRatesForCurrenciesQuery.CurrencyRate>()
            .ToListAsync(cancellationToken);

        var response = new GetAllRatesForCurrenciesQuery.Response(request.FromCurrency, request.ToCurrency, rates);
        return await response.ToSuccessResultAsync();
    }
}