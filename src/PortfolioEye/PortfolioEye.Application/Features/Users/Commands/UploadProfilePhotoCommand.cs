using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Users.Commands;

public record UploadProfilePhotoCommand(string Content) : IRequest<IResult>;