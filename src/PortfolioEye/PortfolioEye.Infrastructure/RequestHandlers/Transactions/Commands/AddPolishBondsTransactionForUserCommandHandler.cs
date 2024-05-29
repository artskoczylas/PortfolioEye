using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Transactions.Commands;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Domain.Entities;
using PortfolioEye.Infrastructure.Data;
using PortfolioEye.Infrastructure.Events;
using PortfolioEye.Infrastructure.Interfaces;
using TransactionType = PortfolioEye.Domain.Entities.TransactionType;

namespace PortfolioEye.Infrastructure.RequestHandlers.Transactions.Commands;

public class AddPolishBondsTransactionForUserCommandHandler(
    ApplicationDbContext dbContext,
    ICurrencyRateService rateService,
    IMediator mediator,
    ILogger<AddPolishBondsTransactionForUserCommandHandler> logger)
    : IRequestHandler<AddPolishBondsTransactionForUserCommand, IResult>
{
    public async Task<IResult> Handle(AddPolishBondsTransactionForUserCommand request,
        CancellationToken cancellationToken)
    {
//         var portfolio = await dbContext.Portfotfolios.FindAsync(request.PortfolioId);
//             var portfolioCurrencyRate =
//                 await rateService.GetRateAsync("PLN", portfolio!.Currency, request.TransactionDate);
//             
//             var transactionValue = Math.Round(request.Quantity * 100m, 2);
//             var transactionValuePortfolioCurrency = Math.Round(transactionValue * portfolioCurrencyRate, 2);
//             var transactionPriceInPortfolioCurrency = Math.Round(100m * portfolioCurrencyRate, 2);
//
//             var transaction = new Transaction()
//             {
//                 Currency = "PLN",
//                 AccountId = request.AccountId,
//                 PortfolioId = request.PortfolioId,
//                 TransactionDate = request.TransactionDate,
//                 Value = transactionValue,
//                 Type = TransactionType.Bonds,
//                 UserId = request.UserId.ToString(),
//                 ValueInPortfolioCurrency = transactionValuePortfolioCurrency
//             };
//             var emissionCode = getEmissionCode(request.Kind, request.TransactionDate);
//             var emission = await dbContext.BondEmissions.FirstOrDefaultAsync(x => x.Emission == emissionCode, cancellationToken);
//             if (emission == null)
//             {
//                 emission = new BondEmission()
//                 {
// Kind = request.Kind.Adapt<BondEmissionKind>(),
// FirstYearInterestRate = request.
//                 };
//             }
//             var bondTransaction = new BondTransaction()
//             {
//                 "PLN",
//                 
//             }
//             await dbContext.StockTransactions.AddAsync(stockTransaction, cancellationToken);
//             await dbContext.SaveChangesAsync(cancellationToken);
//
//             await mediator.Publish(new StockTransactionAdded(stockTransaction.Id), cancellationToken);
//
//             return await Result.SuccessAsync();
        return await Result.FailAsync(WellKnown.ErrorCodes.NotImplemented);
    }

    private string getEmissionCode(BondKind kind, DateOnly date)
    {
        switch (kind)
        {
            case BondKind.EDO:
                var edoEndDate = date.AddYears(10);
                return $"EDO{edoEndDate.ToString("yyMM")}";
            case BondKind.ROD:
                var rodEndDate = date.AddYears(10);
                return $"ROD{rodEndDate.ToString("yyMM")}";
            default:
                throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
        }
    }
}