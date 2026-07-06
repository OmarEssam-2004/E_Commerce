using AutoMapper;
using E_Commerce.Application.DTOS.Products;
using E_Commerce.Domain.Entities.Products;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.MappingProfile.Products
{
    public class PictureUrlResolver(IConfiguration Configuration) : IValueResolver<Product, ProductDto, string>
    {
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            return $"{Configuration["BaseUrl"]}/{source.PictureUrl}";
        }
    }
}
