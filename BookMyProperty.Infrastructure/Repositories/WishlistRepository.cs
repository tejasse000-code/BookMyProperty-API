using AutoMapper;
using BookMyProperty.Application.DTOs;
using BookMyProperty.Domain.Entities;
using BookMyProperty.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookMyProperty.Infrastructure.Repositories;

public interface IWishlistRepository
{
    Task<WishlistDto?> GetByIdAsync(int id);
    Task<IEnumerable<WishlistDto>> GetByUserIdAsync(int userId);
    Task<WishlistDto> CreateAsync(int userId, CreateWishlistDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> CheckIfExistsAsync(int userId, int propertyId);
}

public class WishlistRepository : IWishlistRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public WishlistRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<WishlistDto?> GetByIdAsync(int id)
    {
        var wishlist = await _context.Wishlists
            .Include(w => w.Property)
            .FirstOrDefaultAsync(w => w.Id == id && !w.IsDeleted);
        return wishlist == null ? null : _mapper.Map<WishlistDto>(wishlist);
    }

    public async Task<IEnumerable<WishlistDto>> GetByUserIdAsync(int userId)
    {
        var wishlists = await _context.Wishlists
            .Include(w => w.Property)
            .Where(w => w.UserId == userId && !w.IsDeleted)
            .OrderByDescending(w => w.CreatedDate)
            .ToListAsync();
        return _mapper.Map<IEnumerable<WishlistDto>>(wishlists);
    }

    public async Task<WishlistDto> CreateAsync(int userId, CreateWishlistDto dto)
    {
        var wishlist = new Wishlist
        {
            UserId = userId,
            PropertyId = dto.PropertyId,
            CreatedDate = DateTime.UtcNow
        };

        await _context.Wishlists.AddAsync(wishlist);
        await _context.SaveChangesAsync();

        return _mapper.Map<WishlistDto>(wishlist);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var wishlist = await _context.Wishlists
            .FirstOrDefaultAsync(w => w.Id == id && !w.IsDeleted);
        if (wishlist == null)
            return false;

        wishlist.IsDeleted = true;
        wishlist.ModifiedDate = DateTime.UtcNow;

        _context.Wishlists.Update(wishlist);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> CheckIfExistsAsync(int userId, int propertyId)
    {
        return await _context.Wishlists
            .AnyAsync(w => w.UserId == userId && w.PropertyId == propertyId && !w.IsDeleted);
    }
}
