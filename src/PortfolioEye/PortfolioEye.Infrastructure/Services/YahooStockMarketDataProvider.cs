using System.Text.Json.Serialization;
using Flurl;
using Flurl.Http;
using PortfolioEye.Infrastructure.Interfaces;
using YahooFinanceApi;

namespace PortfolioEye.Infrastructure.Services;

public class YahooStockMarketDataProvider() : IStockMarketDataProvider
{
    public async Task<IEnumerable<string>> FindTickerAsync(string query)
    {
        try
        {
            var url = "https://query2.finance.yahoo.com/v1/finance/search"
                .SetQueryParam("q", query)
                .SetQueryParam("lang", "en-US")
                .SetQueryParam("region", "US")
                .SetQueryParam("quotesCount", "6")
                .SetQueryParam("newsCount", "0")
                .SetQueryParam("listsCount", "0")
                .SetQueryParam("enableFuzzyQuery", "false")
                .SetQueryParam("quotesQueryId", "tss_match_phrase_query")
                .SetQueryParam("multiQuoteQueryId", "multi_quote_single_token_query")
                .SetQueryParam("newsQueryId", "news_cie_vespa")
                .SetQueryParam("enableCb", "false")
                .SetQueryParam("enableNavLinks", "false")
                .SetQueryParam("enableEnhancedTrivialQuery", "true")
                .SetQueryParam("enableResearchReports", "false")
                .SetQueryParam("recommendCount", "0");
            
            
           var  data = await url
                .WithHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36 Edg/122.0.0.0")
                .GetAsync()
                .ReceiveJson<SearchResult>()
                .ConfigureAwait(false);

           return data != null ? data.Quotes.Select(x => x.Symbol) : new List<string>();
        }
        catch (FlurlHttpException ex)
        {
            if (ex.Call.Response.StatusCode == (int)System.Net.HttpStatusCode.NotFound)
            {
                return new List<string>();
            }
            else
            {
                throw;
            }
        }
    }

    public async Task<HistoricalData> GetHistoricalDataAsync(string ticker, DateOnly from, DateOnly to)
    {
        var data = await Yahoo.GetHistoricalAsync(ticker, from.ToDateTime(new TimeOnly()),
            to.ToDateTime(new TimeOnly()));
        var result = data.Select(item => new HistoricalDay(DateOnly.FromDateTime(item.DateTime), item.Open,
            item.Close, item.High, item.Low, item.AdjustedClose)).ToList();

        var stockData =await Yahoo.Symbols(ticker).Fields(Field.Currency).QueryAsync();
        return new HistoricalData(stockData[ticker].Currency, result);
    }
}

public partial class SearchResult
    {
        [JsonPropertyName("explains")]
        public object[] Explains { get; set; }

        [JsonPropertyName("count")]
        public long Count { get; set; }

        [JsonPropertyName("quotes")]
        public Quote[] Quotes { get; set; }

        [JsonPropertyName("news")]
        public object[] News { get; set; }

        [JsonPropertyName("nav")]
        public object[] Nav { get; set; }

        [JsonPropertyName("lists")]
        public object[] Lists { get; set; }

        [JsonPropertyName("researchReports")]
        public object[] ResearchReports { get; set; }

        [JsonPropertyName("screenerFieldResults")]
        public object[] ScreenerFieldResults { get; set; }

        [JsonPropertyName("totalTime")]
        public long TotalTime { get; set; }

        [JsonPropertyName("timeTakenForQuotes")]
        public long TimeTakenForQuotes { get; set; }

        [JsonPropertyName("timeTakenForNews")]
        public long TimeTakenForNews { get; set; }

        [JsonPropertyName("timeTakenForAlgowatchlist")]
        public long TimeTakenForAlgowatchlist { get; set; }

        [JsonPropertyName("timeTakenForPredefinedScreener")]
        public long TimeTakenForPredefinedScreener { get; set; }

        [JsonPropertyName("timeTakenForCrunchbase")]
        public long TimeTakenForCrunchbase { get; set; }

        [JsonPropertyName("timeTakenForNav")]
        public long TimeTakenForNav { get; set; }

        [JsonPropertyName("timeTakenForResearchReports")]
        public long TimeTakenForResearchReports { get; set; }

        [JsonPropertyName("timeTakenForScreenerField")]
        public long TimeTakenForScreenerField { get; set; }

        [JsonPropertyName("timeTakenForCulturalAssets")]
        public long TimeTakenForCulturalAssets { get; set; }
    }

    public partial class Quote
    {
        [JsonPropertyName("exchange")]
        public string Exchange { get; set; }

        [JsonPropertyName("shortname")]
        public string Shortname { get; set; }

        [JsonPropertyName("quoteType")]
        public string QuoteType { get; set; }

        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("index")]
        public string Index { get; set; }

        [JsonPropertyName("score")]
        public long Score { get; set; }

        [JsonPropertyName("typeDisp")]
        public string TypeDisp { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("longname")]
        public string Longname { get; set; }

        [JsonPropertyName("exchDisp")]
        public string ExchDisp { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("sector")]
        public string Sector { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("sectorDisp")]
        public string SectorDisp { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("industry")]
        public string Industry { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("industryDisp")]
        public string IndustryDisp { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("dispSecIndFlag")]
        public bool? DispSecIndFlag { get; set; }

        [JsonPropertyName("isYahooFinance")]
        public bool IsYahooFinance { get; set; }
    }