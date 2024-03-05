using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortfolioEye.Domain.Entities;

namespace PortfolioEye.Domain.Entities
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
	{
	}
}
