using Mapster;
using MediatR;
using PortfolioEye.Application.Features.Portfolios.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.Handlers.Portfolios.Queries;

public class RetrievePortfolioByIdQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<RetrievePortfolioByIdQuery, IResult<RetrievePortfolioByIdQuery.Response>>
{
    public async Task<IResult<RetrievePortfolioByIdQuery.Response>> Handle(RetrievePortfolioByIdQuery request,
        CancellationToken cancellationToken)
    {
        var portfolio = (await dbContext.Portfotfolios
                .FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken))
                .Adapt<RetrievePortfolioByIdQuery.Response>();

        return await portfolio.ToSuccessResultAsync();
    }
}