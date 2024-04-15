using MediatR;
using PortfolioEye.Application.Features.Transactions.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Services;

namespace PortfolioEye.Infrastructure.Handlers.Transactions.Queries;

public class RetrieveTransactionsByUserIdQueryHandler(CurrencyRatesService srv)
    : IRequestHandler<RetrieveTransactionsByUserIdQuery, IResult<RetrieveTransactionsByUserIdQuery.Response>>
{
    public async Task<IResult<RetrieveTransactionsByUserIdQuery.Response>> Handle(RetrieveTransactionsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var currencies = await srv.GetRates("EUR", new DateOnly(2024, 04, 01), new DateOnly(2024, 04, 12));
        
        return await new RetrieveTransactionsByUserIdQuery.Response([]).ToSuccessResultAsync();
    }
}