using System.ComponentModel.DataAnnotations;

namespace PortfolioEye.Domain.Entities;

public class Account
{
    public Guid Id { get; set; }
    
    [StringLength(450)]
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    
    [StringLength(200)]
    [Required]
    public string Name { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    [StringLength(3)]
    [Required]
    public string Currency { get; set; }
    
    public AccountType Type { get; set; }
    public enum AccountType
    {
        Cash = 0,
        Shares = 1,
        Bonds = 2,
        Deposits = 3,
        Savings = 4
    }
    
    public ICollection<Transaction> Transactions { get; set; }
}