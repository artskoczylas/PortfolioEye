using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application.Features.Users;
using PortfolioEye.Application.Features.Users.Queries;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Domain.Entities;

namespace PortfolioEye.Infrastructure.Handlers.Users.Queries;

public class RetrieveUserProfileByIdQueryHandler(ApplicationDbContext context)
    : IRequestHandler<RetrieveUserProfileByIdQuery, IResult<UserProfileResponse>>
{
    public async Task<IResult<UserProfileResponse>> Handle(RetrieveUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == request.Id.ToString(), cancellationToken);
        if (user == null)
            return await Result<UserProfileResponse>.FailAsync(404);

        var response = user.Adapt<UserProfileResponse>();
        return await Result<UserProfileResponse>.SuccessAsync(response);
    }
}