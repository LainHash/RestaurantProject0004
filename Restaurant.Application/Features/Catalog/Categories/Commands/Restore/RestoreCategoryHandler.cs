using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Categories.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Restore
{
    public class RestoreCategoryHandler : IRequestHandler<RestoreCategoryCommand, Result>
    {
        private readonly ICategoryRepository _categoryRepository;
        public RestoreCategoryHandler(ICategoryRepository category)
        {
            _categoryRepository = category;
        }
        public async Task<Result> Handle(RestoreCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = await _categoryRepository.RestoreAsync(request.Id, cancellationToken);
            return response;
        }
    }
}
