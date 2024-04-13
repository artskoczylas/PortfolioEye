using Mapster;
using MediatR;
using PortfolioEye.Application.Features.Accounts.Commands;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.Handlers.Accounts.Commands;

public class AddAccountForUserCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<AddAccountForUserCommand, IResult>
{
    public async Task<IResult> Handle(AddAccountForUserCommand request, CancellationToken cancellationToken)
    {
        var account = request.Adapt<Domain.Entities.Potfolio>();
        await dbContext.Portfotfolios.AddAsync(account, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return await Result.SuccessAsync();
    }
}