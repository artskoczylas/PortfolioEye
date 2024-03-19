using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Users;

public record RetrieveUserProfileByIdQuery(Guid Id) : IRequest<IResult<UserProfileResponse>>
{
}