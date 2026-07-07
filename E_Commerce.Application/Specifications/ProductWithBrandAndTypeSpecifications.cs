using E_Commerce.Application.Common;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecification<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams) :
            base
            (
                p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
                &&
                (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
                &&
                (string.IsNullOrWhiteSpace(queryParams.SearchValue) || p.Name.ToLower().Contains(queryParams.SearchValue.ToLower()))
            )
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);

            switch(queryParams.Sort)
            {
                case ProductSortOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Id);
                    break;
            }

            ApplyPagination(queryParams.PageIndex, queryParams.PageSize);

        }
        public ProductWithBrandAndTypeSpecifications(int id) : base(p => p.Id == id)
        {
            AddInclude(p => p.Brand);
            AddInclude(p => p.Type);
        }

    }
}
