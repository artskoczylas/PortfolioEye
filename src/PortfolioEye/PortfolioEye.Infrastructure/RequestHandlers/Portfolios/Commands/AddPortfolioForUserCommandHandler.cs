using Mapster;
using MediatR;
using PortfolioEye.Application.Features.Portfolios.Commands;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.RequestHandlers.Portfolios.Commands;

public class AddPortfolioForUserCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<AddPortfolioForUserCommand, IResult>
{
    public async Task<IResult> Handle(AddPortfolioForUserCommand request, CancellationToken cancellationToken)
    {
        var portfolio = request.Adapt<Domain.Entities.Potfolio>();
        await dbContext.Portfotfolios.AddAsync(portfolio, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return await Result.SuccessAsync();
    }
}