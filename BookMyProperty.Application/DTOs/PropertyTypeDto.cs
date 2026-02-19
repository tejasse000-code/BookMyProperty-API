namespace BookMyProperty.Application.DTOs;

public class PropertyTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreatePropertyTypeDto
{
    public string Name { get; set; } = string.Empty;
}

public class UpdatePropertyTypeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
