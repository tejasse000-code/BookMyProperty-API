using BookMyProperty.API.Models;
using BookMyProperty.Application.DTOs;
using BookMyProperty.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookMyProperty.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WishlistController : ControllerBase
{
    private readonly IWishlistRepository _repository;
    private readonly ILogger<WishlistController> _logger;

    public WishlistController(IWishlistRepository repository, ILogger<WishlistController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Get user's wishlist
    /// </summary>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<IEnumerable<WishlistDto>>>> GetUserWishlist()
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
                return Unauthorized(new ApiResponse<IEnumerable<WishlistDto>>
                {
                    Success = false,
                    Message = "User not authenticated"
                });

            var wishlist = await _repository.GetByUserIdAsync(userId);
            return Ok(new ApiResponse<IEnumerable<WishlistDto>>
            {
                Success = true,
                Message = "Wishlist retrieved successfully",
                Data = wishlist
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving wishlist: {ex.Message}");
            return BadRequest(new ApiResponse<IEnumerable<WishlistDto>>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Get wishlist item by ID
    /// </summary>
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<WishlistDto>>> GetById([FromRoute] int id)
    {
        try
        {
            var wishlist = await _repository.GetByIdAsync(id);
            if (wishlist == null)
                return NotFound(new ApiResponse<WishlistDto>
                {
                    Success = false,
                    Message = "Wishlist item not found"
                });

            return Ok(new ApiResponse<WishlistDto>
            {
                Success = true,
                Message = "Wishlist item retrieved successfully",
                Data = wishlist
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving wishlist item: {ex.Message}");
            return BadRequest(new ApiResponse<WishlistDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Add property to wishlist
    /// </summary>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<WishlistDto>>> AddToWishlist([FromBody] CreateWishlistDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<WishlistDto> { Success = false, Message = "Invalid input" });

        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
                return Unauthorized(new ApiResponse<WishlistDto>
                {
                    Success = false,
                    Message = "User not authenticated"
                });

            var exists = await _repository.CheckIfExistsAsync(userId, createDto.PropertyId);
            if (exists)
                return BadRequest(new ApiResponse<WishlistDto>
                {
                    Success = false,
                    Message = "Property already in wishlist"
                });

            var wishlist = await _repository.CreateAsync(userId, createDto);
            return CreatedAtAction(nameof(GetById), new { id = wishlist.Id },
                new ApiResponse<WishlistDto>
                {
                    Success = true,
                    Message = "Property added to wishlist",
                    Data = wishlist
                });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error adding to wishlist: {ex.Message}");
            return BadRequest(new ApiResponse<WishlistDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Remove property from wishlist
    /// </summary>
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<bool>>> RemoveFromWishlist([FromRoute] int id)
    {
        try
        {
            var result = await _repository.DeleteAsync(id);
            if (!result)
                return NotFound(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Wishlist item not found"
                });

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "Property removed from wishlist",
                Data = true
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error removing from wishlist: {ex.Message}");
            return BadRequest(new ApiResponse<bool>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }
}
