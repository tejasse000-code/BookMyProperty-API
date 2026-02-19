using AutoMapper;
using BookMyProperty.Application.DTOs;
using BookMyProperty.Domain.Entities;
using BookMyProperty.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookMyProperty.Infrastructure.Repositories;

public interface IPropertyImageRepository
{
    Task<PropertyImageDto?> GetByIdAsync(int id);
    Task<IEnumerable<PropertyImageDto>> GetByPropertyIdAsync(int propertyId);
    Task<PropertyImageDto> CreateAsync(CreatePropertyImageDto dto);
    Task<PropertyImageDto?> UpdateAsync(int id, UpdatePropertyImageDto dto);
    Task<bool> DeleteAsync(int id);
}

public class PropertyImageRepository : IPropertyImageRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public PropertyImageRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PropertyImageDto?> GetByIdAsync(int id)
    {
        var image = await _context.PropertyImages
            .FirstOrDefaultAsync(pi => pi.Id == id && !pi.IsDeleted);
        return image == null ? null : _mapper.Map<PropertyImageDto>(image);
    }

    public async Task<IEnumerable<PropertyImageDto>> GetByPropertyIdAsync(int propertyId)
    {
        var images = await _context.PropertyImages
            .Where(pi => pi.PropertyId == propertyId && !pi.IsDeleted)
            .OrderByDescending(pi => pi.IsPrimary)
            .ToListAsync();
        return _mapper.Map<IEnumerable<PropertyImageDto>>(images);
    }

    public async Task<PropertyImageDto> CreateAsync(CreatePropertyImageDto dto)
    {
        var image = _mapper.Map<PropertyImage>(dto);
        image.CreatedDate = DateTime.UtcNow;

        await _context.PropertyImages.AddAsync(image);
        await _context.SaveChangesAsync();

        return _mapper.Map<PropertyImageDto>(image);
    }

    public async Task<PropertyImageDto?> UpdateAsync(int id, UpdatePropertyImageDto dto)
    {
        var image = await _context.PropertyImages
            .FirstOrDefaultAsync(pi => pi.Id == id && !pi.IsDeleted);
        if (image == null)
            return null;

        _mapper.Map(dto, image);
        image.ModifiedDate = DateTime.UtcNow;

        _context.PropertyImages.Update(image);
        await _context.SaveChangesAsync();

        return _mapper.Map<PropertyImageDto>(image);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var image = await _context.PropertyImages
            .FirstOrDefaultAsync(pi => pi.Id == id && !pi.IsDeleted);
        if (image == null)
            return false;

        image.IsDeleted = true;
        image.ModifiedDate = DateTime.UtcNow;

        _context.PropertyImages.Update(image);
        await _context.SaveChangesAsync();

        return true;
    }
}
