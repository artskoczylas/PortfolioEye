using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Users;

public class RetrieveMyUserProfileQuery: IRequest<IResult<UserProfileResponse>>;