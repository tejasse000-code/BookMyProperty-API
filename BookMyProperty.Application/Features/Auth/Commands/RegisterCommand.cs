using BookMyProperty.Application.DTOs;

namespace BookMyProperty.Application.Features.Auth.Commands;

public class RegisterCommand
{
    public RegisterDto Dto { get; set; } = new();
}

public class RegisterCommandHandler
{
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResponseDto> HandleAsync(RegisterCommand command)
    {
        return await _authService.RegisterAsync(command.Dto);
    }
}
