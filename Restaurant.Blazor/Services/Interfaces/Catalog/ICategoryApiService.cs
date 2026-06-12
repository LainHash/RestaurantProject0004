using Restaurant.Blazor.Common.Models;
using Restaurant.Blazor.DTOs.Catalog.Categoies;

namespace Restaurant.Blazor.Services.Interfaces.Catalog
{
    public interface ICategoryApiService
    {
        Task<List<CategoryVM>> GetCategoiesAsync();
    }
}
