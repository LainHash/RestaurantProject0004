using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Categories.DTOs;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Update
{
    public class UpdateCategoryCommand : IRequest<Result<CategoryDTO>>
    {
        public Guid Id { get; set; }
        public UpdateCategoryDTO UpdateCategoryDTO { get; set; } = null!;
        public UpdateCategoryCommand(Guid id, UpdateCategoryDTO updateCategoryDTO)
        {
            Id = id;
            UpdateCategoryDTO = updateCategoryDTO;
        }
    }
}
