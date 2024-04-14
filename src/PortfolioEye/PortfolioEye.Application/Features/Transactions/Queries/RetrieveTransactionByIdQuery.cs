using MediatR;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Application.Features.Transactions.Queries;

public record RetrieveTransactionByIdQuery(Guid Id) : IRequest<IResult<RetrieveTransactionByIdQuery.Response>>
{
    public record Response(Guid Id, decimal Value, string Currency, DateTime TransactionDate);
}