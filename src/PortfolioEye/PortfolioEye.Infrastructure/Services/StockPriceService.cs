using MediatR;
using PortfolioEye.Application.Features.StockPrices.Commands;
using PortfolioEye.Application.Features.StockPrices.Queries;
using PortfolioEye.Infrastructure.Interfaces;

namespace PortfolioEye.Infrastructure.Services;

public class StockPriceService(IMediator mediator, IStockMarketDataProvider marketDataProvider) : IStockPriceService
{
    public async Task CheckRatesAsync(StockPriceSource source, string ticker, DateOnly fromDate, DateOnly toDate)
    {
        if (fromDate == toDate)
            return;

        ArgumentOutOfRangeException.ThrowIfGreaterThan(fromDate, toDate);

        var dbPricesResult =
            await mediator.Send(new GetStockPricesInRangeQuery(ticker, fromDate, toDate));

        var dbRatesList = dbPricesResult.IsSuccess ? dbPricesResult.Data!.Rates : [];
        var stockPrices = (await marketDataProvider.GetHistoricalDataAsync(ticker, fromDate, toDate));

        var currentDay = fromDate;
        while (currentDay < toDate)
        {
            if (dbRatesList.Any(x => x.Date == currentDay))
            {
                currentDay = currentDay.AddDays(1);
                continue;
            }

            var apiRate = stockPrices.Days.FirstOrDefault(x => x.Day == currentDay);
            if (apiRate != null)
            {
                var command = new AddStockPriceCommand(source, ticker, apiRate.Day, apiRate.AdjustedClose);
                await mediator.Send(command);
            }

            currentDay = currentDay.AddDays(1);
        }
    }
}