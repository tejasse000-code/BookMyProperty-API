using BookMyProperty.Application.DTOs;

namespace BookMyProperty.Application.Features.Properties.Queries;

public class GetPropertyByIdQuery
{
    public int Id { get; set; }
}

public class GetPropertyByIdQueryHandler
{
    private readonly IPropertyQueryRepository _repository;

    public GetPropertyByIdQueryHandler(IPropertyQueryRepository repository)
    {
        _repository = repository;
    }

    public async Task<PropertyDto?> HandleAsync(GetPropertyByIdQuery query)
    {
        return await _repository.GetPropertyByIdAsync(query.Id);
    }
}
