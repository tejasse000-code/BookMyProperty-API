using BookMyProperty.Application.DTOs;

namespace BookMyProperty.Application.Features.Auth.Commands;

public class LoginCommand
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginCommandHandler
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<AuthResponseDto> HandleAsync(LoginCommand command)
    {
        return await _authService.LoginAsync(command.Email, command.Password);
    }
}

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(string email, string password);
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
}
