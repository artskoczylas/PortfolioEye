using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace PortfolioEye.Application
{
	public static class Bootstrapper
	{
		public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
		{
			ArgumentNullException.ThrowIfNull(services);

			services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
			return services;
		}
	}
}
