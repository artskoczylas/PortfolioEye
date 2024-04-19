namespace PortfolioEye.Domain.Entities;

public class PolishRetailTreasuryBondTransaction
{
    public Guid Id { get; set; }
    public Guid TransactionId { get; set; }
    public Transaction? Transaction { get; set; }
    public string Emission { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
}