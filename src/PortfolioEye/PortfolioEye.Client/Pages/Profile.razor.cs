using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Net;
using System.Text.RegularExpressions;

namespace PortfolioEye.Client.Pages
{
	public partial class Profile
	{
		public string AvatarImageLink { get; set; }
		public string AvatarButtonText
		{
			get { return (AvatarImageLink == null) ? "Dodaj zdjêcie" : "Usuñ zdjêcie"; }
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

		private IEnumerable<string> PasswordStrength(string pw)
		{
			if (string.IsNullOrWhiteSpace(pw))
			{
				yield return "Password is required!";
				yield break;
			}
			if (pw.Length < 8)
				yield return "Password must be at least of length 8";
			if (!Regex.IsMatch(pw, @"[A-Z]"))
				yield return "Password must contain at least one capital letter";
			if (!Regex.IsMatch(pw, @"[a-z]"))
				yield return "Password must contain at least one lowercase letter";
			if (!Regex.IsMatch(pw, @"[0-9]"))
				yield return "Password must contain at least one digit";
		}

		private string PasswordMatch(string arg)
		{
			if (pwField1.Value != arg)
				return "Passwords don't match";
			return null;
		}

		[CascadingParameter]
		private Task<AuthenticationState> authenticationStateTask { get; set; }

		[Inject]
		ISnackbar Snackbar { get; set; }

		public AuthenticationState State { get; set; }

		protected async override Task OnInitializedAsync()
		{
			State = await authenticationStateTask;
		}
	}
}