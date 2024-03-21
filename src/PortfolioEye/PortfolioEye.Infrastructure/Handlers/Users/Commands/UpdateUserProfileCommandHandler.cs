using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            return await Result.FailAsync(404);

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhotoUri = request.PhotoUrl;

        context.Update(user);
        return await Result.SuccessAsync();
    }
}