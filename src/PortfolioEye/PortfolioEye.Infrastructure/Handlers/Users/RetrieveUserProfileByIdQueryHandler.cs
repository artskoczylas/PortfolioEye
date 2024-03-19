using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application.Features.Users;
using PortfolioEye.Domain.Entities;
using Mapster;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Infrastructure.Handlers.Users;

public class RetrieveUserProfileByIdQueryHandler(ApplicationDbContext context)
    : IRequestHandler<RetrieveUserProfileByIdQuery, IResult<UserProfileResponse>>
{
    public async Task<IResult<UserProfileResponse>> Handle(RetrieveUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == request.Id.ToString(), cancellationToken: cancellationToken);
        if (user == null)
            return await Result<UserProfileResponse>.FailAsync(404);

        var response = user.Adapt<UserProfileResponse>();
        return await Result<UserProfileResponse>.SuccessAsync(response);
    }
}