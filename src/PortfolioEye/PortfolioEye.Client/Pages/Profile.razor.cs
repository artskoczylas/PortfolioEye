using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Net;
using System.Text.RegularExpressions;
using MediatR;
using PortfolioEye.Application.Features.Users;

namespace PortfolioEye.Client.Pages
{
	public partial class Profile
	{
		
		public string? AvatarImageLink { get; set; }
		public string AvatarButtonText
		{
			get { return (AvatarImageLink == null) ? "Dodaj zdjęcie" : "Usuń zdjęcie"; }
		}
		public Color AvatarButtonColor
		{
			get { return (AvatarImageLink == null) ? Color.Primary : Color.Error; }
		}
		public string FirstName { get; set; } = "Jonny";
		public string LastName { get; set; } = "Larsson";
		public string JobTitle { get; set; } = "IT Consultant";
		public string Email { get; set; } = "Youcanprobably@findout.com";
		public bool FriendSwitch { get; set; } = true;
		public bool NotificationEmail_1 { get; set; } = true;
		public bool NotificationEmail_2 { get; set; }
		public bool NotificationEmail_3 { get; set; }
		public bool NotificationEmail_4 { get; set; } = true;
		public bool NotificationChat_1 { get; set; }
		public bool NotificationChat_2 { get; set; } = true;
		public bool NotificationChat_3 { get; set; } = true;
		public bool NotificationChat_4 { get; set; }

		void DeletePicture()
		{
			if (!String.IsNullOrEmpty(AvatarImageLink))
			{
				AvatarImageLink = null;
			}
			else
			{
				return;
			}
		}

		IList<IBrowserFile> files = new List<IBrowserFile>();
		private void UploadFiles(IBrowserFile file)
		{
			files.Add(file);
			//TODO upload the files to the server
		}

		void SaveChanges(string message, Severity severity)
		{
			Snackbar.Add(message, severity, config =>
			{
				config.ShowCloseIcon = false;
			});
		}

		MudForm form;
		MudTextField<string> pwField1;


		[CascadingParameter]
		private Task<AuthenticationState> authenticationStateTask { get; set; }

		[Inject]
		public ISnackbar Snackbar { get; set; }

		[Inject]
		public NavigationManager NavigationManager { get; set; }
		[Inject]
		public IMediator Mediator { get; set; }

		public AuthenticationState State { get; set; }

		protected override async Task OnInitializedAsync()
		{
			var result = await Mediator.Send(new RetrieveMyUserProfileQuery());
			State = await authenticationStateTask;
		}

		private void NavigateWithReload(string url) => NavigationManager.NavigateTo(url, true);
	}
}