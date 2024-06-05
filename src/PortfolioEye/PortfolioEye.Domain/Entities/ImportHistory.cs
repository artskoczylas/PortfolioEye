namespace PortfolioEye.Domain.Entities;

public enum ImportHistoryType
{
    PolishRetailTreasuryBonds
}
public class ImportHistory
{
    public Guid Id { get; set; }
    public ImportHistoryType Type { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Version { get; set; } = null!;
}