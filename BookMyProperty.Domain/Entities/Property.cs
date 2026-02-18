using BookMyProperty.Domain.Common;
using BookMyProperty.Domain.Enums;

namespace BookMyProperty.Domain.Entities;

public class Property : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int PropertyTypeId { get; set; }
    public PropertyType PropertyType { get; set; }

    public int LocationId { get; set; }
    public Location Location { get; set; }

    public double AreaSqFt { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public int Parking { get; set; }

    public string Status { get; set; } = "Available";
    public bool IsFeatured { get; set; }

    public int AgentId { get; set; }
    public User Agent { get; set; }

    public ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();
    public ICollection<PropertyAmenity> PropertyAmenities { get; set; } = new List<PropertyAmenity>();
    public ICollection<ContactInquiry> ContactInquiries { get; set; } = new List<ContactInquiry>();

}
