using System.ComponentModel.DataAnnotations;

namespace PortfolioEye.Domain.Entities;

public class Currency
{
    public int Id { get; set; }
    [StringLength(3)]
    public string? Code { get; set; }
    public bool IsActive { get; set; }
}