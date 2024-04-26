using MediatR;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Accounts.Commands;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.Handlers.Accounts.Commands;

public class EditAccountCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<EditAccountForUserCommand, IResult>
{
    public async Task<IResult> Handle(EditAccountForUserCommand request, CancellationToken cancellationToken)
    {
        var existingAccount = await dbContext.Accounts.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

        if (existingAccount == null)
            return await Result.FailAsync(WellKnown.ErrorCodes.NotFound);

        if(existingAccount.UserId != request.UserId.ToString())
            return await Result.FailAsync(WellKnown.ErrorCodes.Unauthorized);
        
        existingAccount.Name = request.Name;
        existingAccount.Description = request.Description;
        dbContext.Accounts.Update(existingAccount);
        await dbContext.SaveChangesAsync(cancellationToken);
        return await Result.SuccessAsync();
    }
}