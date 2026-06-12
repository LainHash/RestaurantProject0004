using Restaurant.Blazor.Common.Models;

namespace Restaurant.Blazor.DTOs.Catalog.Categoies
{
    public class CategoryVM : BaseViewModel
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = string.Empty;
    }
}
