using System.Reflection.Metadata.Ecma335;
using MediatR;
using PortfolioEye.Application.Features.Users.Commands;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Domain.Entities;

namespace PortfolioEye.Infrastructure.Handlers.Users.Commands;

public class UploadProfilePhotoByUserIdCommandHandler
    : IRequestHandler<UploadProfilePhotoByUserIdCommand, IResult>
{
    public async Task<IResult> Handle(UploadProfilePhotoByUserIdCommand request, CancellationToken cancellationToken)
    {
        var photosDirectory = new DirectoryInfo("Data/ProfilePhotos");
        if (!photosDirectory.Exists)
            photosDirectory.Create();

        var fi = new FileInfo(Path.Combine(photosDirectory.ToString(), request.UserId.ToString()));
        await using (var writer = fi.OpenWrite())
        {
            var content = Convert.FromBase64String(request.Content);
            await writer.WriteAsync(content, cancellationToken);
            await writer.FlushAsync(cancellationToken);
        }
        return await Result.SuccessAsync();
    }
}