using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace PortfolioEye.Infrastructure.Services;

public class NbpCurrencyRatesService 
{
    public async Task<CurrencyRates?> GetRates(string currency, DateOnly from, DateOnly to)
    {
        try
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://api.nbp.pl");
            var resp = await client.GetFromJsonAsync<CurrencyRates>(
                $"/api/exchangerates/rates/a/{currency.ToLower()}/{from}/{to}/?format=json");
            
            return resp;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
public partial class CurrencyRates
{
    [JsonPropertyName("table")]
    public string Table { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("rates")]
    public Rate[] Rates { get; set; }
}

public partial class Rate
{
    [JsonPropertyName("no")]
    public string No { get; set; }

    [JsonPropertyName("effectiveDate")]
    public DateTimeOffset EffectiveDate { get; set; }

    [JsonPropertyName("mid")]
    public decimal Mid { get; set; }
}