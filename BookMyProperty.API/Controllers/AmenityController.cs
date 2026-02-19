using BookMyProperty.API.Models;
using BookMyProperty.Application.DTOs;
using BookMyProperty.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMyProperty.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AmenityController : ControllerBase
{
    private readonly IAmenityRepository _repository;
    private readonly ILogger<AmenityController> _logger;

    public AmenityController(IAmenityRepository repository, ILogger<AmenityController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Get all amenities
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<AmenityDto>>>> GetAll()
    {
        try
        {
            var amenities = await _repository.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<AmenityDto>>
            {
                Success = true,
                Message = "Amenities retrieved successfully",
                Data = amenities
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving amenities: {ex.Message}");
            return BadRequest(new ApiResponse<IEnumerable<AmenityDto>>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Get amenity by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<AmenityDto>>> GetById([FromRoute] int id)
    {
        try
        {
            var amenity = await _repository.GetByIdAsync(id);
            if (amenity == null)
                return NotFound(new ApiResponse<AmenityDto>
                {
                    Success = false,
                    Message = "Amenity not found"
                });

            return Ok(new ApiResponse<AmenityDto>
            {
                Success = true,
                Message = "Amenity retrieved successfully",
                Data = amenity
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving amenity: {ex.Message}");
            return BadRequest(new ApiResponse<AmenityDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Create a new amenity
    /// </summary>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<AmenityDto>>> Create([FromBody] CreateAmenityDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<AmenityDto> { Success = false, Message = "Invalid input" });

        try
        {
            var amenity = await _repository.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = amenity.Id },
                new ApiResponse<AmenityDto>
                {
                    Success = true,
                    Message = "Amenity created successfully",
                    Data = amenity
                });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating amenity: {ex.Message}");
            return BadRequest(new ApiResponse<AmenityDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Update an amenity
    /// </summary>
    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<AmenityDto>>> Update(
        [FromRoute] int id,
        [FromBody] UpdateAmenityDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<AmenityDto> { Success = false, Message = "Invalid input" });

        try
        {
            var amenity = await _repository.UpdateAsync(id, updateDto);
            if (amenity == null)
                return NotFound(new ApiResponse<AmenityDto>
                {
                    Success = false,
                    Message = "Amenity not found"
                });

            return Ok(new ApiResponse<AmenityDto>
            {
                Success = true,
                Message = "Amenity updated successfully",
                Data = amenity
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error updating amenity: {ex.Message}");
            return BadRequest(new ApiResponse<AmenityDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Delete an amenity
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
                    Message = "Amenity not found"
                });

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "Amenity deleted successfully",
                Data = true
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting amenity: {ex.Message}");
            return BadRequest(new ApiResponse<bool>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }
}
