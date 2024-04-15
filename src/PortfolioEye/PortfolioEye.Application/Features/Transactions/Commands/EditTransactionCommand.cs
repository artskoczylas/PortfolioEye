using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Transactions.Commands;

public enum TransactionType
{
    Purchase,
    Sale,
    Interest,
    Dividends,
    Expenses,
    Deposit,
    Withdrawal
}

public enum AssetClass
{
    Shares,
    Bonds
}

public record AddTransactionCommand(
    Guid PortfolioId,
    Guid AccountId,
    TransactionType Type,
    AssetClass? Class,
    DateTime TransactionDate,
    string? Ticker,
    decimal Quantity,
    decimal Price,
    string Currency,
    decimal FeeValue,
    string FeeCurrency)
    : IRequest<IResult>;

public record EditTransactionCommand(Guid Id, decimal Value, string Currency, DateTime TransactionDate)
    : IRequest<IResult>;

public record AddTransactionForUserCommand(Guid UserId, AddTransactionCommand BaseCommand)
    : AddTransactionCommand(BaseCommand);

public record EditTransactionForUserCommand(Guid UserId, EditTransactionCommand BaseEditCommand)
    : EditTransactionCommand(BaseEditCommand);