using AutoMapper;
using BookMyProperty.Application.DTOs;
using BookMyProperty.Application.Features.Properties.Commands;
using BookMyProperty.Application.Features.Properties.Queries;
using BookMyProperty.Domain.Entities;
using BookMyProperty.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookMyProperty.Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository, IPropertyQueryRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PropertyRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PropertyDto> CreatePropertyAsync(int userId, CreatePropertyDto dto)
    {
        var property = _mapper.Map<Property>(dto);
        property.AgentId = userId;
        property.CreatedDate = DateTime.UtcNow;

        await _context.Properties.AddAsync(property);
        await _context.SaveChangesAsync();

        return _mapper.Map<PropertyDto>(property);
    }

    public async Task<PropertyDto?> UpdatePropertyAsync(int id, UpdatePropertyDto dto)
    {
        var property = await _context.Properties.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        if (property == null)
            return null;

        _mapper.Map(dto, property);
        property.ModifiedDate = DateTime.UtcNow;

        _context.Properties.Update(property);
        await _context.SaveChangesAsync();

        return _mapper.Map<PropertyDto>(property);
    }

    public async Task<bool> DeletePropertyAsync(int id)
    {
        var property = await _context.Properties.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        if (property == null)
            return false;

        property.IsDeleted = true;
        property.ModifiedDate = DateTime.UtcNow;
        _context.Properties.Update(property);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<PaginatedResult<PropertyDto>> GetAllPropertiesAsync(int pageNumber, int pageSize)
    {
        var properties = await _context.Properties
            .Where(p => !p.IsDeleted && p.Status== "Available")
            .OrderByDescending(p => p.CreatedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var total = await _context.Properties
            .Where(p => !p.IsDeleted && p.Status == "Available")
            .CountAsync();

        return new PaginatedResult<PropertyDto>
        {
            Items = _mapper.Map<IEnumerable<PropertyDto>>(properties),
            PageNumber = pageNumber,
            PageSize = pageSize,
            Total = total
        };
    }

    public async Task<PropertyDto?> GetPropertyByIdAsync(int id)
    {
        var property = await _context.Properties
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

        return property == null ? null : _mapper.Map<PropertyDto>(property);
    }

    public async Task<PaginatedResult<PropertyDto>> SearchPropertiesAsync(string? location, int? propertyType, decimal? minPrice, decimal? maxPrice, int pageNumber, int pageSize)
    {
        var query = _context.Properties.Where(p => !p.IsDeleted && p.Status == "Available");

        if (!string.IsNullOrEmpty(location))
            query = query.Where(p => p.Location.City.Contains(location));

        if (propertyType.HasValue)
            query = query.Where(p => p.PropertyTypeId== propertyType);

        if (minPrice.HasValue)
            query = query.Where(p => p.Price >= minPrice);

        if (maxPrice.HasValue)
            query = query.Where(p => p.Price <= maxPrice);

        var total = await query.CountAsync();

        var properties = await query
            .OrderByDescending(p => p.CreatedDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResult<PropertyDto>
        {
            Items = _mapper.Map<IEnumerable<PropertyDto>>(properties),
            PageNumber = pageNumber,
            PageSize = pageSize,
            Total = total
        };
    }
}
