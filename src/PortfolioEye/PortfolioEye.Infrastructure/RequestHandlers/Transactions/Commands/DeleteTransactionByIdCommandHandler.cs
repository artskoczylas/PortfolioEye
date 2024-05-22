using System.Runtime.InteropServices.JavaScript;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Transactions.Commands;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;
using TransactionType = PortfolioEye.Domain.Entities.TransactionType;

namespace PortfolioEye.Infrastructure.RequestHandlers.Transactions.Commands;

public class DeleteTransactionByIdCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<DeleteTransactionByIdCommand, IResult>
{
    public async Task<IResult> Handle(DeleteTransactionByIdCommand request, CancellationToken cancellationToken)
    {
        var transaction = await dbContext.Transactions.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (transaction == null)
            return await Result.FailAsync(WellKnown.ErrorCodes.NotFound);

        if (transaction.Type == TransactionType.Stocks)
        {
            var stockTransaction = await dbContext.StockTransactions.FirstOrDefaultAsync(x => x.TransactionId == request.Id, cancellationToken);
            if (stockTransaction == null)
                return await Result.FailAsync(WellKnown.ErrorCodes.NotFound);
            dbContext.Remove(stockTransaction);
        }
        else
        {
            return await Result.FailAsync(WellKnown.ErrorCodes.NotImplemented);
        }

        dbContext.Transactions.Remove(transaction);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await Result.SuccessAsync();
    }
}