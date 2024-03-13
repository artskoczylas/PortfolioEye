using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioEye.Application.Features.Users;
using PortfolioEye.Interfaces;
using PortfolioEye.Services;

namespace PortfolioEye.Controllers;

[Authorize]
[Route("api/[controller]")]
public class MeController(IMediator mediator) : ControllerBase
{
    [HttpGet("Profile")]
    public async Task<IActionResult> Get([FromServices] ICurrentUserAccessor userAccessor)
    {
        var currentUser = userAccessor.Get();
        if (currentUser == null)
            return Unauthorized();

        var user = await mediator.Send(new RetrieveUserProfileByIdQuery(currentUser.Id));
        if (user.IsSuccess)
            return Ok(user);
        return user.ErrorCode switch
        {
            404 => NotFound(user),
            _ => BadRequest(user)
        };
    }
}