using eastsoft.RCP.Shared.Wrappers;
using MediatR;

namespace PortfolioEye.Application.Features.Users;

public class RetrieveMyUserProfileQuery: IRequest<IResult<UserProfileResponse>>;