using BookMyProperty.Domain.Common;
using BookMyProperty.Domain.Entities;

public class PropertyImage : BaseEntity
{
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsPrimary { get; set; }

    public int PropertyId { get; set; }
    public Property Property { get; set; }
}
