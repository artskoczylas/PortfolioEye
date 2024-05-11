using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Transactions.Queries;

public record RetrieveTransactionsByUserIdQuery(Guid UserId) : IRequest<IResult<RetrieveTransactionsByUserIdQuery.Response>>
{
    public record Response(List<Transaction> Transactions);

    public record Transaction(string PortfolioName, string AccountName, Guid Id, decimal Value, string Currency, DateOnly TransactionDate);
}