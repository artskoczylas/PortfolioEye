using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Transactions.Queries;

public record RetrieveTransactionsByUserIdQuery(Guid UserId) : IRequest<IResult<RetrieveTransactionsByUserIdQuery.Response>>
{
    public record Response(List<Transaction> Transactions);

    public record Transaction(Guid Id, decimal Value, string Currency, DateTime TransactionDate);
}