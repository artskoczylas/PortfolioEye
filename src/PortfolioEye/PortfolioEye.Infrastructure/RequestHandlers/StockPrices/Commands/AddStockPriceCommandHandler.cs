using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.StockPrices.Commands;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Domain.Entities;
using PortfolioEye.Infrastructure.Data;

namespace PortfolioEye.Infrastructure.RequestHandlers.StockPrices.Commands;

public class AddStockPriceCommandHandler(ApplicationDbContext dbContext)
    : IRequestHandler<AddStockPriceCommand, IResult>
{
    public async Task<IResult> Handle(AddStockPriceCommand request, CancellationToken cancellationToken)
    {
        var existing = await dbContext.StockPrices.FirstOrDefaultAsync(x =>
            x.Ticker.Equals(request.Ticker.ToUpper()) && x.Date == request.Date, cancellationToken);
        if (existing != null)
            return await Result.FailAsync(WellKnown.ErrorCodes.Conflict);

        var newStockPrice = new StockPrice();
        newStockPrice.Date = request.Date;
        newStockPrice.Price = request.Price;
        newStockPrice.Ticker = request.Ticker.ToUpper();
        newStockPrice.Source = request.Source.Adapt<PortfolioEye.Domain.Entities.StockPriceSource>();

        await dbContext.StockPrices.AddAsync(newStockPrice, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return await Result.SuccessAsync();
    }
}