using eastsoft.RCP.Shared.Wrappers;
using PortfolioEye.Application.Features.Portfolios.Queries;
using PortfolioEye.Client.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioEye.Client.Infrastructure.Managers
{
	public class PortfolioManager(HttpClient httpClient) : IPortfolioManager
	{
		public async Task<IResult<RetrievePortfoliosByUserId.Response>> RetrieveByUserId(Guid userId)
		{
			var response = await httpClient.GetAsync($"/api/portfolios/{userId}");
			return await response.ToResult<RetrievePortfoliosByUserId.Response>();
		}
	}

	public interface IPortfolioManager : IManager
	{
		Task<IResult<RetrievePortfoliosByUserId.Response>> RetrieveByUserId(Guid userId);

	}
}
