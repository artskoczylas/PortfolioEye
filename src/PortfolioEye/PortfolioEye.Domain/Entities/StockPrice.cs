using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace PortfolioEye.Domain.Entities;

public enum StockPriceSource
{
    Yahoo = 1
}

public class StockPrice
{
    [StringLength(9)]
    public string Ticker { get; set; } = null!;
    public StockPriceSource Source { get; set; }
    public DateOnly Date { get; set; }
    [Precision(18,6)]
    public decimal Price { get; set; }
}