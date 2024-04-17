using MediatR;
using PortfolioEye.Application.Features.Transactions.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Services;

namespace PortfolioEye.Infrastructure.Handlers.Transactions.Queries;

public class RetrieveTransactionsByUserIdQueryHandler
    : IRequestHandler<RetrieveTransactionsByUserIdQuery, IResult<RetrieveTransactionsByUserIdQuery.Response>>
{
    public async Task<IResult<RetrieveTransactionsByUserIdQuery.Response>> Handle(RetrieveTransactionsByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await new RetrieveTransactionsByUserIdQuery.Response([]).ToSuccessResultAsync();
    }
}