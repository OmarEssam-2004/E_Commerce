using AutoMapper;
using E_Commerce.Application.DTOS.Products;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.MappingProfile.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
            CreateMap<Product, ProductDto>()
                    .ForMember(D => D.ProductBrand, opt => opt.MapFrom(S => S.Brand.Name))
                    .ForMember(D => D.ProductType, opt => opt.MapFrom(S => S.Type.Name))
                    .ForMember(D => D.PictureUrl, opt => opt.MapFrom<PictureUrlResolver>());

        }
    }
}
