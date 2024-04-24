﻿using MediatR;
using PortfolioEye.Application.Features.CurrencyRates.Commands;
using PortfolioEye.Application.Features.CurrencyRates.Queries;
using PortfolioEye.Infrastructure.Interfaces;

namespace PortfolioEye.Infrastructure.Services;

public class CurrencyRateService : ICurrencyRateService
{
    private readonly IMediator _mediator;
    private readonly ICurrencyRatesApiService _currencyRatesApi;

    public CurrencyRateService(IMediator mediator, ICurrencyRatesApiService currencyRatesApi)
    {
        _mediator = mediator;
        _currencyRatesApi = currencyRatesApi;
    }

    public async Task<decimal> GetRateAsync(string fromCurrency, string toCurrency, DateOnly date)
    {
        var databaseRate = await _mediator.Send(new GetDayRateForCurrencies(fromCurrency, toCurrency, date));
        if (databaseRate.IsSuccess)
            return databaseRate.Data!.Value;

        var apiRate = (await _currencyRatesApi.GetRates(fromCurrency, toCurrency, date, date))?.ToList();

        if (apiRate == null || apiRate.Count == 0)
            throw new CannotGetRateException();
        var firstRate = apiRate.First();
        var command = new AddCurrencyRate(firstRate.FromCurrency, firstRate.ToCurrency, firstRate.Date, firstRate.Rate);
        await _mediator.Send(command);
        
        return firstRate.Rate;
    }
}

public class CannotGetRateException : Exception
{
}