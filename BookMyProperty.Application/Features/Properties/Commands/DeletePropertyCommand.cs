namespace BookMyProperty.Application.Features.Properties.Commands;

public class DeletePropertyCommand
{
    public int PropertyId { get; set; }
}

public class DeletePropertyCommandHandler
{
    private readonly IPropertyRepository _repository;

    public DeletePropertyCommandHandler(IPropertyRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> HandleAsync(DeletePropertyCommand command)
    {
        return await _repository.DeletePropertyAsync(command.PropertyId);
    }
}
