using Mapster;
using MediatR;
using PortfolioEye.Application.Features.Accounts.Commands;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.RequestHandlers.Accounts.Commands;

public class AddAccountForUserCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<AddAccountForUserCommand, IResult>
{
    public async Task<IResult> Handle(AddAccountForUserCommand request, CancellationToken cancellationToken)
    {
        var account = request.Adapt<Domain.Entities.Account>();
        await dbContext.Accounts.AddAsync(account, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return await Result.SuccessAsync();
    }
}