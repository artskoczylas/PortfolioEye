using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application.Features.Portfolios.Queries;
using PortfolioEye.Application.Features.Transactions.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;
using PortfolioEye.Infrastructure.Services;

namespace PortfolioEye.Infrastructure.Handlers.Transactions.Queries;

public class RetrieveTransactionsByUserIdQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<RetrieveTransactionsByUserIdQuery, IResult<RetrieveTransactionsByUserIdQuery.Response>>
{
    public async Task<IResult<RetrieveTransactionsByUserIdQuery.Response>> Handle(RetrieveTransactionsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var transactions = await dbContext.Transactions
            .Where(c => c.UserId == request.UserId.ToString())
            .OrderByDescending(t => t.TransactionDate)
            .ProjectToType<RetrieveTransactionsByUserIdQuery.Transaction>()
            .ToListAsync(cancellationToken);

        var response = new RetrieveTransactionsByUserIdQuery.Response(transactions);
        return await response.ToSuccessResultAsync();
    }
}