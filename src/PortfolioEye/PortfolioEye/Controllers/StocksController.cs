using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioEye.Application;
using PortfolioEye.Application.Features.Stocks.Queries;

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
    
    [HttpGet("Details/{ticker}")]
    public async Task<IActionResult> StockDetails(string ticker)
    {
        var result = await mediator.Send(new GetStockDetailsQuery(ticker));
        if (result.IsSuccess)
            return Ok(result);
        return result.ErrorCode switch
        {
            WellKnown.ErrorCodes.NotFound => NotFound(result),
            _ => BadRequest(result)
        };
    }
    
    [HttpGet("History/{ticker}")]
    public async Task<IActionResult> StockPriceHistory(string ticker, [FromQuery] DateOnly from, [FromQuery] DateOnly to)
    {
        var result = await mediator.Send(new GetStockHistoryQuery(ticker, from, to));
        if (result.IsSuccess)
            return Ok(result);
        return result.ErrorCode switch
        {
            WellKnown.ErrorCodes.NotFound => NotFound(result),
            _ => BadRequest(result)
        };
    }
}