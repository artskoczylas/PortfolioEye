using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application.Features.Currencies.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.RequestHandlers.Currencies.Queries;

public class RetrieveActiveCurrenciesQueryHandler(ApplicationDbContext context)
    : IRequestHandler<RetrieveActiveCurrenciesQuery, IResult<RetrieveActiveCurrenciesQuery.Response>>
{
    public async Task<IResult<RetrieveActiveCurrenciesQuery.Response>> Handle(RetrieveActiveCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var activeCurrencies = await context.Currencies
            .Where(c => c.IsActive)
            .ProjectToType<RetrieveActiveCurrenciesQuery.Currency>()
            .ToListAsync(cancellationToken);

        var response = new RetrieveActiveCurrenciesQuery.Response(activeCurrencies);
        return await response.ToSuccessResultAsync();
    }
}