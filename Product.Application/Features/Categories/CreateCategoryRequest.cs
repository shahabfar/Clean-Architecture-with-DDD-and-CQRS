using MediatR;
using Product.Application.Interfaces;
using Product.Domain.Entities;

namespace Product.Application.Catalog.Categories
{
    public record CreateCategoryRequest(string Name, string? Description) : IRequest<int>
    {
        public class CreateCategoryRequestHandler : IRequestHandler<CreateCategoryRequest, int>
        {
            private readonly IRepository<Category> _repository;

            public CreateCategoryRequestHandler(IRepository<Category> repository) => _repository = repository;

            public async Task<int> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
            {
                var category = new Category(request.Name, request.Description);
                var addedCategory = await _repository.AddAsync(category, cancellationToken);
                return addedCategory.Id;
            }
        }
    }
}
