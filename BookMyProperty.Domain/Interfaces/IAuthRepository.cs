namespace BookMyProperty.Domain.Interfaces;

public interface IAuthRepository
{
    Task<(bool IsSuccess, string Message)> RegisterAsync(string email, string passwordHash, string firstName, string lastName, string phoneNumber);
    Task<(bool IsSuccess, string? Token, string Message)> LoginAsync(string email, string password);
    Task<bool> EmailExistsAsync(string email);
}
