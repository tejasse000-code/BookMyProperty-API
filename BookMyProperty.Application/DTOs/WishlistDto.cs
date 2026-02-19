namespace BookMyProperty.Application.DTOs;

public class WishlistDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PropertyId { get; set; }
    public PropertyDto? Property { get; set; }
    public DateTime CreatedDate { get; set; }
}

public class CreateWishlistDto
{
    public int PropertyId { get; set; }
}
