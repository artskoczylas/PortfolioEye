using MediatR;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Users.Commands;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.RequestHandlers.Users.Commands;

public class DeleteProfilePhotoByUserIdCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<DeleteProfilePhotoByUserIdCommand, IResult>
{
    public async Task<IResult> Handle(DeleteProfilePhotoByUserIdCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FindAsync(request.UserId.ToString());
        if (user == null)
            return await Result.FailAsync(WellKnown.ErrorCodes.NotFound);
        
        var photosDirectory = new DirectoryInfo("Data/ProfilePhotos");
        if (photosDirectory.Exists)
        {
            var fi = new FileInfo(Path.Combine(photosDirectory.ToString(), request.UserId.ToString()));
            if(fi.Exists)
                fi.Delete();
        }

        user.PhotoUri = null;
        dbContext.Update(user);
        await dbContext.SaveChangesAsync(cancellationToken);
        return await Result.SuccessAsync();
    }
}