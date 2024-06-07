using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Bonds.Queries;

public enum BondKind
{
    EDO,
    ROD
}

public record RetrieveBondEmissionByBuyDateQuery(BondKind Kind, DateOnly BuyDate)
    : IRequest<IResult<RetrieveBondEmissionByBuyDateQuery.Response>>
{
    public record Response(Guid Id, string Emission, decimal FirstYearInterestRate, decimal NextYearsInterestMargin);
}