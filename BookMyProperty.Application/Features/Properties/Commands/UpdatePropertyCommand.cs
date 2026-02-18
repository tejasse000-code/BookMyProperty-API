using BookMyProperty.Application.DTOs;

namespace BookMyProperty.Application.Features.Properties.Commands;

public class UpdatePropertyCommand
{
    public int Id { get; set; }
    public UpdatePropertyDto Dto { get; set; } = new();
}

public class UpdatePropertyCommandHandler
{
    private readonly IPropertyRepository _repository;

    public UpdatePropertyCommandHandler(IPropertyRepository repository)
    {
        _repository = repository;
    }

    public async Task<PropertyDto?> HandleAsync(UpdatePropertyCommand command)
    {
        return await _repository.UpdatePropertyAsync(command.Id, command.Dto);
    }
}
