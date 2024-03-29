using MediatR;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Users.Commands;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Domain.Entities;
using SkiaSharp;

namespace PortfolioEye.Infrastructure.Handlers.Users.Commands;

public class UploadProfilePhotoByUserIdCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<UploadProfilePhotoByUserIdCommand, IResult>
{
    public async Task<IResult> Handle(UploadProfilePhotoByUserIdCommand request, CancellationToken cancellationToken)
    {
        SKBitmap bitmap = SKBitmap.Decode(request.Content);
        SKImage image = SKImage.FromEncodedData(Convert.FromBase64String(request.Content));
        SKData png = image.Encode(SKEncodedImageFormat.Png, 95);

        var photosDirectory = new DirectoryInfo("Data/ProfilePhotos");
        if (!photosDirectory.Exists)
            photosDirectory.Create();

        await using (var filestream =
                     File.OpenWrite(Path.Combine(photosDirectory.ToString(), request.UserId.ToString() + ".png")))
        {
            png.SaveTo(filestream);
        }

        var user = await dbContext.Users.FindAsync(request.UserId.ToString());
        if (user == null)
            return await Result.FailAsync(WellKnown.ErrorCodes.NotFound);

        user.PhotoUri = $"/api/Users/{request.UserId}/Photo";
        dbContext.Update(user);
        await dbContext.SaveChangesAsync(cancellationToken);
        return await Result.SuccessAsync();
    }
}