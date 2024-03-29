﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioEye.Application.Features.Users;
using PortfolioEye.Application.Features.Users.Queries;

namespace PortfolioEye.Controllers;

[Authorize]
[Route("api/[controller]")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var user = await mediator.Send(new RetrieveUserProfileByIdQuery(id));
        if (user.IsSuccess)
            return Ok(user);
        return user.ErrorCode switch
        {
            404 => NotFound(user),
            _ => BadRequest(user)
        };
    }
    [HttpGet("{id:guid}/Photo")]
    public IActionResult GetProfilePhoto(Guid id)
    {
        var photosDirectory = new DirectoryInfo("Data/ProfilePhotos");
        var photo = Path.Combine(photosDirectory.ToString(), $"{id}");
        var stream = new FileStream(photo, FileMode.Open);
        var result = new FileStreamResult(stream, "image/png");
        result.FileDownloadName = $"{id}";
        return result;
    }
}