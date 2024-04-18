using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PortfolioEye.Domain.Entities;

public class StockTransaction
{
    public Guid Id { get; set; }
    public Guid TransactionId { get; set; }
    public Transaction Transaction { get; set; }
    public StockTransactionSide Side { get; set; }
    public string Ticker { get; set; }
    [Precision(18,8)]
    public decimal Quantity { get; set; }
    [Precision(18,6)]
    public decimal Price { get; set; }
    [StringLength(3)]
    public string Currency { get; set; }
    [Precision(18,2)]
    public decimal FeeValue { get; set; }
    [StringLength(3)]
    public string FeeCurrency { get; set; }
}

public enum StockTransactionSide
{
    Sale = 0,
    Purchase = 1
}