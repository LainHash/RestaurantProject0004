using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Products.DTOs;

namespace Restaurant.Application.Interfaces.Repositories.Catalog
{
    public interface IProductRepository
    {
        Task<Result<List<ProductDTO>>> GetAllAsync(CancellationToken cancellationToken);
        Task<Result<ProductDTO>> GetOneByPublicIdAsync(Guid id, 
                                                    CancellationToken cancellationToken);
    }
}
