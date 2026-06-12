using Restaurant.Application.Common.Models;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Application.Features.Catalog.Categories.DTOs
{
    public class CategoryDTO : SoftDeleteDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = string.Empty;

        public CategoryDTO() { }

        public CategoryDTO(Category category)
        {
            PublicId = category.PublicId;
            Name = category.Name;
            Description = category.Description ?? string.Empty;
            CreatedAt = category.CreatedAt;
            UpdatedAt = category.UpdatedAt;
            IsDeleted = category.IsDeleted;
            DeletedAt = category.DeletedAt;
        }
    }
}
