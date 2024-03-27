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

        private string AvatarButtonText => (AvatarImageLink == null)
            ? Localizer?.GetString("AddPhoto")
            : Localizer?.GetString("DeletePhoto");

        private Color AvatarButtonColor => (AvatarImageLink == null) ? Color.Primary : Color.Error;
        private string? FirstName { get; set; }
        private string? LastName { get; set; }
        private string? Email { get; set; }

        private async Task DeletePicture()
        {
            if (string.IsNullOrEmpty(AvatarImageLink) || CurrentUserManager == null)
                return;

            var result = await CurrentUserManager.DeletePhoto();
            if (result.IsSuccess)
            {
                AvatarImageLink = null;
                Snackbar?.Add(Localizer?.GetString("PhotoDeleted"), Severity.Success);
            }
            else
                Snackbar?.Add(Localizer?.GetString("CannotDeletePhoto"), Severity.Warning);
            StateHasChanged();
        }

        IList<IBrowserFile> files = new List<IBrowserFile>();

        private async Task UploadFiles(IBrowserFile file)
        {
            await using var stream = file.OpenReadStream();
            await using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            var base64 = Convert.ToBase64String(ms.ToArray());
            var result = await CurrentUserManager.UploadPhoto(base64);
            if (result.IsSuccess)
            {
                Snackbar?.Add(Localizer?.GetString("PhotoUploaded"), Severity.Success);
                await LoadProfile();
            }
            else
            {
                Snackbar?.Add(Localizer?.GetString("CannotUploadPhoto"), Severity.Success);
            }
            StateHasChanged();
        }


        [Inject] public ISnackbar? Snackbar { get; set; }

        [Inject] public NavigationManager? NavigationManager { get; set; }

        [Inject] public ICurrentUserManager? CurrentUserManager { get; set; }

        private UserProfileResponse? _profile;

        protected override async Task OnInitializedAsync()
        {
            await LoadProfile();
        }

        private async Task LoadProfile()
        {
            var result = await CurrentUserManager!.RetrieveMyProfile();
            if (result.IsSuccess)
            {
                _profile = result.Data;
                FirstName = result.Data!.FirstName;
                LastName = result.Data!.LastName;
                Email = result.Data!.Email;
                AvatarImageLink = result.Data!.PhotoUri;
            }
            else
                Snackbar?.Add(Localizer?.GetString("FailedToGetProfile"), Severity.Warning);
        }
        
        private async Task SaveChanges()
        {
            var profileCommand = new UpdateProfileCommand(FirstName, LastName);
            var result = await CurrentUserManager!.UpdateMyProfile(profileCommand);
            if (result.IsSuccess)
                Snackbar?.Add(Localizer?.GetString("ProfileSaved"), Severity.Info);
            else
                Snackbar?.Add(Localizer?.GetString("FailedToSaveProfile"), Severity.Warning);
        }

        private void NavigateWithReload(string url) => NavigationManager!.NavigateTo(url, true);
    }
}