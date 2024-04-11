using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Portfolios.Queries;

public record RetrievePortfolioById(Guid Id) : IRequest<IResult<RetrievePortfolioById.Response>>
{
    public record Response(Guid Id, string Name, string Description,  string Currency, decimal Balance);
}