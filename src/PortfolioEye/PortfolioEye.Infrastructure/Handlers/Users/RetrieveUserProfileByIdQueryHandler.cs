using eastsoft.RCP.Shared.Wrappers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application.Features.Users;
using PortfolioEye.Domain.Entities;
using Mapster;

namespace PortfolioEye.Infrastructure.Handlers.Users;

public class RetrieveUserProfileByIdQueryHandler(ApplicationDbContext context)
    : IRequestHandler<RetrieveUserProfileByIdQuery, IResult<RetrieveUserProfileByIdQuery.Response>>
{
    public async Task<IResult<RetrieveUserProfileByIdQuery.Response>> Handle(RetrieveUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == request.Id.ToString(), cancellationToken: cancellationToken);
        if (user == null)
            return await Result<RetrieveUserProfileByIdQuery.Response>.FailAsync(404);

        var response = user.Adapt<RetrieveUserProfileByIdQuery.Response>();
        return await Result<RetrieveUserProfileByIdQuery.Response>.SuccessAsync(response);
    }
}