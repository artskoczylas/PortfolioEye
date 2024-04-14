using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Transactions.Commands;

public record AddTransactionCommand(decimal Value, string Currency, DateTime TransactionDate)
    : IRequest<IResult>;

public record EditTransactionCommand(Guid Id, decimal Value, string Currency, DateTime TransactionDate)
    : IRequest<IResult>;

public record AddTransactionForUserCommand(Guid UserId, AddTransactionCommand BaseCommand)
    : AddTransactionCommand(BaseCommand);

public record EditTransactionForUserCommand(Guid UserId, EditTransactionCommand BaseEditCommand)
    : EditTransactionCommand(BaseEditCommand);

