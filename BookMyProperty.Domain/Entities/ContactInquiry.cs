using BookMyProperty.Domain.Common;
using BookMyProperty.Domain.Entities;

public class ContactInquiry : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    public int PropertyId { get; set; }
    public Property Property { get; set; }
}
