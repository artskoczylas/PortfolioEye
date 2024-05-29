using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Mapster;
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
			services.AddTransient<ICurrencyRateService, CurrencyRateService>();
			services.AddTransient<IStockMarketDataProvider, YahooStockMarketDataProvider>();
			services.AddTransient<IStockPriceService, StockPriceService>();
			services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
			
			TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
			
			return services;
		}
	}
}
