using System.Diagnostics;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application.Features.Transactions.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;
using PortfolioEye.Infrastructure.Interfaces;

namespace PortfolioEye.Infrastructure.RequestHandlers.Transactions.Queries;

public class RetrieveTransactionsByUserIdQueryHandler(
    ApplicationDbContext dbContext)
    : IRequestHandler<RetrieveTransactionsByUserIdQuery, IResult<RetrieveTransactionsByUserIdQuery.Response>>
{
    public async Task<IResult<RetrieveTransactionsByUserIdQuery.Response>> Handle(
        RetrieveTransactionsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var transactions = await dbContext.Transactions
            .Include(x => x.Portfolio)
            .Include(x => x.Account)
            .Where(c => c.UserId == request.UserId.ToString())
            .OrderByDescending(t => t.TransactionDate)
            .ProjectToType<RetrieveTransactionsByUserIdQuery.Transaction>(new TypeAdapterConfig(){RuleMap = {  }})
            .ToListAsync(cancellationToken);

        var response = new RetrieveTransactionsByUserIdQuery.Response(transactions);
        return await response.ToSuccessResultAsync();
    }
}