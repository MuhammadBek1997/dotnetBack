using AutoMapper;
using FudballManagement.Application.DTOs.Admin;
using FudballManagement.Application.DTOs.Commons;
using FudballManagement.Application.DTOs.Customer;
using FudballManagement.Application.DTOs.Order;
using FudballManagement.Application.DTOs.Stadium;
using FudballManagement.Application.DTOs.Stadium.SadiumComment;
using FudballManagement.Domain.Entities.Order;
using FudballManagement.Domain.Entities.Stadiums;
using FudballManagement.Domain.Entities.Users;

namespace FudballManagement.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Admin
        CreateMap<Admin, AdminResponseDto>()
    .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.ProfilePhoto))
    .ForMember(dest => dest.CreatAtUtc, opt => opt.MapFrom(src => src.CreatedAt));
        CreateMap<AdminCreateDto, Admin>().ReverseMap();
        CreateMap<AdminUpdateDto, Admin>().ReverseMap();
        CreateMap<AdminProfileDto, Admin>().ReverseMap();

        CreateMap<Admin, AdminProfileResponseDto>()
            .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.ProfilePhoto))
            .ForMember(dest => dest.UpdateAtUtc, opt => opt.MapFrom(src => src.UpdatedAt));

        CreateMap<AdminProfileResponseDto, Admin>()
            .ForMember(dest => dest.ProfilePhoto, opt => opt.MapFrom(src => src.PhotoPath));
        #endregion


        #region Customer
        // One mapping with all fields
        CreateMap<Customer, CustomerResponseDto>()
            .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.ProfilePhoto))
            .ForMember(dest => dest.CreatAtUtc, opt => opt.MapFrom(src => src.CreatedAt));

        CreateMap<CustomerResponseDto, Customer>()
            .ForMember(dest => dest.ProfilePhoto, opt => opt.MapFrom(src => src.PhotoPath));

        // One mapping with all fields
        CreateMap<Customer, CustomerProfileResponseDto>()
            .ForMember(dest => dest.PhotoPath, opt => opt.MapFrom(src => src.ProfilePhoto))
            .ForMember(dest => dest.UpdateAtUtc, opt => opt.MapFrom(src => src.UpdatedAt));

        CreateMap<CustomerProfileResponseDto, Customer>()
            .ForMember(dest => dest.ProfilePhoto, opt => opt.MapFrom(src => src.PhotoPath));


        CreateMap<CustomerCreateDto, Customer>().ReverseMap();
        CreateMap<CustomerProfileDto, Customer>().ReverseMap();
        CreateMap<CustomerUpdateDto, Customer>().ReverseMap();
        #endregion

        #region Stadiu
        CreateMap<StadiumCreateDto, Stadium>()
    .ForMember(dest => dest.StadiumMedias, opt => opt.Ignore());
        CreateMap<StadiumUpdateDto, Stadium>()
    .ForMember(dest => dest.StadiumMedias, opt => opt.Ignore());

        //CreateMap<Stadium, StadiumResponseDto>();
        CreateMap<StadiumMedia, PhotoUploadDto>(); // if you use DTO for media

        //CreateMap<StadiumUpdateDto, Stadium>().ReverseMap();
        CreateMap<StadiumResponseDto, Stadium>().ReverseMap();
        CreateMap<StadiumComentCreateDto, StadiumComment>().ReverseMap();
        CreateMap<StadiumCommentResponseDto, StadiumComment>().ReverseMap();
        CreateMap<StadiumComment, StadiumCommentResponseDto>()
            .ForMember(dest => dest.FullName, opt => 
            opt.MapFrom(src => src.IsAnonymous ? null : src.Customer.FullName));

        CreateMap<StadiumComment, StadiumComentCreateDto>()
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId));

        CreateMap<Stadium, StadiumResponseDto>()
    .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src =>
        src.stadiumRatings.Any() ? (double)src.stadiumRatings.Average(r => r.Rating) : 0))
    .ForMember(dest => dest.TotalVotes, opt => opt.MapFrom(src => src.stadiumRatings.Count))
    .ForMember(dest => dest.StadiumMedias, opt => opt.MapFrom(src => src.StadiumMedias))
    .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.StadiumComments));
        CreateMap<StadiumRating, StadiumRatingDto>().ReverseMap();
        CreateMap<OrderCreateDto, Orders>().ReverseMap();
        CreateMap<OrderResponseDto, Orders>().ReverseMap();

        #endregion
    }
};
