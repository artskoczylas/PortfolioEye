using System.Security.Claims;
using PortfolioEye.Application;
using PortfolioEye.Interfaces;

namespace PortfolioEye.Services;

public class CurrentUserAccessor(IHttpContextAccessor contextAccessor) : ICurrentUserAccessor
{
    public CurrentUser? Get()
    {
        if (contextAccessor.HttpContext?.User == null)
            return null;
        
        var principal = contextAccessor.HttpContext.User;
        var userIdRaw = principal.FindFirstValue(WellKnown.Claims.Id);
        if (!Guid.TryParse(userIdRaw, out var userId))
            return null;
        
        var userName = principal.FindFirstValue(WellKnown.Claims.Name);
        var userEmail = principal.FindFirstValue(WellKnown.Claims.Email);

        return new CurrentUser(userId, userName, userEmail);
    }
}