using eastsoft.RCP.Shared.Wrappers;
using MediatR;
using PortfolioEye.Application.Features.Users;
using PortfolioEye.Client.Infrastructure.Extensions;

namespace PortfolioEye.Client.Infrastructure.Handlers.Users;

public class RetrieveUserProfileByIdQueryHandler(HttpClient httpClient)
    : IRequestHandler<RetrieveUserProfileByIdQuery, IResult<UserProfileResponse>>
{
    public async Task<IResult<UserProfileResponse>> Handle(RetrieveUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await httpClient.GetAsync($"/api/users/{request.Id}", cancellationToken);
        return await response.ToResult<UserProfileResponse>();
    }
}