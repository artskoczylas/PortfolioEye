using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioEye.Application.Features.Portfolios.Commands;
using PortfolioEye.Application.Features.Portfolios.Queries;
using PortfolioEye.Interfaces;

namespace PortfolioEye.Controllers;

[Authorize]
[Route("api/[controller]")]
public class PortfoliosController (IMediator mediator) : ControllerBase
{
    [HttpGet("My")]
    public async Task<IActionResult> RetrieveAll([FromServices] ICurrentUserAccessor userAccessor)
    {
        var currentUser = userAccessor.Get();
        if (currentUser == null)
            return Unauthorized();
        
        var result = await mediator.Send(new RetrievePortfoliosByUserId(currentUser.Id));
        if (result.IsSuccess)
            return Ok(result);
        return result.ErrorCode switch
        {
            _ => BadRequest(result)
        };
    }
    
    [HttpPost("My")]
    public async Task<IActionResult> CreateNew([FromServices] ICurrentUserAccessor userAccessor,
        [FromBody] AddPortfolioCommand command)
    {
        var currentUser = userAccessor.Get();
        if (currentUser == null)
            return Unauthorized();
        
        var result = await mediator.Send(new AddPortfolioForUserCommand(currentUser.Id, command));
        if (result.IsSuccess)
            return Ok(result);
        return result.ErrorCode switch
        {
            _ => BadRequest(result)
        };
    }
    
    [HttpPut("My")]
    public async Task<IActionResult> Update([FromServices] ICurrentUserAccessor userAccessor,
        [FromBody] EditPortfolioCommand command)
    {
        var currentUser = userAccessor.Get();
        if (currentUser == null)
            return Unauthorized();
        
        var result = await mediator.Send(new EditPortfolioForUserCommand(currentUser.Id, command));
        if (result.IsSuccess)
            return Ok(result);
        return result.ErrorCode switch
        {
            _ => BadRequest(result)
        };
    }
}