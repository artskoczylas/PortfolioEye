﻿using MediatR;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Users.Commands;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;
using PortfolioEye.Services;
using SkiaSharp;

namespace PortfolioEye.Infrastructure.RequestHandlers.Users.Commands;

public class UploadProfilePhotoByUserIdCommandHandler(
    ApplicationDbContext dbContext,
    IHostingInformationProvider hostingInformationProvider)
    : IRequestHandler<UploadProfilePhotoByUserIdCommand, IResult>
{
    public async Task<IResult> Handle(UploadProfilePhotoByUserIdCommand request, CancellationToken cancellationToken)
    {
        SKImage image = SKImage.FromEncodedData(Convert.FromBase64String(request.Content));
        SKBitmap bitmap = SKBitmap.FromImage(image);
        bitmap = CropImageIfNeeded(bitmap);
        bitmap = ResizeImageIfNeeded(bitmap);
        var png = bitmap.Encode(SKEncodedImageFormat.Png, 95);
        
        await using (var filestream = File.OpenWrite(Path.Combine(
                         hostingInformationProvider.ProfilePhotosDirectory.ToString(),
                         request.UserId.ToString() + ".png")))
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

    private SKBitmap CropImageIfNeeded(SKBitmap bitmap)
    {
        if (bitmap.Height == bitmap.Width)
            return bitmap;
        using var pixmap = new SKPixmap(bitmap.Info, bitmap.GetPixels());
        int left = 0;
        int top = 0;
        int right = 0;
        int bottom = 0;
        var horizontal = bitmap.Width > bitmap.Height;
        if (horizontal)
        {
            top = 0;
            bottom = bitmap.Height;
            left = (bitmap.Width - bitmap.Height) / 2;
            right = left + bitmap.Height;
        }
        else
        {
            top = (bitmap.Height - bitmap.Width) / 2;
            bottom = top + bitmap.Width;
            left = 0;
            right = bitmap.Width;
        }

        var rectI = new SkiaSharp.SKRectI(left, top, right, bottom);
        var subset = pixmap.ExtractSubset(rectI);
        return SKBitmap.FromImage(SKImage.FromPixels(subset));
    }

    private SKBitmap ResizeImageIfNeeded(SKBitmap source)
    {
        if (source.Width < 400)
            return source;
        return source.Resize(new SKImageInfo(400, 400), SKFilterQuality.High);
    }
}