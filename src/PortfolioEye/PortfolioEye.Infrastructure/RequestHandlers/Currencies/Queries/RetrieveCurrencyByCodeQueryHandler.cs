using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Currencies.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.Handlers.Currencies.Queries;

public class RetrieveCurrencyByCodeQueryHandler(ApplicationDbContext context)
    : IRequestHandler<RetrieveCurrencyByCodeQuery, IResult<RetrieveCurrencyByCodeQuery.Response>>
{
    public async Task<IResult<RetrieveCurrencyByCodeQuery.Response>> Handle(RetrieveCurrencyByCodeQuery request, CancellationToken cancellationToken)
    {
        var response = (await context.Currencies.FirstOrDefaultAsync(x => x.Code == request.CurrencyCode, cancellationToken: cancellationToken))
            ?.Adapt<RetrieveCurrencyByCodeQuery.Response>();

        if (response == null)
            return await Result<RetrieveCurrencyByCodeQuery.Response>.FailAsync(WellKnown.ErrorCodes.NotFound);
        return await response.ToSuccessResultAsync();
    }
}