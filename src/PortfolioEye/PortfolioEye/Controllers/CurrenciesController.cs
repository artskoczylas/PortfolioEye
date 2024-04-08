using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Currencies.Commands;
using PortfolioEye.Application.Features.Currencies.Queries;
using PortfolioEye.Application.Features.Users.Commands;

namespace PortfolioEye.Controllers;

[Authorize]
[Route("api/[controller]")]
public class CurrenciesController (IMediator mediator) : ControllerBase
{
    [HttpGet("Active")]
    public async Task<IActionResult> RetrieveActive()
    {
        var result = await mediator.Send(new RetrieveActiveCurrenciesQuery());
        if (result.IsSuccess)
            return Ok(result);
        return result.ErrorCode switch
        {
            _ => BadRequest(result)
        };
    }
    
    [HttpGet]
    public async Task<IActionResult> RetrieveAll()
    {
        var result = await mediator.Send(new RetrieveAllCurrenciesQuery());
        if (result.IsSuccess)
            return Ok(result);
        return result.ErrorCode switch
        {
            _ => BadRequest(result)
        };
    }
    
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddCurrencyCommand command)
    {
        var result = await mediator.Send(command);
        if (result.IsSuccess)
            return Ok(result);
        return result.ErrorCode switch
        {
            _ => BadRequest(result)
        };
    }
}