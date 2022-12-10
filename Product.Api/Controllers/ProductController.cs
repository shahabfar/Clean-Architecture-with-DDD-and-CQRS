using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Product.Application.DTOs;
using Product.Application.Features.Products;
using Product.Application.Paginations;
using Product.Domain.Enums;

namespace Product.Api.Controllers
{
    //[ApiVersion("2.0")]  // in order you want to have different version number for entire controller
    //[Route("api/v{version:apiVersion}/products")]  // in order to use versioning
    [Route("api/products")]
    public class ProductController : BaseController
    {
        // we use HttpPost here in order to be abale to use of Head and Body for pagination
        [HttpPost]
        [OpenApiOperation("Get all products w/o category Id.", "")]
        public async Task<PaginationResponse<ProductDto>> GetAllAsync(GetAllProductsRequest request)
        {
            return await Mediator.Send(request);
        }

        // in case the counts of products for different status requested only by one endpoint
        [HttpGet("count")]
        public async Task<ProductsCountDto> GetProductsCount()
        {
            return await Mediator.Send(new GetProductsCountRequest());
        }

        [HttpGet("sold/count")]
        public async Task<int> GetSoldProductsCount()
        {
            return await Mediator.Send(new GetProductGroupCountRequest(ProductStatus.Sold));
        }

        [HttpGet("in-stock/count")]
        public async Task<int> GetInStockProductsCount()
        {
            return await Mediator.Send(new GetProductGroupCountRequest(ProductStatus.InStock));
        }

        [HttpGet("damaged/count")]
        public async Task<int> GetDamagedProductsCount()
        {
            return await Mediator.Send(new GetProductGroupCountRequest(ProductStatus.Damaged));
        }

        [HttpGet("{id:int}")]
        public async Task<ProductDto> GetProductById(int id)
        {
            return await Mediator.Send(new GetProductRequest(id));
        }

        [HttpPost("add")]
        [OpenApiOperation("Add a new product.", "")]
        public async Task<int> AddAsync(CreateProductRequest request)
        {
            return await Mediator.Send(request);
        }

        [HttpPut]
        [OpenApiOperation("Update a product.", "")]
        public async Task<ActionResult<int>> UpdateAsync(UpdateProductRequest request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPut("update-status")]
        [OpenApiOperation("Change the status of a product.", "")]
        public async Task<ActionResult<int>> UpdateStatusAsync(UpdateProductStatusRequest request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpPut("sell")]
        [OpenApiOperation("Sell a product.", "")]
        public async Task<ActionResult<int>> SellAsync(int productId)
        {
            var request = new UpdateProductStatusRequest(productId, ProductStatus.Sold);
            return Ok(await Mediator.Send(request));
        }

        [HttpDelete("{id:int}")]
        [OpenApiOperation("Delete a product.", "")]
        public async Task<int> DeleteAsync(int id)
        {
            return await Mediator.Send(new DeleteProductRequest(id));
        }
    }
}
