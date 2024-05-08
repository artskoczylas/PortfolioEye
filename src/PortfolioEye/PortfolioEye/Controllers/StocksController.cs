using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioEye.Application.Features.Portfolios.Queries;
using PortfolioEye.Application.Features.Stocks.Queries;
using PortfolioEye.Infrastructure.Interfaces;

namespace PortfolioEye.Controllers;

[Authorize]
[Route("api/[controller]")]
public class StocksController(IMediator mediator) : ControllerBase
{
    [HttpGet("Search")]
    public async Task<IActionResult> SearchForTickers([FromQuery] string query)
    {
        var result = await mediator.Send(new SearchTickersQuery(query));
        if (result.IsSuccess)
            return Ok(result);
        return result.ErrorCode switch
        {
            _ => BadRequest(result)
        };
    }
}