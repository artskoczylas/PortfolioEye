using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioEye.Application.Features.Accounts.Commands;
using PortfolioEye.Application.Features.Accounts.Queries;
using PortfolioEye.Interfaces;

namespace PortfolioEye.Controllers;

[Authorize]
[Route("api/[controller]")]
public class AccountsController (IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Retrieve(Guid id)
    {
        var result = await mediator.Send(new RetrieveAccountByIdQuery(id));
        if (result.IsSuccess)
            return Ok(result);
        return result.ErrorCode switch
        {
            _ => BadRequest(result)
        };
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await mediator.Send(new DeleteAccountByIdCommand(id));
        if (result.IsSuccess)
            return Ok(result);
        return result.ErrorCode switch
        {
            _ => BadRequest(result)
        };
    }
    
    [HttpGet("My")]
    public async Task<IActionResult> RetrieveAll([FromServices] ICurrentUserAccessor userAccessor)
    {
        var currentUser = userAccessor.Get();
        if (currentUser == null)
            return Unauthorized();
        
        var result = await mediator.Send(new RetrieveAccountsByUserId(currentUser.Id));
        if (result.IsSuccess)
            return Ok(result);
        return result.ErrorCode switch
        {
            _ => BadRequest(result)
        };
    }
    
    [HttpPost("My")]
    public async Task<IActionResult> CreateNew([FromServices] ICurrentUserAccessor userAccessor,
        [FromBody] AddAccountCommand command)
    {
        var currentUser = userAccessor.Get();
        if (currentUser == null)
            return Unauthorized();
        
        var result = await mediator.Send(new AddAccountForUserCommand(currentUser.Id, command));
        if (result.IsSuccess)
            return Ok(result);
        return result.ErrorCode switch
        {
            _ => BadRequest(result)
        };
    }
    
    [HttpPut("My")]
    public async Task<IActionResult> Update([FromServices] ICurrentUserAccessor userAccessor,
        [FromBody] EditAccountCommand command)
    {
        var currentUser = userAccessor.Get();
        if (currentUser == null)
            return Unauthorized();
        
        var result = await mediator.Send(new EditAccountForUserCommand(currentUser.Id, command));
        if (result.IsSuccess)
            return Ok(result);
        return result.ErrorCode switch
        {
            _ => BadRequest(result)
        };
    }
}