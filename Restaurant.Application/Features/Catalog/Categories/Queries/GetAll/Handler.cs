using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Categories.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Application.Features.Catalog.Categories.Queries.GetAll
{
    public class Handler : IRequestHandler<Query, Result<List<CategoryDTO>>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public Handler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Result<List<CategoryDTO>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.GetAllAsync(cancellationToken);
            return result;
        }
    }
}
