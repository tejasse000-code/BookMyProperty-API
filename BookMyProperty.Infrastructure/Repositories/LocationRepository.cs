using AutoMapper;
using BookMyProperty.Application.DTOs;
using BookMyProperty.Domain.Entities;
using BookMyProperty.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookMyProperty.Infrastructure.Repositories;

public interface ILocationRepository
{
    Task<LocationDto?> GetByIdAsync(int id);
    Task<IEnumerable<LocationDto>> GetAllAsync();
    Task<LocationDto> CreateAsync(CreateLocationDto dto);
    Task<LocationDto?> UpdateAsync(int id, UpdateLocationDto dto);
    Task<bool> DeleteAsync(int id);
}

public class LocationRepository : ILocationRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public LocationRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LocationDto?> GetByIdAsync(int id)
    {
        var location = await _context.Locations
            .FirstOrDefaultAsync(l => l.Id == id && !l.IsDeleted);
        return location == null ? null : _mapper.Map<LocationDto>(location);
    }

    public async Task<IEnumerable<LocationDto>> GetAllAsync()
    {
        var locations = await _context.Locations
            .Where(l => !l.IsDeleted)
            .OrderBy(l => l.City)
            .ToListAsync();
        return _mapper.Map<IEnumerable<LocationDto>>(locations);
    }

    public async Task<LocationDto> CreateAsync(CreateLocationDto dto)
    {
        var location = _mapper.Map<Location>(dto);
        location.CreatedDate = DateTime.UtcNow;

        await _context.Locations.AddAsync(location);
        await _context.SaveChangesAsync();

        return _mapper.Map<LocationDto>(location);
    }

    public async Task<LocationDto?> UpdateAsync(int id, UpdateLocationDto dto)
    {
        var location = await _context.Locations
            .FirstOrDefaultAsync(l => l.Id == id && !l.IsDeleted);
        if (location == null)
            return null;

        _mapper.Map(dto, location);
        location.ModifiedDate = DateTime.UtcNow;

        _context.Locations.Update(location);
        await _context.SaveChangesAsync();

        return _mapper.Map<LocationDto>(location);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var location = await _context.Locations
            .FirstOrDefaultAsync(l => l.Id == id && !l.IsDeleted);
        if (location == null)
            return false;

        location.IsDeleted = true;
        location.ModifiedDate = DateTime.UtcNow;

        _context.Locations.Update(location);
        await _context.SaveChangesAsync();

        return true;
    }
}
