namespace Restaurant.Application.Features.Catalog.Categories.DTOs
{
    public class CreateCategoryDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = string.Empty!;
    }
}
