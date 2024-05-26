using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Transactions.Commands;

public record AddPolishBondsTransactionForUserCommand(Guid UserId, AddPolishBondsTransactionCommand Inner)
    : AddPolishBondsTransactionCommand(Inner);
public record AddPolishBondsTransactionCommand(
    Guid PortfolioId,
    Guid AccountId,
    BondTransactionSide Side,
    DateOnly TransactionDate,
    BondKind Kind,
    int Quantity)
    : IRequest<IResult>;
    
public enum BondTransactionSide
{
    Purchase,
    Sale
}

public enum BondKind
{
    EDO,
    ROD
}