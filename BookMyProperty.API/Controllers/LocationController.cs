using BookMyProperty.API.Models;
using BookMyProperty.Application.DTOs;
using BookMyProperty.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMyProperty.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly ILocationRepository _repository;
    private readonly ILogger<LocationController> _logger;

    public LocationController(ILocationRepository repository, ILogger<LocationController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Get all locations
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<LocationDto>>>> GetAll()
    {
        try
        {
            var locations = await _repository.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<LocationDto>>
            {
                Success = true,
                Message = "Locations retrieved successfully",
                Data = locations
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving locations: {ex.Message}");
            return BadRequest(new ApiResponse<IEnumerable<LocationDto>>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Get location by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<LocationDto>>> GetById([FromRoute] int id)
    {
        try
        {
            var location = await _repository.GetByIdAsync(id);
            if (location == null)
                return NotFound(new ApiResponse<LocationDto>
                {
                    Success = false,
                    Message = "Location not found"
                });

            return Ok(new ApiResponse<LocationDto>
            {
                Success = true,
                Message = "Location retrieved successfully",
                Data = location
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving location: {ex.Message}");
            return BadRequest(new ApiResponse<LocationDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Create a new location
    /// </summary>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<LocationDto>>> Create([FromBody] CreateLocationDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<LocationDto> { Success = false, Message = "Invalid input" });

        try
        {
            var location = await _repository.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = location.Id },
                new ApiResponse<LocationDto>
                {
                    Success = true,
                    Message = "Location created successfully",
                    Data = location
                });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating location: {ex.Message}");
            return BadRequest(new ApiResponse<LocationDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Update a location
    /// </summary>
    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<LocationDto>>> Update(
        [FromRoute] int id,
        [FromBody] UpdateLocationDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<LocationDto> { Success = false, Message = "Invalid input" });

        try
        {
            var location = await _repository.UpdateAsync(id, updateDto);
            if (location == null)
                return NotFound(new ApiResponse<LocationDto>
                {
                    Success = false,
                    Message = "Location not found"
                });

            return Ok(new ApiResponse<LocationDto>
            {
                Success = true,
                Message = "Location updated successfully",
                Data = location
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error updating location: {ex.Message}");
            return BadRequest(new ApiResponse<LocationDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Delete a location
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
                    Message = "Location not found"
                });

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "Location deleted successfully",
                Data = true
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting location: {ex.Message}");
            return BadRequest(new ApiResponse<bool>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }
}
