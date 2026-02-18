using BookMyProperty.Domain.Common;

public class Amenity : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<PropertyAmenity> PropertyAmenities { get; set; } = new List<PropertyAmenity>();
}
