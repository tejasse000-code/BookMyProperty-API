using BookMyProperty.Application.DTOs;

namespace BookMyProperty.Application.Features.Properties.Queries;

public class SearchPropertiesQuery
{
    public string? Location { get; set; }
    public int? PropertyType { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class SearchPropertiesQueryHandler
{
    private readonly IPropertyQueryRepository _repository;

    public SearchPropertiesQueryHandler(IPropertyQueryRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedResult<PropertyDto>> HandleAsync(SearchPropertiesQuery query)
    {
        return await _repository.SearchPropertiesAsync(query.Location, query.PropertyType, query.MinPrice, query.MaxPrice, query.PageNumber, query.PageSize);
    }
}
