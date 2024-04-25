using System.Net.Http.Json;
using System.Text.Json.Serialization;
using PortfolioEye.Infrastructure.Interfaces;

namespace PortfolioEye.Infrastructure.Services;

public class NbpCurrencyRatesApiService : ICurrencyRatesApiService
{
    public async Task<IEnumerable<DayRate>?> GetRates(string fromCurrency, string toCurrency, DateOnly from,
        DateOnly to)
    {
        if (!"PLN".Equals(toCurrency, StringComparison.InvariantCultureIgnoreCase))
            throw new NotSupportedException("Only conversion to PLN is supported");
        try
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://api.nbp.pl");
            var resp = await client.GetFromJsonAsync<NbpCurrencyRates>(
                $"/api/exchangerates/rates/a/{fromCurrency.ToLower()}/{from:yyyy-MM-dd}/{to:yyyy-MM-dd}/?format=json");

            if (resp == null)
                return null;
            return resp.Rates.Select(x => new DayRate(x.EffectiveDate, x.Mid, resp.Code, "PLN"));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public partial class NbpCurrencyRates
    {
        [JsonPropertyName("table")] public string Table { get; set; }

        [JsonPropertyName("currency")] public string Currency { get; set; }

        [JsonPropertyName("code")] public string Code { get; set; }

        [JsonPropertyName("rates")] public NbpRate[] Rates { get; set; }
    }

    public partial class NbpRate
    {
        [JsonPropertyName("no")] public string No { get; set; }

        [JsonPropertyName("effectiveDate")] public DateOnly EffectiveDate { get; set; }

        [JsonPropertyName("mid")] public decimal Mid { get; set; }
    }
}