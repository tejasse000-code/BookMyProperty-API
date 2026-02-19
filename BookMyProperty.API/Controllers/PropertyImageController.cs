using BookMyProperty.API.Models;
using BookMyProperty.Application.DTOs;
using BookMyProperty.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMyProperty.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyImageController : ControllerBase
{
    private readonly IPropertyImageRepository _repository;
    private readonly ILogger<PropertyImageController> _logger;

    public PropertyImageController(IPropertyImageRepository repository, ILogger<PropertyImageController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Get images for a property
    /// </summary>
    [HttpGet("property/{propertyId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<PropertyImageDto>>>> GetByPropertyId([FromRoute] int propertyId)
    {
        try
        {
            var images = await _repository.GetByPropertyIdAsync(propertyId);
            return Ok(new ApiResponse<IEnumerable<PropertyImageDto>>
            {
                Success = true,
                Message = "Property images retrieved successfully",
                Data = images
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving property images: {ex.Message}");
            return BadRequest(new ApiResponse<IEnumerable<PropertyImageDto>>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Get image by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<PropertyImageDto>>> GetById([FromRoute] int id)
    {
        try
        {
            var image = await _repository.GetByIdAsync(id);
            if (image == null)
                return NotFound(new ApiResponse<PropertyImageDto>
                {
                    Success = false,
                    Message = "Property image not found"
                });

            return Ok(new ApiResponse<PropertyImageDto>
            {
                Success = true,
                Message = "Property image retrieved successfully",
                Data = image
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving property image: {ex.Message}");
            return BadRequest(new ApiResponse<PropertyImageDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Upload a property image
    /// </summary>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<PropertyImageDto>>> Create([FromBody] CreatePropertyImageDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<PropertyImageDto> { Success = false, Message = "Invalid input" });

        try
        {
            var image = await _repository.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = image.Id },
                new ApiResponse<PropertyImageDto>
                {
                    Success = true,
                    Message = "Property image created successfully",
                    Data = image
                });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating property image: {ex.Message}");
            return BadRequest(new ApiResponse<PropertyImageDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Update a property image
    /// </summary>
    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<PropertyImageDto>>> Update(
        [FromRoute] int id,
        [FromBody] UpdatePropertyImageDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<PropertyImageDto> { Success = false, Message = "Invalid input" });

        try
        {
            var image = await _repository.UpdateAsync(id, updateDto);
            if (image == null)
                return NotFound(new ApiResponse<PropertyImageDto>
                {
                    Success = false,
                    Message = "Property image not found"
                });

            return Ok(new ApiResponse<PropertyImageDto>
            {
                Success = true,
                Message = "Property image updated successfully",
                Data = image
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error updating property image: {ex.Message}");
            return BadRequest(new ApiResponse<PropertyImageDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Delete a property image
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
                    Message = "Property image not found"
                });

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "Property image deleted successfully",
                Data = true
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting property image: {ex.Message}");
            return BadRequest(new ApiResponse<bool>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }
}
