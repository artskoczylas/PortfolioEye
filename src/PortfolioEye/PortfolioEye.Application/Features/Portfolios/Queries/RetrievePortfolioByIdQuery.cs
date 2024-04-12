using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Portfolios.Queries;

public record RetrievePortfolioByIdQuery(Guid Id) : IRequest<IResult<RetrievePortfolioByIdQuery.Response>>
{
    public record Response(Guid Id, string Name, string Description,  string Currency, decimal Balance);
}