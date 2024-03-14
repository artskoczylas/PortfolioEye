﻿using eastsoft.RCP.Shared.Wrappers;
using MediatR;

namespace PortfolioEye.Application.Features.Users;

public record RetrieveUserProfileByIdQuery(Guid Id) : IRequest<IResult<UserProfileResponse>>
{
}