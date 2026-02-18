using BookMyProperty.Application.DTOs;

namespace BookMyProperty.Application.Features.Properties.Queries;

public class GetAllPropertiesQuery
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class GetAllPropertiesQueryHandler
{
    private readonly IPropertyQueryRepository _repository;

    public GetAllPropertiesQueryHandler(IPropertyQueryRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<PropertyDto>> HandleAsync(GetAllPropertiesQuery query)
    {
        return await _repository.GetAllPropertiesAsync(query.PageNumber, query.PageSize);
    }
}

public interface IPropertyQueryRepository
{
    Task<PaginatedResult<PropertyDto>> GetAllPropertiesAsync(int pageNumber, int pageSize);
    Task<PropertyDto?> GetPropertyByIdAsync(int id);
    Task<PaginatedResult<PropertyDto>> SearchPropertiesAsync(string? location, int? propertyType, decimal? minPrice, decimal? maxPrice, int pageNumber, int pageSize);
}
