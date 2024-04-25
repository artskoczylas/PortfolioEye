using MediatR;
using PortfolioEye.Application.Features.Transactions.Commands;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Domain.Entities;
using PortfolioEye.Infrastructure.Data;
using PortfolioEye.Infrastructure.Interfaces;
using StockTransactionSide = PortfolioEye.Application.Features.Transactions.Commands.StockTransactionSide;
using TransactionType = PortfolioEye.Domain.Entities.TransactionType;

namespace PortfolioEye.Infrastructure.Handlers.Transactions.Commands;

public class AddStockTransactionForUserCommandHandler(ApplicationDbContext dbContext, ICurrencyRateService rateService)
    : IRequestHandler<AddStockTransactionForUserCommand, IResult>
{
    public async Task<IResult> Handle(AddStockTransactionForUserCommand request, CancellationToken cancellationToken)
    {
        var portfolio = await dbContext.Portfotfolios.FindAsync(request.PortfolioId);
        var rate = await rateService.GetRateAsync(request.Currency, portfolio!.Currency, request.TransactionDate);
        var transactionValue = Math.Round(request.Quantity * request.Price, 2);
        var transactionValuePortfolioCurrency = Math.Round(transactionValue * rate, 2);
        var transaction = new Transaction()
        {
            Currency = request.Currency,
            AccountId = request.AccountId,
            PortfolioId = request.PortfolioId,
            TransactionDate = request.TransactionDate,
            Value = transactionValue,
            Type = TransactionType.Stocks,
            UserId = request.UserId.ToString(),
            ValueInPortfolioCurrency = transactionValuePortfolioCurrency
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
        
        //Po zapisaniu rzuć event, że dodano transakcje i pewnie że transakcję stock
        //Handler eventu pobierze kursy od tej daty do końca (jeśli nie ma), pewnie trzeba na to metodę w currencyRateService
        //Kolejny handler będzie mógł ogarnąć pobranie kursów akcji
    }
}