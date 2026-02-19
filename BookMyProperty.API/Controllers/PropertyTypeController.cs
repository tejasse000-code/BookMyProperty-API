using BookMyProperty.API.Models;
using BookMyProperty.Application.DTOs;
using BookMyProperty.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMyProperty.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyTypeController : ControllerBase
{
    private readonly IPropertyTypeRepository _repository;
    private readonly ILogger<PropertyTypeController> _logger;

    public PropertyTypeController(IPropertyTypeRepository repository, ILogger<PropertyTypeController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Get all property types
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<PropertyTypeDto>>>> GetAll()
    {
        try
        {
            var propertyTypes = await _repository.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<PropertyTypeDto>>
            {
                Success = true,
                Message = "Property types retrieved successfully",
                Data = propertyTypes
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving property types: {ex.Message}");
            return BadRequest(new ApiResponse<IEnumerable<PropertyTypeDto>>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Get property type by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<PropertyTypeDto>>> GetById([FromRoute] int id)
    {
        try
        {
            var propertyType = await _repository.GetByIdAsync(id);
            if (propertyType == null)
                return NotFound(new ApiResponse<PropertyTypeDto>
                {
                    Success = false,
                    Message = "Property type not found"
                });

            return Ok(new ApiResponse<PropertyTypeDto>
            {
                Success = true,
                Message = "Property type retrieved successfully",
                Data = propertyType
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving property type: {ex.Message}");
            return BadRequest(new ApiResponse<PropertyTypeDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Create a new property type
    /// </summary>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<PropertyTypeDto>>> Create([FromBody] CreatePropertyTypeDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<PropertyTypeDto> { Success = false, Message = "Invalid input" });

        try
        {
            var propertyType = await _repository.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = propertyType.Id },
                new ApiResponse<PropertyTypeDto>
                {
                    Success = true,
                    Message = "Property type created successfully",
                    Data = propertyType
                });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating property type: {ex.Message}");
            return BadRequest(new ApiResponse<PropertyTypeDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Update a property type
    /// </summary>
    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<PropertyTypeDto>>> Update(
        [FromRoute] int id,
        [FromBody] UpdatePropertyTypeDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<PropertyTypeDto> { Success = false, Message = "Invalid input" });

        try
        {
            var propertyType = await _repository.UpdateAsync(id, updateDto);
            if (propertyType == null)
                return NotFound(new ApiResponse<PropertyTypeDto>
                {
                    Success = false,
                    Message = "Property type not found"
                });

            return Ok(new ApiResponse<PropertyTypeDto>
            {
                Success = true,
                Message = "Property type updated successfully",
                Data = propertyType
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error updating property type: {ex.Message}");
            return BadRequest(new ApiResponse<PropertyTypeDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Delete a property type
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
                    Message = "Property type not found"
                });

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "Property type deleted successfully",
                Data = true
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting property type: {ex.Message}");
            return BadRequest(new ApiResponse<bool>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }
}
