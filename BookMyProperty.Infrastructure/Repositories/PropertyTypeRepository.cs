using AutoMapper;
using BookMyProperty.Application.DTOs;
using BookMyProperty.Domain.Entities;
using BookMyProperty.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookMyProperty.Infrastructure.Repositories;

public interface IPropertyTypeRepository
{
    Task<PropertyTypeDto?> GetByIdAsync(int id);
    Task<IEnumerable<PropertyTypeDto>> GetAllAsync();
    Task<PropertyTypeDto> CreateAsync(CreatePropertyTypeDto dto);
    Task<PropertyTypeDto?> UpdateAsync(int id, UpdatePropertyTypeDto dto);
    Task<bool> DeleteAsync(int id);
}

public class PropertyTypeRepository : IPropertyTypeRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PropertyTypeRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PropertyTypeDto?> GetByIdAsync(int id)
    {
        var propertyType = await _context.PropertyTypes
            .FirstOrDefaultAsync(pt => pt.Id == id && !pt.IsDeleted);
        return propertyType == null ? null : _mapper.Map<PropertyTypeDto>(propertyType);
    }

    public async Task<IEnumerable<PropertyTypeDto>> GetAllAsync()
    {
        var propertyTypes = await _context.PropertyTypes
            .Where(pt => !pt.IsDeleted)
            .OrderBy(pt => pt.Name)
            .ToListAsync();
        return _mapper.Map<IEnumerable<PropertyTypeDto>>(propertyTypes);
    }

    public async Task<PropertyTypeDto> CreateAsync(CreatePropertyTypeDto dto)
    {
        var propertyType = _mapper.Map<PropertyType>(dto);
        propertyType.CreatedDate = DateTime.UtcNow;

        await _context.PropertyTypes.AddAsync(propertyType);
        await _context.SaveChangesAsync();

        return _mapper.Map<PropertyTypeDto>(propertyType);
    }

    public async Task<PropertyTypeDto?> UpdateAsync(int id, UpdatePropertyTypeDto dto)
    {
        var propertyType = await _context.PropertyTypes
            .FirstOrDefaultAsync(pt => pt.Id == id && !pt.IsDeleted);
        if (propertyType == null)
            return null;

        _mapper.Map(dto, propertyType);
        propertyType.ModifiedDate = DateTime.UtcNow;

        _context.PropertyTypes.Update(propertyType);
        await _context.SaveChangesAsync();

        return _mapper.Map<PropertyTypeDto>(propertyType);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var propertyType = await _context.PropertyTypes
            .FirstOrDefaultAsync(pt => pt.Id == id && !pt.IsDeleted);
        if (propertyType == null)
            return false;

        propertyType.IsDeleted = true;
        propertyType.ModifiedDate = DateTime.UtcNow;

        _context.PropertyTypes.Update(propertyType);
        await _context.SaveChangesAsync();

        return true;
    }
}
