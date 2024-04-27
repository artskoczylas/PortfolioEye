using MediatR;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Portfolios.Commands;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.RequestHandlers.Portfolios.Commands;

public class EditPortfolioCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<EditPortfolioForUserCommand, IResult>
{
    public async Task<IResult> Handle(EditPortfolioForUserCommand request, CancellationToken cancellationToken)
    {
        var existingPortfolio = await dbContext.Portfotfolios.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

        if (existingPortfolio == null)
            return await Result.FailAsync(WellKnown.ErrorCodes.NotFound);

        if(existingPortfolio.UserId != request.UserId.ToString())
            return await Result.FailAsync(WellKnown.ErrorCodes.Unauthorized);
        
        existingPortfolio.Name = request.Name;
        existingPortfolio.Description = request.Description;
        dbContext.Portfotfolios.Update(existingPortfolio);
        await dbContext.SaveChangesAsync(cancellationToken);
        return await Result.SuccessAsync();
    }
}