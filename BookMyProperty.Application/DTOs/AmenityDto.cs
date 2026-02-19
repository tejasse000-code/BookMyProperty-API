namespace BookMyProperty.Application.DTOs;

public class AmenityDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class CreateAmenityDto
{
    public string Name { get; set; } = string.Empty;
}

public class UpdateAmenityDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
