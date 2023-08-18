using AutoMapper;
using BookShop.Application.BookShop;
using BookShop.Application.BookShop.Commands.EditBookShop;
using BookShop.Application.BookShopService;
using BookShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Application.Mappings
{
    public class BookShopMappingProfile : Profile
    {
        public BookShopMappingProfile(ApplicationUser.IUserContext userContext) 
        {
            var user = userContext.GetCurrentUser();
            CreateMap<BookShopDto, Domain.Entities.BookShop>()
                .ForMember(e => e.ContactDetails, p => p.MapFrom(src => new  BookShopContactDetails()
                {
                    City = src.City,
                    NumberPhone = src.NumberPhone,
                    PostalCode = src.PostalCode,
                    Street = src.Street,
                }));

            CreateMap<Domain.Entities.BookShop, BookShopDto>()
                .ForMember(d => d.IsEditable, o => o.MapFrom(src => user != null 
                                             && (src.CreatedById == user.Id || user.IsInRole("Moderator") ) ))
                .ForMember(d => d.Street, o => o.MapFrom(src => src.ContactDetails.Street))
                .ForMember(d => d.PostalCode, o => o.MapFrom(src => src.ContactDetails.PostalCode))
                .ForMember(d => d.NumberPhone, o => o.MapFrom(src => src.ContactDetails.NumberPhone))
                .ForMember(d => d.City, o => o.MapFrom(src => src.ContactDetails.City));

            CreateMap<BookShopDto, EditBookShopCommand>();

            CreateMap<BookShopServiceDto, Domain.Entities.BookShopService>()
                .ReverseMap();

        }
    }
}
