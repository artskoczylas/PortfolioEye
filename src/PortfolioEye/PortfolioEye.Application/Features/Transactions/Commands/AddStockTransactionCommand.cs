using MediatR;
using PortfolioEye.Application.Features.Accounts.Queries;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Transactions.Commands;

public record AddStockTransactionForUserCommand(Guid UserId, AddStockTransactionCommand Inner)
    : AddStockTransactionCommand(Inner);
public record AddStockTransactionCommand(
    Guid PortfolioId,
    Guid AccountId,
    StockTransactionSide Side,
    DateOnly TransactionDate,
    string Ticker,
    decimal Quantity,
    decimal Price,
    string Currency,
    decimal FeeValue,
    string FeeCurrency)
    : IRequest<IResult>;
    
public enum StockTransactionSide
{
    Purchase,
    Sale
}