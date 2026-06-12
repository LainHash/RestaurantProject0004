using Restaurant.Blazor.DTOs.Catalog.Products;

namespace Restaurant.Blazor.Services.Interfaces.Catalog
{
    public interface IProductApiService
    {
        Task<List<ProductVM>> GetProductsAsync(CancellationToken cancellation);
        Task<ProductVM?> GetProductByPublicIdAsync(Guid id, CancellationToken cancellation);
    }
}
