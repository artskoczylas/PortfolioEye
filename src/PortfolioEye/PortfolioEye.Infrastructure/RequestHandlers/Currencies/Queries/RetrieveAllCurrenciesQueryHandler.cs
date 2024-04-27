using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application.Features.Currencies.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.RequestHandlers.Currencies.Queries;

public class RetrieveAllCurrenciesQueryHandler(ApplicationDbContext context)
    : IRequestHandler<RetrieveAllCurrenciesQuery, IResult<RetrieveAllCurrenciesQuery.Response>>
{
    public async Task<IResult<RetrieveAllCurrenciesQuery.Response>> Handle(RetrieveAllCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var currencies = await context.Currencies
            .ProjectToType<RetrieveAllCurrenciesQuery.Currency>()
            .ToListAsync(cancellationToken);

        var response = new RetrieveAllCurrenciesQuery.Response(currencies);
        return await response.ToSuccessResultAsync();
    }
}