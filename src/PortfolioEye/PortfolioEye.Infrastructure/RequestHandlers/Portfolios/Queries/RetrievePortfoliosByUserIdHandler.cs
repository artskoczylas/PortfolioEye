using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application.Features.Currencies.Queries;
using PortfolioEye.Application.Features.Portfolios.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.Handlers.Portfolios.Queries;

public class RetrievePortfoliosByUserIdHandler(ApplicationDbContext dbContext)
    : IRequestHandler<RetrievePortfoliosByUserId, IResult<RetrievePortfoliosByUserId.Response>>
{
    public async Task<IResult<RetrievePortfoliosByUserId.Response>> Handle(RetrievePortfoliosByUserId request,
        CancellationToken cancellationToken)
    {
        var portfolios = await dbContext.Portfotfolios
            .Where(c => c.UserId == request.UserId.ToString())
            .ProjectToType<RetrievePortfoliosByUserId.Portfolio>()
            .ToListAsync(cancellationToken);

        var response = new RetrievePortfoliosByUserId.Response(portfolios);
        return await response.ToSuccessResultAsync();
    }
}