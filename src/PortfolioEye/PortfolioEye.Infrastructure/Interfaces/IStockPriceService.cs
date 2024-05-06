using PortfolioEye.Application.Features.StockPrices.Commands;

namespace PortfolioEye.Infrastructure.Services;

public interface IStockPriceService
{
    Task CheckRatesAsync(StockPriceSource source, string ticker, DateOnly fromDate, DateOnly toDate);
}