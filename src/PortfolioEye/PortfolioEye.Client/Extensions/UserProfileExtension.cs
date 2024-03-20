using PortfolioEye.Application.Features.Users;

namespace PortfolioEye.Client.Extensions;

public static class UserProfileExtension
{
    public static string GetInitials(this UserProfileResponse profile)
    {
        if (!string.IsNullOrEmpty(profile.FirstName) && !string.IsNullOrEmpty(profile.LastName))
            return $"{profile.FirstName[0]}{profile.LastName[0]}".ToUpper();
        return "NA";
    }
}