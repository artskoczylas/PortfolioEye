using FluentAssertions;
using MediatR;
using NSubstitute;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.CurrencyRates.Commands;
using PortfolioEye.Application.Features.CurrencyRates.Queries;
using PortfolioEye.Common.Extensions;
using PortfolioEye.Common.Wrappers;
using PortfolioEye.Infrastructure.Interfaces;
using PortfolioEye.Infrastructure.Services;

namespace PortfolioEye.Infrastructure.Tests.Services;

[TestFixture]
[TestOf(typeof(CurrencyRateService))]
public class CurrencyRateServiceTest
{
    private IMediator _mediator;
    private ICurrencyRatesApiService _currencyRatesApi;
    private CurrencyRateService _currencyRateService;

    [SetUp]
    public void SetUp()
    {
        _mediator = Substitute.For<IMediator>();
        _currencyRatesApi = Substitute.For<ICurrencyRatesApiService>();
        _currencyRateService = new CurrencyRateService(_mediator, _currencyRatesApi);
    }

    [Test]
    public async Task GetRateAsync_WhenDatabaseRateExists_ShouldReturnDatabaseRate()
    {
        var fromCurrency = "USD";
        var toCurrency = "EUR";
        var date = new DateOnly(2023, 6, 12);
        var expectedRate = 0.85m;
        var databaseRate = new GetDayRateForCurrencies.Response(fromCurrency, toCurrency, date, expectedRate)
            .ToSuccessResultAsync();
        _mediator.Send(Arg.Any<GetDayRateForCurrencies>()).Returns(databaseRate);

        var result = await _currencyRateService.GetRateAsync(fromCurrency, toCurrency, date);

        result.Should().Be(expectedRate);
        await _mediator.Received(1).Send(Arg.Any<GetDayRateForCurrencies>());
        await _currencyRatesApi.DidNotReceive()
            .GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<DateOnly>(), Arg.Any<DateOnly>());
    }

    [Test]
    public async Task GetRateAsync_WhenDatabaseRateDoesNotExist_ShouldFetchRateFromApiAndSaveToDatabase()
    {
        var fromCurrency = "USD";
        var toCurrency = "EUR";
        var date = new DateOnly(2023, 6, 12);
        var expectedRate = 0.85m;
        var databaseRate = Result<GetDayRateForCurrencies.Response>.FailAsync(WellKnown.ErrorCodes.NotFound);
        var apiRate = new List<DayRate> { new DayRate(date, expectedRate, fromCurrency, toCurrency) };
        _mediator.Send(Arg.Any<GetDayRateForCurrencies>()).Returns(databaseRate);
        _currencyRatesApi.GetRates(fromCurrency, toCurrency, date, date).Returns(apiRate);

        var result = await _currencyRateService.GetRateAsync(fromCurrency, toCurrency, date);

        result.Should().Be(expectedRate);
        await _mediator.Received(1).Send(Arg.Any<GetDayRateForCurrencies>());
        await _currencyRatesApi.Received(1).GetRates(fromCurrency, toCurrency, date, date);
        await _mediator.Received(1).Send(Arg.Any<AddCurrencyRate>());
    }

    [Test]
    public async Task GetRateAsync_WhenApiReturnsEmptyRates_ShouldThrowCannotGetRateException()
    {
        string fromCurrency = "USD";
        string toCurrency = "EUR";
        DateOnly date = new DateOnly(2023, 6, 12);

        _mediator.Send(Arg.Any<GetDayRateForCurrencies>())
            .Returns(Result<GetDayRateForCurrencies.Response>.FailAsync(WellKnown.ErrorCodes.NotFound));

        _currencyRatesApi.GetRates(fromCurrency, toCurrency, date, date)!
            .Returns(Task.FromResult<IEnumerable<DayRate>>(new List<DayRate>()));

        await _currencyRateService.Invoking(async x => await x.GetRateAsync(fromCurrency, toCurrency, date))
            .Should().ThrowAsync<CannotGetRateException>();
    }

    [Test]
    public async Task GetRateAsync_ShouldReturn1_WhenFromAndToCurrencyAreTheSame()
    {
        string fromCurrency = "USD";
        string toCurrency = "USD";
        DateOnly date = DateOnly.FromDateTime(DateTime.Now);

        decimal rate = await _currencyRateService.GetRateAsync(fromCurrency, toCurrency, date);

        rate.Should().Be(1);
    }

    [Test]
    public async Task CheckRatesAsync_ShouldReturnWhenFromDateEqualsToDate()
    {
        var fromDate = new DateOnly(2023, 1, 1);
        var toDate = new DateOnly(2023, 1, 1);

        await _currencyRateService.CheckRatesAsync("USD", "EUR", fromDate, toDate);

        await _mediator.DidNotReceive().Send(Arg.Any<GetRatesInRangeForCurrenciesQuery>());
        await _currencyRatesApi.DidNotReceive()
            .GetRates(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<DateOnly>(), Arg.Any<DateOnly>());
    }

    [Test]
    public void CheckRatesAsync_ShouldThrowArgumentOutOfRangeExceptionWhenFromDateGreaterThanToDate()
    {
        var fromDate = new DateOnly(2023, 1, 2);
        var toDate = new DateOnly(2023, 1, 1);

        Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            _currencyRateService.CheckRatesAsync("USD", "EUR", fromDate, toDate));
    }

    [Test]
    public async Task CheckRatesAsync_ShouldAddMissingRatesFromApi()
    {
        var fromCurrency = "USD";
        var toCurrency = "EUR";
        var fromDate = new DateOnly(2023, 1, 1);
        var toDate = new DateOnly(2023, 1, 5);

        var dbRates = new List<GetRatesInRangeForCurrenciesQuery.CurrencyRate>
        {
            new(new DateOnly(2023, 1, 1), 0.8m),
            new(new DateOnly(2023, 1, 3), 0.82m)
        };

        var apiRates = new List<DayRate>
        {
            new DayRate(new DateOnly(2023, 1, 2), 0.81m, fromCurrency, toCurrency),
            new DayRate(new DateOnly(2023, 1, 4), 0.83m, fromCurrency, toCurrency)
        };

        _mediator.Send(Arg.Any<GetRatesInRangeForCurrenciesQuery>())
            .Returns(Result<GetRatesInRangeForCurrenciesQuery.Response>.Success(
                new GetRatesInRangeForCurrenciesQuery.Response(fromCurrency, toCurrency, fromDate, toDate, dbRates)));

        _currencyRatesApi.GetRates(fromCurrency, toCurrency, fromDate, toDate).Returns(apiRates);

        await _currencyRateService.CheckRatesAsync(fromCurrency, toCurrency, fromDate, toDate);

        await _mediator.Received(2).Send(Arg.Any<AddCurrencyRate>());
        await _mediator.Received().Send(Arg.Is<AddCurrencyRate>(x =>
            x.FromCurrency == fromCurrency && x.ToCurrency == toCurrency && x.Date == apiRates[0].Date &&
            x.Value == apiRates[0].Rate));
        await _mediator.Received().Send(Arg.Is<AddCurrencyRate>(x =>
            x.FromCurrency == fromCurrency && x.ToCurrency == toCurrency && x.Date == apiRates[1].Date &&
            x.Value == apiRates[1].Rate));
    }
}