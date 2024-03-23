using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using MudBlazor;
using PortfolioEye.Application.Features.Users;
using PortfolioEye.Application.Features.Users.Commands;
using PortfolioEye.Application.Features.Users.Queries;
using PortfolioEye.Client.Infrastructure.Managers;

namespace PortfolioEye.Client.Pages
{
	public partial class Profile
	{
		[Inject] protected IStringLocalizer<Profile>? Localizer { get; set; }
		
		private string? AvatarImageLink { get; set; }
		private string AvatarButtonText => (AvatarImageLink == null) ? "Dodaj zdjęcie" : "Usuń zdjęcie";
		private Color AvatarButtonColor => (AvatarImageLink == null) ? Color.Primary : Color.Error;
		private string? FirstName { get; set; }
		private string? LastName { get; set; }
		private string? Email { get; set; }
		
		private void DeletePicture()
		{
			if (!string.IsNullOrEmpty(AvatarImageLink))
			{
				AvatarImageLink = null;
			}
		}

		IList<IBrowserFile> files = new List<IBrowserFile>();
		private void UploadFiles(IBrowserFile file)
		{
			files.Add(file);
			//TODO upload the files to the server
		}


		
		[Inject]
		public ISnackbar? Snackbar { get; set; }

		[Inject]
		public NavigationManager? NavigationManager { get; set; }
		
		[Inject]
		public ICurrentUserManager? CurrentUserManager { get; set; }

		private UserProfileResponse? _profile;

		protected override async Task OnInitializedAsync()
		{
			var result = await CurrentUserManager!.RetrieveMyProfile();
			if (result.IsSuccess)
			{
				_profile = result.Data;
				FirstName = result.Data!.FirstName;
				LastName = result.Data!.LastName;
				Email = result.Data!.Email;
				AvatarImageLink = result.Data!.PhotoUrl;
			}
			else
				Snackbar?.Add("Nie udało się pobrać profilu", Severity.Warning);
		}
		
		private async Task SaveChanges()
		{
			var profileCommand = new UpdateProfileCommand(FirstName, LastName);
			var result = await CurrentUserManager!.UpdateMyProfile(profileCommand);
			if(result.IsSuccess)
				Snackbar?.Add("Zapisano dane profilu", Severity.Info);
			else
				Snackbar?.Add("Nie udało się zapisać profilu", Severity.Warning);
		}

		private void NavigateWithReload(string url) => NavigationManager!.NavigateTo(url, true);
	}
}