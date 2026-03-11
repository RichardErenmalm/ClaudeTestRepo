using ClaudeTestRepo.Application.Commands.Auth;
using ClaudeTestRepo.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClaudeTestRepo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto dto)
    {
        var result = await _mediator.Send(new RegisterCommand(dto));
        if (result == null) return Conflict("Username already exists.");
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
    {
        var result = await _mediator.Send(new LoginCommand(dto));
        if (result == null) return Unauthorized("Invalid username or password.");
        return Ok(result);
    }
}
