using eastsoft.RCP.Shared.Wrappers;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application.Features.Users;
using PortfolioEye.Domain.Entities;
using PortfolioEye.Interfaces;

namespace PortfolioEye.Client.Infrastructure.Handlers.Users;

public class RetrieveMyUserProfileQueryHandler(ApplicationDbContext context, ICurrentUserAccessor userAccessor)
    : IRequestHandler<RetrieveMyUserProfileQuery, IResult<UserProfileResponse>>
{
    public async Task<IResult<UserProfileResponse>> Handle(RetrieveMyUserProfileQuery request, CancellationToken cancellationToken)
    {
        var currentUser = userAccessor.Get();
        if(currentUser == null)
            return await Result<UserProfileResponse>.FailAsync(401); 
        
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == currentUser.Id.ToString(), cancellationToken: cancellationToken);
        if (user == null)
            return await Result<UserProfileResponse>.FailAsync(404);

        var response = user.Adapt<UserProfileResponse>();
        return await Result<UserProfileResponse>.SuccessAsync(response);
    }
}