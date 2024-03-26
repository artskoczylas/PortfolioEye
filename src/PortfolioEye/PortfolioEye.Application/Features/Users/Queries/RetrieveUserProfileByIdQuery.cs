using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Users.Queries;

public record RetrieveUserProfileByIdQuery(Guid Id) : IRequest<IResult<UserProfileResponse>>;
public record UserProfileResponse(string FirstName, string LastName, string Email, string? PhotoUri);