using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Product.Application.Catalog.Categories;

namespace Product.Api.Controllers
{
    //[Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    public class CategoryController : BaseController
    {
        [HttpPost]
        [OpenApiOperation("Create a new product.", "")]
        public Task<int> CreateAsync(CreateCategoryRequest request)
        {
            return Mediator.Send(request);
        }
    }
}
