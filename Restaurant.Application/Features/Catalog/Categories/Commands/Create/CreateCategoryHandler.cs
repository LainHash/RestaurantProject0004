using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Categories.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Create;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Result<CategoryDTO>>
{
    private readonly ICategoryRepository _categoryRepository;
    public CreateCategoryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Result<CategoryDTO>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var response = await _categoryRepository.CreateAsync(request.CreateCategoryDTO, cancellationToken);
        return response;
    }
}
