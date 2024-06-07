using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Bonds.Queries;
using PortfolioEye.Application.Features.Stocks.Queries;

namespace PortfolioEye.Controllers;

[Authorize]
[Route("api/[controller]")]
public class BondsController(IMediator mediator) : ControllerBase
{
    [HttpGet("Search")]
    public async Task<IActionResult> SearchForTickers([FromQuery] BondKind kind, [FromQuery] DateOnly buyDate)
    {
        var result = await mediator.Send(new RetrieveBondEmissionByBuyDateQuery(kind, buyDate));
        if (result.IsSuccess)
            return Ok(result);
        return result.ErrorCode switch
        {
            _ => BadRequest(result)
        };
    }
}