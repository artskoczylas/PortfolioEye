using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioEye.Application.Features.Transactions.Commands;
using PortfolioEye.Application.Features.Transactions.Queries;
using PortfolioEye.Interfaces;

namespace PortfolioEye.Controllers;

[Authorize]
[Route("api/[controller]")]
public class TransactionsController (IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Retrieve(Guid id)
    {
        var result = await mediator.Send(new RetrieveTransactionByIdQuery(id));
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
        var result = await mediator.Send(new DeleteTransactionByIdCommand(id));
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
        
        var result = await mediator.Send(new RetrieveTransactionsByUserIdQuery(currentUser.Id));
        if (result.IsSuccess)
            return Ok(result);
        return result.ErrorCode switch
        {
            _ => BadRequest(result)
        };
    }
    
    [HttpPost("My/Stocks")]
    public async Task<IActionResult> CreateNew([FromServices] ICurrentUserAccessor userAccessor,
        [FromBody] AddStockTransactionCommand command)
    {
        var currentUser = userAccessor.Get();
        if (currentUser == null)
            return Unauthorized();
        
        var result = await mediator.Send(new AddStockTransactionForUserCommand(currentUser.Id, command));
        if (result.IsSuccess)
            return Ok(result);
        return result.ErrorCode switch
        {
            _ => BadRequest(result)
        };
    }
    
    [HttpPut("My")]
    public async Task<IActionResult> Update([FromServices] ICurrentUserAccessor userAccessor,
        [FromBody] EditTransactionCommand command)
    {
        var currentUser = userAccessor.Get();
        if (currentUser == null)
            return Unauthorized();
        
        var result = await mediator.Send(new EditTransactionForUserCommand(currentUser.Id, command));
        if (result.IsSuccess)
            return Ok(result);
        return result.ErrorCode switch
        {
            _ => BadRequest(result)
        };
    }
}