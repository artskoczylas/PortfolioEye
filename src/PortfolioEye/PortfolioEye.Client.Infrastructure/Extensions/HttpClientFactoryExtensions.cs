using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioEye.Client.Infrastructure.Extensions
{
	public static class HttpClientFactoryExtensions
	{
		public static HttpClient MainApiClient(this IHttpClientFactory factory) => factory.CreateClient("MainApi");
	}
}
