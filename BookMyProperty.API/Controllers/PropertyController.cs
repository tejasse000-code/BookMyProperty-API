using BookMyProperty.API.Models;
using BookMyProperty.Application.DTOs;
using BookMyProperty.Application.Features.Properties.Commands;
using BookMyProperty.Application.Features.Properties.Queries;
using BookMyProperty.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookMyProperty.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PropertyController : ControllerBase
{
    private readonly IPropertyRepository _repository;
    private readonly IPropertyQueryRepository _queryRepository;
    private readonly ILogger<PropertyController> _logger;

    public PropertyController(IPropertyRepository repository, IPropertyQueryRepository queryRepository, ILogger<PropertyController> logger)
    {
        _repository = repository;
        _queryRepository = queryRepository;
        _logger = logger;
    }

    /// <summary>
    /// Get all properties with pagination
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<PaginatedResult<PropertyDto>>>> GetAllProperties(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = new GetAllPropertiesQuery { PageNumber = pageNumber, PageSize = pageSize };
            var handler = new GetAllPropertiesQueryHandler(_queryRepository);
            var result = await handler.HandleAsync(query);

            return Ok(new ApiResponse<PaginatedResult<PropertyDto>>
            {
                Success = true,
                Message = "Properties retrieved successfully",
                Data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving properties: {ex.Message}");
            return BadRequest(new ApiResponse<PaginatedResult<PropertyDto>>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Get property by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<PropertyDto>>> GetPropertyById([FromRoute] int id)
    {
        try
        {
            var query = new GetPropertyByIdQuery { Id = id };
            var handler = new GetPropertyByIdQueryHandler(_queryRepository);
            var result = await handler.HandleAsync(query);

            if (result == null)
                return NotFound(new ApiResponse<PropertyDto>
                {
                    Success = false,
                    Message = "Property not found"
                });

            return Ok(new ApiResponse<PropertyDto>
            {
                Success = true,
                Message = "Property retrieved successfully",
                Data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving property: {ex.Message}");
            return BadRequest(new ApiResponse<PropertyDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Search properties with filters
    /// </summary>
    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<PaginatedResult<PropertyDto>>>> SearchProperties(
        [FromQuery] string? location,
        [FromQuery] int? propertyType,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = new SearchPropertiesQuery
            {
                Location = location,
                PropertyType = propertyType,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var handler = new SearchPropertiesQueryHandler(_queryRepository);
            var result = await handler.HandleAsync(query);

            return Ok(new ApiResponse<PaginatedResult<PropertyDto>>
            {
                Success = true,
                Message = "Properties searched successfully",
                Data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error searching properties: {ex.Message}");
            return BadRequest(new ApiResponse<PaginatedResult<PropertyDto>>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Create a new property
    /// </summary>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<PropertyDto>>> CreateProperty([FromBody] CreatePropertyDto createPropertyDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<PropertyDto> { Success = false, Message = "Invalid input" });

        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userId == 0)
                return Unauthorized(new ApiResponse<PropertyDto> { Success = false, Message = "User not authenticated" });

            var command = new CreatePropertyCommand { UserId = userId, Dto = createPropertyDto };
            var handler = new CreatePropertyCommandHandler(_repository);
            var result = await handler.HandleAsync(command);

            return CreatedAtAction(nameof(GetPropertyById), new { id = result.Id },
                new ApiResponse<PropertyDto>
                {
                    Success = true,
                    Message = "Property created successfully",
                    Data = result
                });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating property: {ex.Message}");
            return BadRequest(new ApiResponse<PropertyDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Update a property
    /// </summary>
    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<PropertyDto>>> UpdateProperty(
        [FromRoute] int id,
        [FromBody] UpdatePropertyDto updatePropertyDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<PropertyDto> { Success = false, Message = "Invalid input" });

        try
        {
            var command = new UpdatePropertyCommand { Id = id, Dto = updatePropertyDto };
            var handler = new UpdatePropertyCommandHandler(_repository);
            var result = await handler.HandleAsync(command);

            if (result == null)
                return NotFound(new ApiResponse<PropertyDto>
                {
                    Success = false,
                    Message = "Property not found"
                });

            return Ok(new ApiResponse<PropertyDto>
            {
                Success = true,
                Message = "Property updated successfully",
                Data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error updating property: {ex.Message}");
            return BadRequest(new ApiResponse<PropertyDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Delete a property
    /// </summary>
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<bool>>> DeleteProperty([FromRoute] int id)
    {
        try
        {
            var command = new DeletePropertyCommand { PropertyId = id };
            var handler = new DeletePropertyCommandHandler(_repository);
            var result = await handler.HandleAsync(command);

            if (!result)
                return NotFound(new ApiResponse<bool>
                {
                    Success = false,
                    Message = "Property not found"
                });

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "Property deleted successfully",
                Data = true
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting property: {ex.Message}");
            return BadRequest(new ApiResponse<bool>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }
}
