using PortfolioEye.Application.Features.Portfolios.Queries;
using PortfolioEye.Client.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using PortfolioEye.Application.Features.Users;
using PortfolioEye.Application.Features.Users.Commands;
using PortfolioEye.Application.Features.Users.Queries;
using PortfolioEye.Common.Wrappers;

namespace PortfolioEye.Client.Infrastructure.Managers
{
	public class CurrentUserManager(IHttpClientFactory factory) : ICurrentUserManager
	{
		public async Task<IResult<UserProfileResponse>> RetrieveMyProfile()
		{
			var response = await factory.MainApiClient().GetAsync($"/api/Me/Profile");
			return await response.ToResult<UserProfileResponse>();
		}

		public async Task<IResult> UpdateMyProfile(UpdateProfileCommand profile)
		{
			var response = await factory.MainApiClient().PutAsJsonAsync($"/api/Me/Profile", profile);
			return await response.ToResult();
		}
		
		public async Task<IResult> UploadPhoto(string base64)
		{
			var response = await factory.MainApiClient().PostAsJsonAsync("/api/Me/Profile/Photo", new UploadProfilePhotoCommand(base64));
			return await response.ToResult();
		}

		public async Task<IResult> DeletePhoto()
		{
			var response = await factory.MainApiClient().DeleteAsync("/api/Me/Profile/Photo");
			return await response.ToResult();
		}
	}

	public interface ICurrentUserManager : IManager
	{
		Task<IResult<UserProfileResponse>> RetrieveMyProfile();
		Task<IResult> UpdateMyProfile(UpdateProfileCommand profile);
		Task<IResult> UploadPhoto(string base64);
		Task<IResult> DeletePhoto();
	}
}
