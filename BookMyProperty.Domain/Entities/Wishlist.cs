using BookMyProperty.Domain.Common;
using BookMyProperty.Domain.Entities;

public class Wishlist : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int PropertyId { get; set; }
    public Property Property { get; set; }
}
