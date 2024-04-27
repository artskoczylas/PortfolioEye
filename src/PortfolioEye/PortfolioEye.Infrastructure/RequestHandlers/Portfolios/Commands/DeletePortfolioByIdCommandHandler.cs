using MediatR;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Portfolios.Commands;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.RequestHandlers.Portfolios.Commands;

public class DeletePortfolioByIdCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<DeletePortfolioByIdCommand, IResult>
{
    public async Task<IResult> Handle(DeletePortfolioByIdCommand request, CancellationToken cancellationToken)
    {
        var portfolioToDelete = await dbContext.Portfotfolios.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

        if (portfolioToDelete == null)
            return await Result.FailAsync(WellKnown.ErrorCodes.NotFound);
        
        dbContext.Portfotfolios.Remove(portfolioToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
        return await Result.SuccessAsync();
    }
}