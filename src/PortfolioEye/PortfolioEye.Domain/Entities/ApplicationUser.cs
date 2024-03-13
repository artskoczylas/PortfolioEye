using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PortfolioEye.Domain.Entities
{
	// Add profile data for application users by adding properties to the ApplicationUser class
	public class ApplicationUser : IdentityUser
	{
		[StringLength(150)]
		public string? FirstName { get; set; }
		[StringLength(150)]
		public string? LastName { get; set; }
		[StringLength(150)]
		public string? PhotoUri { get; set; }
	}

}
