using MediatR;
using PortfolioEye.Application.Features.Transactions.Commands;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Domain.Entities;
using PortfolioEye.Infrastructure.Data;
using StockTransactionSide = PortfolioEye.Application.Features.Transactions.Commands.StockTransactionSide;
using TransactionType = PortfolioEye.Domain.Entities.TransactionType;

namespace PortfolioEye.Infrastructure.Handlers.Transactions.Commands;

public class AddStockTransactionForUserCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<AddStockTransactionForUserCommand, IResult>
{
    public async Task<IResult> Handle(AddStockTransactionForUserCommand request, CancellationToken cancellationToken)
    {
        var transaction = new Transaction()
        {
            Currency = request.Currency,
            AccountId = request.AccountId,
            PortfolioId = request.PortfolioId,
            TransactionDate = request.TransactionDate,
            Value = Math.Round(request.Quantity * request.Price, 2),
            Type = TransactionType.Stocks,
            UserId = request.UserId.ToString()
        };
        var stockTransaction = new StockTransaction()
        {
            Currency = request.Currency,
            Price = request.Price,
            Quantity = request.Quantity,
            Side = request.Side == StockTransactionSide.Purchase
                ? Domain.Entities.StockTransactionSide.Purchase
                : Domain.Entities.StockTransactionSide.Sale,
            FeeCurrency = request.FeeCurrency,
            Ticker = request.Ticker,
            FeeValue = request.FeeValue,
            Transaction = transaction
        };
        await dbContext.StockTransactions.AddAsync(stockTransaction, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return await Result.SuccessAsync();
    }
}