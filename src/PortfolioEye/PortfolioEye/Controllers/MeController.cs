using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioEye.Application.Features.Users;
using PortfolioEye.Application.Features.Users.Commands;
using PortfolioEye.Application.Features.Users.Queries;
using PortfolioEye.Interfaces;
using PortfolioEye.Services;

namespace PortfolioEye.Controllers;

[Authorize]
[Route("api/[controller]")]
public class MeController(IMediator mediator) : ControllerBase
{
    [HttpGet("Profile")]
    public async Task<IActionResult> GetProfile([FromServices] ICurrentUserAccessor userAccessor)
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
    
    [HttpPut("Profile")]
    public async Task<IActionResult> UpdateProfile([FromServices] ICurrentUserAccessor userAccessor, UpdateProfileCommand command)
    {
        var currentUser = userAccessor.Get();
        if (currentUser == null)
            return Unauthorized();

        var user = await mediator.Send(new UpdateUserProfileByIdCommand(currentUser.Id, command));
        if (user.IsSuccess)
            return Ok();
        return user.ErrorCode switch
        {
            404 => NotFound(user),
            _ => BadRequest(user)
        };
    }
}