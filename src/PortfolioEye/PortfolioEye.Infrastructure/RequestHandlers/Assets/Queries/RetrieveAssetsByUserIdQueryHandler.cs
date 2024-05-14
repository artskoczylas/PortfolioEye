using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application.Features.Assets.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.RequestHandlers.Assets.Queries;

public class RetrieveAssetsByUserIdQueryHandler(
    ApplicationDbContext dbContext)
    : IRequestHandler<RetrieveAssetsByUserIdQuery, IResult<RetrieveAssetsByUserIdQuery.Response>>
{
    public async Task<IResult<RetrieveAssetsByUserIdQuery.Response>> Handle(
        RetrieveAssetsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var sqlResult = await dbContext.Database
            .SqlQuery<RetrieveAssetsByUserIdQuery.Asset>($"""
              SELECT
                 0 AS "Type",
                 ST."Ticker",
                 sum(T."Value") AS "Value",
                 T."Currency"
             FROM public."Transactions" T
             INNER JOIN public."StockTransactions" ST on T."Id" = ST."TransactionId"
             WHERE T."UserId" = {request.UserId.ToString()}
             GROUP BY ST."Ticker", T."Currency";
             """).ToListAsync(cancellationToken);
        
        var response = new RetrieveAssetsByUserIdQuery.Response(sqlResult);
        return await response.ToSuccessResultAsync();
    }
}