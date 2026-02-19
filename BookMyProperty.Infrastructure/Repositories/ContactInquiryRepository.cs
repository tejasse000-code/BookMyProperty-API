using AutoMapper;
using BookMyProperty.Application.DTOs;
using BookMyProperty.Domain.Entities;
using BookMyProperty.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookMyProperty.Infrastructure.Repositories;

public interface IContactInquiryRepository
{
    Task<ContactInquiryDto?> GetByIdAsync(int id);
    Task<IEnumerable<ContactInquiryDto>> GetAllAsync();
    Task<IEnumerable<ContactInquiryDto>> GetByPropertyIdAsync(int propertyId);
    Task<ContactInquiryDto> CreateAsync(CreateContactInquiryDto dto);
    Task<bool> DeleteAsync(int id);
}

public class ContactInquiryRepository : IContactInquiryRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ContactInquiryRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ContactInquiryDto?> GetByIdAsync(int id)
    {
        var inquiry = await _context.ContactInquiries
            .FirstOrDefaultAsync(ci => ci.Id == id && !ci.IsDeleted);
        return inquiry == null ? null : _mapper.Map<ContactInquiryDto>(inquiry);
    }

    public async Task<IEnumerable<ContactInquiryDto>> GetAllAsync()
    {
        var inquiries = await _context.ContactInquiries
            .Where(ci => !ci.IsDeleted)
            .OrderByDescending(ci => ci.CreatedDate)
            .ToListAsync();
        return _mapper.Map<IEnumerable<ContactInquiryDto>>(inquiries);
    }

    public async Task<IEnumerable<ContactInquiryDto>> GetByPropertyIdAsync(int propertyId)
    {
        var inquiries = await _context.ContactInquiries
            .Where(ci => ci.PropertyId == propertyId && !ci.IsDeleted)
            .OrderByDescending(ci => ci.CreatedDate)
            .ToListAsync();
        return _mapper.Map<IEnumerable<ContactInquiryDto>>(inquiries);
    }

    public async Task<ContactInquiryDto> CreateAsync(CreateContactInquiryDto dto)
    {
        var inquiry = _mapper.Map<ContactInquiry>(dto);
        inquiry.CreatedDate = DateTime.UtcNow;

        await _context.ContactInquiries.AddAsync(inquiry);
        await _context.SaveChangesAsync();

        return _mapper.Map<ContactInquiryDto>(inquiry);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var inquiry = await _context.ContactInquiries
            .FirstOrDefaultAsync(ci => ci.Id == id && !ci.IsDeleted);
        if (inquiry == null)
            return false;

        inquiry.IsDeleted = true;
        inquiry.ModifiedDate = DateTime.UtcNow;

        _context.ContactInquiries.Update(inquiry);
        await _context.SaveChangesAsync();

        return true;
    }
}
