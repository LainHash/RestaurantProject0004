using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Categories.DTOs;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<Result<CategoryDTO>>
{
    public CreateCategoryDTO CreateCategoryDTO { get; set; }
    public CreateCategoryCommand(CreateCategoryDTO createCategoryDTO)
    {
        CreateCategoryDTO = createCategoryDTO;
    }
}
