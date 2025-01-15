using AutoMapper;
using Restaurants.Application.DTOs.DishDtos;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Mapping
{
    public class DishProfile:Profile
    {
        public DishProfile()
        {
            CreateMap<Dish,DishDto>().ReverseMap();
        }
    }
}
