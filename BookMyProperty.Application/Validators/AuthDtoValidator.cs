using BookMyProperty.Application.DTOs;

namespace BookMyProperty.Application.Validators;

public class AuthDtoValidator
{
    public static bool Validate(AuthDto dto, out List<string> errors)
    {
        errors = new List<string>();

        if (string.IsNullOrWhiteSpace(dto.Email))
            errors.Add("Email is required.");
        else if (!dto.Email.Contains("@"))
            errors.Add("Invalid email format.");

        if (string.IsNullOrWhiteSpace(dto.Password))
            errors.Add("Password is required.");
        else if (dto.Password.Length < 6)
            errors.Add("Password must be at least 6 characters.");

        return errors.Count == 0;
    }
}

public class RegisterDtoValidator
{
    public static bool Validate(RegisterDto dto, out List<string> errors)
    {
        errors = new List<string>();

        if (string.IsNullOrWhiteSpace(dto.Email))
            errors.Add("Email is required.");
        else if (!dto.Email.Contains("@"))
            errors.Add("Invalid email format.");

        if (string.IsNullOrWhiteSpace(dto.Password))
            errors.Add("Password is required.");
        else if (dto.Password.Length < 6)
            errors.Add("Password must be at least 6 characters.");

        if (string.IsNullOrWhiteSpace(dto.FirstName))
            errors.Add("FirstName is required.");

        if (string.IsNullOrWhiteSpace(dto.LastName))
            errors.Add("LastName is required.");

        if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
            errors.Add("PhoneNumber is required.");

        return errors.Count == 0;
    }
}
