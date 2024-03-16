using eastsoft.RCP.Shared.Wrappers;
using PortfolioEye.Application.Features.Portfolios.Queries;
using PortfolioEye.Client.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PortfolioEye.Application.Features.Users;

namespace PortfolioEye.Client.Infrastructure.Managers
{
	public class CurrentUserManager(HttpClient httpClient) : ICurrentUserManager
	{
		public async Task<IResult<UserProfileResponse>> RetrieveMyProfile()
		{
			var response = await httpClient.GetAsync($"/api/Me/Profile");
			return await response.ToResult<UserProfileResponse>();
		}
	}

	public interface ICurrentUserManager : IManager
	{
		Task<IResult<UserProfileResponse>> RetrieveMyProfile();

	}
}
