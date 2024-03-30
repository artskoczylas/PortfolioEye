﻿using MediatR;
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
        SKImage image = SKImage.FromEncodedData(Convert.FromBase64String(request.Content));

        var png = CropImage(image);

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

    private SKData CropImage(SKImage image)
    {
        SKBitmap bitmap = SKBitmap.FromImage(image);

        using var pixmap =  new SKPixmap(bitmap.Info, bitmap.GetPixels());
        int left = 0;
        int top = 0;
        int right = 0;
        int bottom = 0;
        var horizontal = bitmap.Width > bitmap.Height;
        if (horizontal)
        {
            top = 0;
            bottom = bitmap.Height;
            left = (bitmap.Height - bitmap.Width) / 2;
            right = left + bitmap.Width;
        }
        else
        {
            top = (bitmap.Width - bitmap.Height) / 2;
            bottom = top + bitmap.Height;
            left = 0;
            right = bitmap.Width;
        }
        SkiaSharp.SKRectI rectI = new SkiaSharp.SKRectI(0, 0, 105, 105);
        var subset = pixmap.ExtractSubset(rectI);
        SKData png = subset.Encode(SKEncodedImageFormat.Png, 95);
        return png;
    }
}