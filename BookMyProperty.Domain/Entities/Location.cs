using BookMyProperty.Domain.Common;
using BookMyProperty.Domain.Entities;

public class Location : BaseEntity
{
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;

    public ICollection<Property> Properties { get; set; } = new List<Property>();
}
