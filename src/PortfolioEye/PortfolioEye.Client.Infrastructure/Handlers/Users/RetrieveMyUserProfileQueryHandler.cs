using eastsoft.RCP.Shared.Wrappers;
using MediatR;
using PortfolioEye.Application.Features.Users;
using PortfolioEye.Client.Infrastructure.Extensions;

namespace PortfolioEye.Client.Infrastructure.Handlers.Users;

public class RetrieveMyUserProfileQueryHandler(HttpClient httpClient)
    : IRequestHandler<RetrieveMyUserProfileQuery, IResult<UserProfileResponse>>
{
    public async Task<IResult<UserProfileResponse>> Handle(RetrieveMyUserProfileQuery request, CancellationToken cancellationToken)
    {
        var response = await httpClient.GetAsync($"/api/Me/Profile", cancellationToken);
        return await response.ToResult<UserProfileResponse>();
    }
}