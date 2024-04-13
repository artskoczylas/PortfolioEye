﻿using MediatR;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Accounts.Commands;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.Handlers.Accounts.Commands;

public class DeleteAccountByIdCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<DeleteAccountByIdCommand, IResult>
{
    public async Task<IResult> Handle(DeleteAccountByIdCommand request, CancellationToken cancellationToken)
    {
        var accountToDelete = await dbContext.Portfotfolios.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken);

        if (accountToDelete == null)
            return await Result.FailAsync(WellKnown.ErrorCodes.NotFound);
        
        dbContext.Portfotfolios.Remove(accountToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
        return await Result.SuccessAsync();
    }
}