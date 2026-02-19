using BookMyProperty.API.Models;
using BookMyProperty.Application.DTOs;
using BookMyProperty.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookMyProperty.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactInquiryController : ControllerBase
{
    private readonly IContactInquiryRepository _repository;
    private readonly ILogger<ContactInquiryController> _logger;

    public ContactInquiryController(IContactInquiryRepository repository, ILogger<ContactInquiryController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Get all contact inquiries
    /// </summary>
    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<IEnumerable<ContactInquiryDto>>>> GetAll()
    {
        try
        {
            var inquiries = await _repository.GetAllAsync();
            return Ok(new ApiResponse<IEnumerable<ContactInquiryDto>>
            {
                Success = true,
                Message = "Contact inquiries retrieved successfully",
                Data = inquiries
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving contact inquiries: {ex.Message}");
            return BadRequest(new ApiResponse<IEnumerable<ContactInquiryDto>>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Get contact inquiry by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<ContactInquiryDto>>> GetById([FromRoute] int id)
    {
        try
        {
            var inquiry = await _repository.GetByIdAsync(id);
            if (inquiry == null)
                return NotFound(new ApiResponse<ContactInquiryDto>
                {
                    Success = false,
                    Message = "Contact inquiry not found"
                });

            return Ok(new ApiResponse<ContactInquiryDto>
            {
                Success = true,
                Message = "Contact inquiry retrieved successfully",
                Data = inquiry
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving contact inquiry: {ex.Message}");
            return BadRequest(new ApiResponse<ContactInquiryDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Get inquiries for a specific property
    /// </summary>
    [Authorize]
    [HttpGet("property/{propertyId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponse<IEnumerable<ContactInquiryDto>>>> GetByPropertyId([FromRoute] int propertyId)
    {
        try
        {
            var inquiries = await _repository.GetByPropertyIdAsync(propertyId);
            return Ok(new ApiResponse<IEnumerable<ContactInquiryDto>>
            {
                Success = true,
                Message = "Property inquiries retrieved successfully",
                Data = inquiries
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving property inquiries: {ex.Message}");
            return BadRequest(new ApiResponse<IEnumerable<ContactInquiryDto>>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Create a new contact inquiry
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<ContactInquiryDto>>> Create([FromBody] CreateContactInquiryDto createDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<ContactInquiryDto> { Success = false, Message = "Invalid input" });

        try
        {
            var inquiry = await _repository.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = inquiry.Id },
                new ApiResponse<ContactInquiryDto>
                {
                    Success = true,
                    Message = "Contact inquiry created successfully",
                    Data = inquiry
                });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating contact inquiry: {ex.Message}");
            return BadRequest(new ApiResponse<ContactInquiryDto>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }

    /// <summary>
    /// Delete a contact inquiry
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
                    Message = "Contact inquiry not found"
                });

            return Ok(new ApiResponse<bool>
            {
                Success = true,
                Message = "Contact inquiry deleted successfully",
                Data = true
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting contact inquiry: {ex.Message}");
            return BadRequest(new ApiResponse<bool>
            {
                Success = false,
                Message = ex.Message
            });
        }
    }
}
