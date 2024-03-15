using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PortfolioEye.Client.Infrastructure.Managers;

namespace PortfolioEye.Client.Infrastructure
{
	public static class Bootstrapper
	{
		public static IServiceCollection AddManagers(this IServiceCollection services)
		{
			var managers = typeof(IManager);

			var types = managers
				.Assembly
				.GetExportedTypes()
				.Where(t => t.IsClass && !t.IsAbstract)
				.Select(t => new
				{
					Service = t.GetInterface($"I{t.Name}"),
					Implementation = t
				})
				.Where(t => t.Service != null);

			foreach (var type in types)
			{
				if (managers.IsAssignableFrom(type.Service))
				{
					services.AddTransient(type.Service, type.Implementation);
				}
			}

			return services;
		}

		public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
		{
			ArgumentNullException.ThrowIfNull(services);

			services.AddManagers();
			var asada = Assembly.GetExecutingAssembly();
			services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
			return services;
		}
	}
}
