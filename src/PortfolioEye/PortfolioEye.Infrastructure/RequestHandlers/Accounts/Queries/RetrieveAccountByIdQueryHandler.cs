using Mapster;
using MediatR;
using PortfolioEye.Application.Features.Accounts.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.RequestHandlers.Accounts.Queries;

public class RetrieveAccountByIdQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<RetrieveAccountByIdQuery, IResult<RetrieveAccountByIdQuery.Response>>
{
    public async Task<IResult<RetrieveAccountByIdQuery.Response>> Handle(RetrieveAccountByIdQuery request,
        CancellationToken cancellationToken)
    {
        var account = (await dbContext.Accounts
                .FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken))
                .Adapt<RetrieveAccountByIdQuery.Response>();

        return await account.ToSuccessResultAsync();
    }
}