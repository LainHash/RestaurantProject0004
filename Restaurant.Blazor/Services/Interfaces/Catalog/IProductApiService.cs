using Restaurant.Blazor.Common.Models;
using Restaurant.Blazor.DTOs.Catalog.Products;

namespace Restaurant.Blazor.Services.Interfaces.Catalog
{
    public interface IProductApiService
    {
        Task<List<ProductVM>> GetProductsAsync(ProductQuery query);
        Task<ProductVM?> GetProductByPublicIdAsync(Guid id);
    }
}
