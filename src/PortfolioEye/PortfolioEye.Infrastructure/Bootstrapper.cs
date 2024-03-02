using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PortfolioEye.Infrastructure
{
	public static class Bootstrapper
	{
		public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
		{
			ArgumentNullException.ThrowIfNull(services);

			services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
			return services;
		}
	}
}
