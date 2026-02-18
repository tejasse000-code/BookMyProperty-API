using BookMyProperty.Domain.Common;
using BookMyProperty.Domain.Enums;

namespace BookMyProperty.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    //public UserRole Role { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    public bool IsActive { get; set; } = true;
    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
    public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

}
