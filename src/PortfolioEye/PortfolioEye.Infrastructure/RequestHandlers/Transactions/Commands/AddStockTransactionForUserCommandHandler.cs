using MediatR;
using Microsoft.Extensions.Logging;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Transactions.Commands;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Domain.Entities;
using PortfolioEye.Infrastructure.Data;
using PortfolioEye.Infrastructure.Events;
using PortfolioEye.Infrastructure.Interfaces;
using PortfolioEye.Infrastructure.Services;
using StockTransactionSide = PortfolioEye.Application.Features.Transactions.Commands.StockTransactionSide;
using TransactionType = PortfolioEye.Domain.Entities.TransactionType;

namespace PortfolioEye.Infrastructure.RequestHandlers.Transactions.Commands;

public class AddStockTransactionForUserCommandHandler(
    ApplicationDbContext dbContext,
    ICurrencyRateService rateService,
    IMediator mediator,
    IStockMarketDataProvider stockMarketDataProvider,
    ILogger<AddStockTransactionForUserCommandHandler> logger)
    : IRequestHandler<AddStockTransactionForUserCommand, IResult>
{
    public async Task<IResult> Handle(AddStockTransactionForUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var stockDetails = await stockMarketDataProvider.GetDetailsAsync(request.Ticker);
            if (stockDetails == null)
                return await Result.FailAsync(WellKnown.ErrorCodes.CannotGetStockInfo);

            var portfolio = await dbContext.Portfotfolios.FindAsync(request.PortfolioId);
            var portfolioCurrencyRate =
                await rateService.GetRateAsync(request.Currency, portfolio!.Currency, request.TransactionDate);
            var stockCurrencyRate =
                await rateService.GetRateAsync(request.Currency, stockDetails.Currency, request.TransactionDate);
            var transactionValue = Math.Round(request.Quantity * request.Price, 2);
            var transactionValuePortfolioCurrency = Math.Round(transactionValue * portfolioCurrencyRate, 2);
            var transactionPriceInPortfolioCurrency = Math.Round(request.Price * portfolioCurrencyRate, 2);
            var transactionValueInStockCurrency = Math.Round(transactionValue * stockCurrencyRate, 2);
            var transactionPriceInStockCurrency = Math.Round(request.Price * stockCurrencyRate, 2);

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
                Transaction = transaction,
                StockCurrency = stockDetails.Currency,
                PriceInStockCurrency = transactionPriceInStockCurrency,
                ValueInStockCurrency = transactionValueInStockCurrency,
                PriceInPortfolioCurrency = transactionPriceInPortfolioCurrency
            };
            await dbContext.StockTransactions.AddAsync(stockTransaction, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            await mediator.Publish(new StockTransactionAdded(stockTransaction.Id), cancellationToken);

            return await Result.SuccessAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        //Handler eventu pobierze kursy od tej daty do końca (jeśli nie ma), pewnie trzeba na to metodę w currencyRateService
        //Kolejny handler będzie mógł ogarnąć pobranie kursów akcji
    }
}