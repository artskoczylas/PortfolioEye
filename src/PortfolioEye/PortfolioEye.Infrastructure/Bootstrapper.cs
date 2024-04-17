using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using PortfolioEye.Infrastructure.Services;

namespace PortfolioEye.Infrastructure
{
	public static class Bootstrapper
	{
		public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
		{
			ArgumentNullException.ThrowIfNull(services);
			services.AddTransient<NbpCurrencyRatesService>();
			services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
			return services;
		}
	}
}
