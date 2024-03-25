using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Users.Commands;

public record UploadProfilePhotoByUserIdCommand(Guid UserId, UploadProfilePhotoCommand PhotoCommand)
    : UploadProfilePhotoCommand(PhotoCommand);