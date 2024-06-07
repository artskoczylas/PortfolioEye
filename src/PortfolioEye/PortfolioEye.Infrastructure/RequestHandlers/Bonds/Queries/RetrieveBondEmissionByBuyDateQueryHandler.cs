using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Bonds.Queries;
using PortfolioEye.Application.Features.Currencies.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Domain.Entities;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.RequestHandlers.Bonds.Queries;

public class RetrieveBondEmissionByBuyDateQueryHandler(ApplicationDbContext context)
    : IRequestHandler<RetrieveBondEmissionByBuyDateQuery, IResult<RetrieveBondEmissionByBuyDateQuery.Response>>
{
    public async Task<IResult<RetrieveBondEmissionByBuyDateQuery.Response>> Handle(RetrieveBondEmissionByBuyDateQuery request, CancellationToken cancellationToken)
    {
        var dbKind = request.Kind.Adapt<BondEmissionKind>();
        var response = (await context.BondEmissions.FirstOrDefaultAsync(x => x.Kind == dbKind && x.SaleStart < request.BuyDate && x.SaleEnd > request.BuyDate, cancellationToken: cancellationToken))
            ?.Adapt<RetrieveBondEmissionByBuyDateQuery.Response>();

        if (response == null)
            return await Result<RetrieveBondEmissionByBuyDateQuery.Response>.FailAsync(WellKnown.ErrorCodes.NotFound);
        return await response.ToSuccessResultAsync();
    }
}