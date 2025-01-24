using AutoMapper;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.UpdateDish;
using Restaurants.Application.Dishes.DishDtos;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.DTOs
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<Dish,DishDto>().ReverseMap();
            CreateMap<CreateDishCommand, Dish>();
            CreateMap<UpdateDishCommand, Dish>();
        }
    }
}
