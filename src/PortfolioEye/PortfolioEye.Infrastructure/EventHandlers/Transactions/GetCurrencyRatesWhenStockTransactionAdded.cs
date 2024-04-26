using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortfolioEye.Infrastructure.Data;
using PortfolioEye.Infrastructure.Events;
using PortfolioEye.Infrastructure.Interfaces;

namespace PortfolioEye.Infrastructure.EventHandlers.Transactions;

public class GetCurrencyRatesWhenStockTransactionAdded(ICurrencyRateService rateService, ApplicationDbContext dbContext, ILogger<GetCurrencyRatesWhenStockTransactionAdded> logger)
    : INotificationHandler<StockTransactionAdded>
{
    public async Task Handle(StockTransactionAdded notification, CancellationToken cancellationToken)
    {
        try
        {
            var stockTransaction = await dbContext.StockTransactions.Include(x => x.Transaction)
                .ThenInclude(x => x.Portfolio).FirstOrDefaultAsync(x => x.Id == notification.Id, cancellationToken);

            if(stockTransaction == null)
                return;
            await rateService.CheckRatesAsync(stockTransaction.Currency, stockTransaction.Transaction.Portfolio!.Currency,
                stockTransaction.Transaction.TransactionDate, DateOnly.FromDateTime(DateTime.Now));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error in GetCurrencyRatesWhenStockTransactionAdded");
        }
    }
}