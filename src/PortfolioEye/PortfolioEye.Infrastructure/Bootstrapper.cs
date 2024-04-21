using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using PortfolioEye.Infrastructure.Interfaces;
using PortfolioEye.Infrastructure.Services;

namespace PortfolioEye.Infrastructure
{
	public static class Bootstrapper
	{
		public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
		{
			ArgumentNullException.ThrowIfNull(services);
			services.AddTransient<ICurrencyRatesApiService, NbpCurrencyRatesApiService>();
			services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
			return services;
		}
	}
}
