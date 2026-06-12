using Restaurant.Blazor.Common.Models;
using Restaurant.Blazor.DTOs.Catalog.Products;
using Restaurant.Blazor.Services.Interfaces;
using Restaurant.Blazor.Services.Interfaces.Catalog;

namespace Restaurant.Blazor.Services.Implementations.Catalog
{
    public class ProductApiService : IProductApiService
    {
        private readonly IApiService _apiService;
        public ProductApiService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<ProductVM>> GetProductsAsync(CancellationToken cancellation)
        {
            var result = await _apiService.GetAsync<ApiResponse<List<ProductVM>>>("api/product");
            return result?.IsSuccess == true ? result.Data ?? new() : new();
        }

        public async Task<ProductVM?> GetProductByPublicIdAsync(Guid id, CancellationToken cancellation)
        {
            var result = await _apiService.GetAsync<ApiResponse<ProductVM>>($"api/product/{id}");
            return result?.IsSuccess == true ? result.Data : null;
        }
    }
}
