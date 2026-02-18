using BookMyProperty.Application.DTOs;

namespace BookMyProperty.Application.Validators;

public class CreatePropertyDtoValidator
{
    public static bool Validate(CreatePropertyDto dto, out List<string> errors)
    {
        errors = new List<string>();

        if (string.IsNullOrWhiteSpace(dto.Title))
            errors.Add("Title is required.");
        else if (dto.Title.Length < 5 || dto.Title.Length > 200)
            errors.Add("Title must be between 5 and 200 characters.");

        if (string.IsNullOrWhiteSpace(dto.Description))
            errors.Add("Description is required.");
        else if (dto.Description.Length < 10 || dto.Description.Length > 2000)
            errors.Add("Description must be between 10 and 2000 characters.");

        if (dto.Price <= 0)
            errors.Add("Price must be greater than 0.");

        if (string.IsNullOrWhiteSpace(dto.Location))
            errors.Add("Location is required.");

        if (dto.PropertyType <= 0 || dto.PropertyType > 6)
            errors.Add("Invalid PropertyType.");

        if (dto.Area <= 0)
            errors.Add("Area must be greater than 0.");

        if (dto.Bedrooms < 0)
            errors.Add("Bedrooms cannot be negative.");

        if (dto.Bathrooms < 0)
            errors.Add("Bathrooms cannot be negative.");

        return errors.Count == 0;
    }
}
