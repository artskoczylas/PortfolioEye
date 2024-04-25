using Microsoft.EntityFrameworkCore;

namespace PortfolioEye.Domain.Entities;

public class CurrencyRate
{
    public int FromCurrencyId { get; set; }
    public virtual Currency FromCurrency { get; set; }
    public int ToCurrencyId { get; set; }
    public virtual Currency ToCurrency { get; set; }
    public DateOnly Date { get; set; }
    [Precision(18,6)]
    public decimal RateValue { get; set; }
}