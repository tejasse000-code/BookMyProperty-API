using AutoMapper;
using BookMyProperty.Application.DTOs;
using BookMyProperty.Domain.Entities;
using BookMyProperty.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookMyProperty.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<UserDto?> GetByIdAsync(int id);
    Task<UserDto?> GetByEmailAsync(string email);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<UserDto> CreateAsync(User user);
    Task<UserDto?> UpdateAsync(int id, User user);
    Task<bool> DeleteAsync(int id);
}

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> GetByEmailAsync(string email)
    {
        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _context.Users
            .Include(u => u.Role)
            .Where(u => !u.IsDeleted)
            .OrderBy(u => u.FirstName)
            .ToListAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto> CreateAsync(User user)
    {
        user.CreatedDate = DateTime.UtcNow;
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto?> UpdateAsync(int id, User userUpdate)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        if (user == null)
            return null;

        user.FirstName = userUpdate.FirstName;
        user.LastName = userUpdate.LastName;
        user.PhoneNumber = userUpdate.PhoneNumber;
        user.IsActive = userUpdate.IsActive;
        user.ModifiedDate = DateTime.UtcNow;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
        if (user == null)
            return false;

        user.IsDeleted = true;
        user.ModifiedDate = DateTime.UtcNow;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return true;
    }
}
