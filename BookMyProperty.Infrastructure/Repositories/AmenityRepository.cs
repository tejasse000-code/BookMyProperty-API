using AutoMapper;
using BookMyProperty.Application.DTOs;
using BookMyProperty.Domain.Entities;
using BookMyProperty.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookMyProperty.Infrastructure.Repositories;

public interface IAmenityRepository
{
    Task<AmenityDto?> GetByIdAsync(int id);
    Task<IEnumerable<AmenityDto>> GetAllAsync();
    Task<AmenityDto> CreateAsync(CreateAmenityDto dto);
    Task<AmenityDto?> UpdateAsync(int id, UpdateAmenityDto dto);
    Task<bool> DeleteAsync(int id);
}

public class AmenityRepository : IAmenityRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AmenityRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AmenityDto?> GetByIdAsync(int id)
    {
        var amenity = await _context.Amenities
            .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        return amenity == null ? null : _mapper.Map<AmenityDto>(amenity);
    }

    public async Task<IEnumerable<AmenityDto>> GetAllAsync()
    {
        var amenities = await _context.Amenities
            .Where(a => !a.IsDeleted)
            .OrderBy(a => a.Name)
            .ToListAsync();
        return _mapper.Map<IEnumerable<AmenityDto>>(amenities);
    }

    public async Task<AmenityDto> CreateAsync(CreateAmenityDto dto)
    {
        var amenity = _mapper.Map<Amenity>(dto);
        amenity.CreatedDate = DateTime.UtcNow;

        await _context.Amenities.AddAsync(amenity);
        await _context.SaveChangesAsync();

        return _mapper.Map<AmenityDto>(amenity);
    }

    public async Task<AmenityDto?> UpdateAsync(int id, UpdateAmenityDto dto)
    {
        var amenity = await _context.Amenities
            .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        if (amenity == null)
            return null;

        _mapper.Map(dto, amenity);
        amenity.ModifiedDate = DateTime.UtcNow;

        _context.Amenities.Update(amenity);
        await _context.SaveChangesAsync();

        return _mapper.Map<AmenityDto>(amenity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var amenity = await _context.Amenities
            .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        if (amenity == null)
            return false;

        amenity.IsDeleted = true;
        amenity.ModifiedDate = DateTime.UtcNow;

        _context.Amenities.Update(amenity);
        await _context.SaveChangesAsync();

        return true;
    }
}
