using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Domain.Entities;

namespace PortfolioEye.Infrastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Potfolio> Portfotfolios { get; set; }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions).OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Portfolio)
                .WithMany(p => p.Transactions).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StockTransaction>()
                .HasOne(st => st.Transaction)
                .WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}