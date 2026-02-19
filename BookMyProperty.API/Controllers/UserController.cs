using BookMyProperty.API.Models;
using BookMyProperty.Application.DTOs;
using BookMyProperty.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookMyProperty.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserRepository repository, ILogger<UserController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Get all users (Admin only)
    /// </summary>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<IEnumerable<UserDto>>>> GetAll()
    {
        try
        {
            var users = await _repository.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<UserDto>>
            {
                Success = true,
                Message = "Users retrieved successfully",
                Data = users
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving users: {ex.Message}");
            return BadRequest(new ApiResponse<IEnumerable<UserDto>>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Get user by ID
    /// </summary>
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<UserDto>>> GetById([FromRoute] int id)
    {
        try
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
                return NotFound(new ApiResponse<UserDto>
                {
                    Success = false,
                    Message = "User not found"
                });

            return Ok(new ApiResponse<UserDto>
            {
                Success = true,
                Message = "User retrieved successfully",
                Data = user
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving user: {ex.Message}");
            return BadRequest(new ApiResponse<UserDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Get current user profile
    /// </summary>
    [Authorize]
    [HttpGet("profile/me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<UserDto>>> GetCurrentUser()
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
                return Unauthorized(new ApiResponse<UserDto>
                {
                    Success = false,
                    Message = "User not authenticated"
                });

            var user = await _repository.GetByIdAsync(userId);
            if (user == null)
                return NotFound(new ApiResponse<UserDto>
                {
                    Success = false,
                    Message = "User not found"
                });

            return Ok(new ApiResponse<UserDto>
            {
                Success = true,
                Message = "User profile retrieved successfully",
                Data = user
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving user profile: {ex.Message}");
            return BadRequest(new ApiResponse<UserDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Delete a user (Admin only)
    /// </summary>
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<bool>>> Delete([FromRoute] int id)
    {
        try
        {
            var result = await _repository.DeleteAsync(id);
            if (!result)
                return NotFound(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "User not found"
                });

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "User deleted successfully",
                Data = true
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting user: {ex.Message}");
            return BadRequest(new ApiResponse<bool>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }
}
