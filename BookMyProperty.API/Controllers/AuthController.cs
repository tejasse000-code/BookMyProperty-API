using BookMyProperty.API.Models;
using BookMyProperty.Application.DTOs;
using BookMyProperty.Application.Features.Auth.Commands;
using BookMyProperty.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookMyProperty.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<AuthResponseDto> { Success = false, Message = "Invalid input" });

        try
        {
            var command = new RegisterCommand { Dto = registerDto };
            var handler = new RegisterCommandHandler(_authService);
            var result = await handler.HandleAsync(command);

            if (!result.IsSuccess)
                return BadRequest(new ApiResponse<AuthResponseDto> { Success = false, Message = result.Message });

            return Ok(new ApiResponse<AuthResponseDto>
            {
                Success = true,
                Message = "Registration successful",
                Data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Registration error: {ex.Message}");
            return BadRequest(new ApiResponse<AuthResponseDto> { Success = false, Message = ex.Message });
        }
    }

    /// <summary>
    /// Login user
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody] AuthDto authDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<AuthResponseDto> { Success = false, Message = "Invalid input" });

        try
        {
            var command = new LoginCommand { Email = authDto.Email, Password = authDto.Password };
            var handler = new LoginCommandHandler(_authService);
            var result = await handler.HandleAsync(command);

            if (!result.IsSuccess)
                return Unauthorized(new ApiResponse<AuthResponseDto> { Success = false, Message = result.Message });

            return Ok(new ApiResponse<AuthResponseDto>
            {
                Success = true,
                Message = "Login successful",
                Data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Login error: {ex.Message}");
            return Unauthorized(new ApiResponse<AuthResponseDto> { Success = false, Message = ex.Message });
        }
    }
}
