using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PortfolioEye.Domain.Entities;

public class BondEmission
{
    public Guid Id { get; set; }
    public BondEmissionKind Kind { get; set; }
    [StringLength(10)] public string Emission { get; set; } = null!;
    public string Isin { get; set; }
    public DateOnly SaleStart { get; set; }
    public DateOnly SaleEnd { get; set; }
    [Precision(18, 4)] public decimal? ConvertPrice { get; set; }
    [Precision(18, 4)] public decimal FirstYearInterestRate { get; set; }
    [Precision(18, 4)] public decimal NextYearsInterestMargin { get; set; }
    public virtual List<BondEmissionsYear> Years { get; set; } = null!;
}

public class BondEmissionsYear
{
    public Guid Id { get; set; }
    public Guid BondEmissionId { get; set; }
    public BondEmission BondEmission { get; set; } = null!;
    public int No { get; set; }
    [Precision(18, 4)] public decimal? InterestRate { get; set; }
}

public class BondTransaction
{
    public Guid Id { get; set; }
    public Guid TransactionId { get; set; }
    public Transaction Transaction { get; set; } = null!;
    public Guid EmissionId { get; set; }
    public BondEmission Emission { get; set; } = null!;
    public BondTransactionSide Side { get; set; }
    [Precision(18, 8)] public decimal Quantity { get; set; }
    [Precision(18, 6)] public decimal Price { get; set; }

    [StringLength(3)] public string Currency { get; set; } = null!;

    [Precision(18, 6)] public decimal PriceInPortfolioCurrency { get; set; }
}

public enum BondTransactionSide
{
    Sale = 0,
    Purchase = 1
}

public enum BondEmissionKind
{
    Edo = 0,
    Rod = 1
}