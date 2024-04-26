using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application.Features.Accounts.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.Handlers.Accounts.Queries;

public class RetrieveAccountsByUserIdHandler(ApplicationDbContext dbContext)
    : IRequestHandler<RetrieveAccountsByUserId, IResult<RetrieveAccountsByUserId.Response>>
{
    public async Task<IResult<RetrieveAccountsByUserId.Response>> Handle(RetrieveAccountsByUserId request,
        CancellationToken cancellationToken)
    {
        var accounts = await dbContext.Accounts
            .Where(a => a.UserId == request.UserId.ToString())
            .ProjectToType<RetrieveAccountsByUserId.Account>()
            .ToListAsync(cancellationToken);

        var response = new RetrieveAccountsByUserId.Response(accounts);
        return await response.ToSuccessResultAsync();
    }
}