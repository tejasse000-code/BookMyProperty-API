namespace BookMyProperty.Application.DTOs;

public class PropertyImageDto
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
    public int PropertyId { get; set; }
}

public class CreatePropertyImageDto
{
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
    public int PropertyId { get; set; }
}

public class UpdatePropertyImageDto
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }
}
