using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Users.Commands;

public record UpdateUserProfileByIdCommand(Guid Id, UpdateProfileCommand Profile)
    : UpdateProfileCommand(Profile), IRequest<IResult>;