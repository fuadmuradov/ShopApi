using AutoMapper;
using ShopApi.Apps.AdminApi.DTOs.CateforyDtos;
using ShopApi.Apps.AdminApi.DTOs.ProductDtos;
using ShopApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Apps.AdminApi.Profiles
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Category, CategoryGetDtos>();
            CreateMap<Category, CategoryInProductGetDto>();

            CreateMap<Product, ProductGetDto>()
                .ForMember(dest => dest.Profit, map => map.MapFrom(src => src.SalePrice - src.CostPrice));
        }
    }
}
