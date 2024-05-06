using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortfolioEye.Application.Features.StockPrices.Commands;
using PortfolioEye.Infrastructure.Data;
using PortfolioEye.Infrastructure.Events;
using PortfolioEye.Infrastructure.Interfaces;
using PortfolioEye.Infrastructure.Services;

namespace PortfolioEye.Infrastructure.EventHandlers.Transactions;

public class GetStockPricesWhenStockTransactionAdded(IStockPriceService stockPriceService, ApplicationDbContext dbContext, ILogger<GetStockPricesWhenStockTransactionAdded> logger)
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
            await stockPriceService.CheckRatesAsync(StockPriceSource.Yahoo, stockTransaction.Ticker, 
                stockTransaction.Transaction.TransactionDate, DateOnly.FromDateTime(DateTime.Now));
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error in GetCurrencyRatesWhenStockTransactionAdded");
        }
    }
}