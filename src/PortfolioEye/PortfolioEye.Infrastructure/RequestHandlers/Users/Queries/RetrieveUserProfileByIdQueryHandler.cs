using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Users.Queries;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.RequestHandlers.Users.Queries;

public class RetrieveUserProfileByIdQueryHandler(ApplicationDbContext context)
    : IRequestHandler<RetrieveUserProfileByIdQuery, IResult<UserProfileResponse>>
{
    public async Task<IResult<UserProfileResponse>> Handle(RetrieveUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (user == null)
            return await Result<UserProfileResponse>.FailAsync(WellKnown.ErrorCodes.NotFound);

        var response = user.Adapt<UserProfileResponse>();
        return await Result<UserProfileResponse>.SuccessAsync(response);
    }
}