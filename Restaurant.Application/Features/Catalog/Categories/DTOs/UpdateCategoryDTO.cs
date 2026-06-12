

namespace Restaurant.Application.Features.Catalog.Categories.DTOs
{
    public class UpdateCategoryDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = string.Empty!;
    }
}
