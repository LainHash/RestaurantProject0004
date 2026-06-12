using Restaurant.Blazor.Common.Models;
using Restaurant.Blazor.DTOs.Catalog.Categoies;
using Restaurant.Blazor.DTOs.Catalog.Products;
using Restaurant.Blazor.Services.Interfaces;
using Restaurant.Blazor.Services.Interfaces.Catalog;

namespace Restaurant.Blazor.Services.Implementations.Catalog
{
    public class CategoryApiService : ICategoryApiService
    {
        private readonly IApiService _apiService;
        public CategoryApiService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<CategoryVM>> GetCategoiesAsync()
        {
            var result = await _apiService.GetAsync<ApiResponse<List<CategoryVM>>>("api/category");
            return result?.IsSuccess == true ? result.Data ?? new() : new();
        }
    }
}
