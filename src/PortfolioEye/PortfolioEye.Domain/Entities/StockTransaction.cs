using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PortfolioEye.Domain.Entities;

public class StockTransaction
{
    public Guid Id { get; set; }
    public Guid TransactionId { get; set; }
    public Transaction Transaction { get; set; } = null!;
    public StockTransactionSide Side { get; set; }
    [StringLength(9)] public string Ticker { get; set; } = null!;
    [Precision(18, 8)] public decimal Quantity { get; set; }
    [Precision(18, 6)] public decimal Price { get; set; }

    [StringLength(3)] public string Currency { get; set; } = null!;
    [Precision(18, 2)] public decimal FeeValue { get; set; }

    [StringLength(3)] public string FeeCurrency { get; set; } = null!;

    [Precision(18, 6)] public decimal PriceInStockCurrency { get; set; }
    [Precision(18, 2)] public decimal ValueInStockCurrency { get; set; }
    [StringLength(3)] public string StockCurrency { get; set; } = null!;
}

public enum StockTransactionSide
{
    Sale = 0,
    Purchase = 1
}