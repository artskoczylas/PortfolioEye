using MediatR;
using PortfolioEye.Application.Features.CurrencyRates.Commands;
using PortfolioEye.Application.Features.CurrencyRates.Queries;
using PortfolioEye.Infrastructure.Interfaces;

namespace PortfolioEye.Infrastructure.Services;

public class CurrencyRateService(IMediator mediator, ICurrencyRatesApiService currencyRatesApi)
    : ICurrencyRateService
{
    public async Task<decimal> GetRateAsync(string fromCurrency, string toCurrency, DateOnly date)
    {
        if (fromCurrency.Equals(toCurrency, StringComparison.InvariantCultureIgnoreCase))
            return 1;
        var databaseRate = await mediator.Send(new GetDayRateForCurrencies(fromCurrency, toCurrency, date));
        if (databaseRate.IsSuccess)
            return databaseRate.Data!.Value;

        var apiRate = (await currencyRatesApi.GetRates(fromCurrency, toCurrency, date, date))?.ToList();

        if (apiRate == null || apiRate.Count == 0)
            throw new CannotGetRateException();
        var firstRate = apiRate.First();
        var command = new AddCurrencyRate(firstRate.FromCurrency, firstRate.ToCurrency, firstRate.Date, firstRate.Rate);
        await mediator.Send(command);

        return firstRate.Rate;
    }

    public async Task CheckRatesAsync(string fromCurrency, string toCurrency, DateOnly fromDate, DateOnly toDate)
    {
        if (fromDate == toDate)
            return;

        ArgumentOutOfRangeException.ThrowIfGreaterThan(fromDate, toDate);

        var dbRatesResult =
            await mediator.Send(new GetRatesInRangeForCurrenciesQuery(fromCurrency, toCurrency, fromDate, toDate));

        var dbRatesList = dbRatesResult.IsSuccess ? dbRatesResult.Data!.Rates : [];
        var apiRates = (await currencyRatesApi.GetRates(fromCurrency, toCurrency, fromDate, toDate))?.ToList();
        if (apiRates == null || apiRates.Count == 0)
            return;
        var currentDay = fromDate;
        while (currentDay < toDate)
        {
            if (dbRatesList.Any(x => x.Date == currentDay))
            {
                currentDay = currentDay.AddDays(1);
                continue;
            }

            var apiRate = apiRates.FirstOrDefault(x => x.Date == currentDay);
            if (apiRate != null)
            {
                var command = new AddCurrencyRate(apiRate.FromCurrency, apiRate.ToCurrency, apiRate.Date,
                    apiRate.Rate);
                await mediator.Send(command);
            }

            currentDay = currentDay.AddDays(1);
        }
    }
}

public class CannotGetRateException : Exception
{
}