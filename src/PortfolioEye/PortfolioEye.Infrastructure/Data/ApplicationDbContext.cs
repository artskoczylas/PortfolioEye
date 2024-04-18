using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Domain.Entities;

namespace PortfolioEye.Infrastructure.Data
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
	{
		public DbSet<Currency> Currencies { get; set; }
		public DbSet<Potfolio> Portfotfolios { get; set; }
		
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Transaction> Transactions { get; set; }
		public DbSet<StockTransaction> StockTransactions { get; set; }
	}
}
