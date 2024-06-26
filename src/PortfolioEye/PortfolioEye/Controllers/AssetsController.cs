﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioEye.Application.Features.Assets.Queries;
using PortfolioEye.Interfaces;

namespace PortfolioEye.Controllers;

[Authorize]
[Route("api/[controller]")]
public class AssetsController (IMediator mediator) : ControllerBase
{
    [HttpGet("My")]
    public async Task<IActionResult> RetrieveAll([FromServices] ICurrentUserAccessor userAccessor)
    {
        var currentUser = userAccessor.Get();
        if (currentUser == null)
            return Unauthorized();
        
        var result = await mediator.Send(new RetrieveAssetsByUserIdQuery(currentUser.Id));
        if (result.IsSuccess)
            return Ok(result);
        return result.ErrorCode switch
        {
            _ => BadRequest(result)
        };
    }
}