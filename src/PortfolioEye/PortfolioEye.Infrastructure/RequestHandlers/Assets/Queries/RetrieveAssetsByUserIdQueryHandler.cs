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
                 sum(ST."Quantity") AS "Quantity",
                 sum(T."Value") AS "Value",
                 round(sum(T."Value") / sum(ST."Quantity"), 6)  as "AverageBuyPrice",
                 T."Currency",
                 lp."Price" as "CurrentPrice",
                 round(sum(ST."Quantity") * lp."Price", 2) AS "CurrentValue"
             FROM public."Transactions" T
             INNER JOIN public."StockTransactions" ST on T."Id" = ST."TransactionId"
                 INNER JOIN public.laststockprices lp ON lp."Ticker" = ST."Ticker"
             WHERE T."UserId" = {request.UserId.ToString()}
             GROUP BY ST."Ticker", T."Currency", lp."Price";
             """).ToListAsync(cancellationToken);
        
        var response = new RetrieveAssetsByUserIdQuery.Response(sqlResult);
        return await response.ToSuccessResultAsync();
    }
}