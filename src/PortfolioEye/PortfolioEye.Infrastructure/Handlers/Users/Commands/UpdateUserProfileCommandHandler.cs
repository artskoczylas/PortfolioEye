﻿using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Users;
using PortfolioEye.Application.Features.Users.Commands;
using PortfolioEye.Application.Features.Users.Queries;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Domain.Entities;

namespace PortfolioEye.Infrastructure.Handlers.Users.Commands;

public class UpdateUserProfileCommandHandler(ApplicationDbContext context)
    : IRequestHandler<UpdateUserProfileByIdCommand, IResult>
{
    public async Task<IResult> Handle(UpdateUserProfileByIdCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == request.Id.ToString(), cancellationToken);
        if (user == null)
            return await Result.FailAsync(WellKnown.ErrorCodes.NotFound);

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        var result = context.Update(user);
        await context.SaveChangesAsync(cancellationToken);
        return await Result.SuccessAsync();
    }
}