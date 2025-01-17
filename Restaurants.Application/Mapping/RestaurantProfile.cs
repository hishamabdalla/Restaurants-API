using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.RestaurantDtos;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Mapping
{
    public class RestaurantProfile:Profile
    {
        public RestaurantProfile()
        {
            CreateMap<UpdateRestaurantCommand, Restaurant>();
            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(des => des.Address, opt => opt.MapFrom(src => new Address
                {
                    City = src.City,
                    PostalCode = src.PostalCode,
                    Street = src.Street
                }));

            CreateMap<CreateRestaurantCommand, Restaurant>()
               .ForMember(des => des.Address, opt => opt.MapFrom(src => new Address
               {
                   City = src.City,
                   PostalCode = src.PostalCode,
                   Street = src.Street
               }));

            CreateMap<Restaurant,RestaurantDto>()
                .ForMember(des=>des.Street,opt=>opt.MapFrom(src=>src.Address == null?null:src.Address.Street))
                .ForMember(des=>des.PostalCode,opt=>opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
                .ForMember(des=>des.City,opt=>opt.MapFrom(src => src.Address == null ? null : src.Address.City))
                .ForMember(des=>des.Dishes,opt=>opt.MapFrom(src=>src.Dishes))
                .ReverseMap();

        }
    }
}
