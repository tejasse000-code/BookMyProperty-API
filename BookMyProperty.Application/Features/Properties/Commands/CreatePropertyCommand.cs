using BookMyProperty.Application.DTOs;

namespace BookMyProperty.Application.Features.Properties.Commands;

public class CreatePropertyCommand
{
    public int UserId { get; set; }
    public CreatePropertyDto Dto { get; set; } = new();
}

public class CreatePropertyCommandHandler
{
    private readonly IPropertyRepository _repository;

    public CreatePropertyCommandHandler(IPropertyRepository repository)
    {
        _repository = repository;
    }

    public async Task<PropertyDto> HandleAsync(CreatePropertyCommand command)
    {
        return await _repository.CreatePropertyAsync(command.UserId, command.Dto);
    }
}

public interface IPropertyRepository
{
    Task<PropertyDto> CreatePropertyAsync(int userId, CreatePropertyDto dto);
    Task<PropertyDto?> UpdatePropertyAsync(int id, UpdatePropertyDto dto);
    Task<bool> DeletePropertyAsync(int id);
}
