using AutoMapper;
using BookMyProperty.Application.DTOs;
using BookMyProperty.Domain.Entities;

namespace BookMyProperty.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Property mappings
        CreateMap<Property, PropertyDto>().ReverseMap();
        CreateMap<CreatePropertyDto, Property>();
        CreateMap<UpdatePropertyDto, Property>();

        // User mappings
        CreateMap<User, UserDto>().ReverseMap();

        // Location mappings
        CreateMap<Location, LocationDto>().ReverseMap();
        CreateMap<CreateLocationDto, Location>();
        CreateMap<UpdateLocationDto, Location>();

        // Amenity mappings
        CreateMap<Amenity, AmenityDto>().ReverseMap();
        CreateMap<CreateAmenityDto, Amenity>();
        CreateMap<UpdateAmenityDto, Amenity>();

        // PropertyType mappings
        CreateMap<PropertyType, PropertyTypeDto>().ReverseMap();
        CreateMap<CreatePropertyTypeDto, PropertyType>();
        CreateMap<UpdatePropertyTypeDto, PropertyType>();

        // ContactInquiry mappings
        CreateMap<ContactInquiry, ContactInquiryDto>().ReverseMap();
        CreateMap<CreateContactInquiryDto, ContactInquiry>();

        // Wishlist mappings
        CreateMap<Wishlist, WishlistDto>().ReverseMap();
        CreateMap<CreateWishlistDto, Wishlist>();

        // PropertyImage mappings
        CreateMap<PropertyImage, PropertyImageDto>().ReverseMap();
        CreateMap<CreatePropertyImageDto, PropertyImage>();
        CreateMap<UpdatePropertyImageDto, PropertyImage>();
    }
}
