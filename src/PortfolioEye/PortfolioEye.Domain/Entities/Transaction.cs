using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PortfolioEye.Domain.Entities;

public class Transaction
{
    public Guid Id { get; set; }
    [StringLength(450)] public string UserId { get; set; } = null!;
    public Guid PortfolioId { get; set; }
    public virtual Potfolio? Portfolio { get; set; }
    public Guid AccountId { get; set; }
    public virtual Account? Account { get; set; }
    public TransactionType Type { get; set; }
    public DateOnly TransactionDate { get; set; }
    [Precision(18, 2)] public decimal Value { get; set; }

    [StringLength(3)] public string Currency { get; set; } = null!;

    [Precision(18, 2)] public decimal ValueInPortfolioCurrency { get; set; }
}

public enum TransactionType
{
    Stocks = 0,
    Bonds = 1,
    Flows = 2,
    Cash = 3
}