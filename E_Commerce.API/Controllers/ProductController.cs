using E_Commerce.API.Attributes;
using E_Commerce.Application.Common;
using E_Commerce.Application.DTOS.Products;
using E_Commerce.Application.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ApiBaseController
    {
        // GET: api/Product
        [HttpGet]
        [RedisCache(60)]
        public async Task<ActionResult<PaginationResult<ProductDto>>> GetAllProducts([FromQuery] ProductQueryParams queryParams, CancellationToken ct = default)
        {

          var result = await productService.GetAllProductsAsync(queryParams, ct);
            return ToActionResult(result);

        }
        // GET: api/Product
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrands(CancellationToken ct = default)
        {

          var result = await productService.GetAllBrandsAsync(ct);
            return ToActionResult(result);

        }
        // GET: api/Product
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes(CancellationToken ct = default)
        {

          var result = await productService.GetAllTypesAsync(ct);
            return ToActionResult(result);

        }
        // GET: api/Product
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetAllProductById(int id, CancellationToken ct = default)
        {

            var result = await productService.GetProductByIdAsync(id, ct);
            return ToActionResult(result);

        }

    }
}
