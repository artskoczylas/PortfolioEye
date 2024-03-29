﻿using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Users.Commands;

public record DeleteProfilePhotoByUserIdCommand(Guid UserId) : IRequest<IResult>;