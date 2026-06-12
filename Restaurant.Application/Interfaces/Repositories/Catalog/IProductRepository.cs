using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Products.DTOs;

namespace Restaurant.Application.Interfaces.Repositories.Catalog
{
    public interface IProductRepository
    {
        Task<PageResult<List<ProductDTO>>> GetAllAsync(ProductQuery query,
                                                CancellationToken cancellationToken);

        Task<Result<ProductDTO>> GetOneByPublicIdAsync(Guid id,
                                                    CancellationToken cancellationToken);

        Task<Result<ProductDTO>> CreateAsync(CreateProductDTO request,
                                            CancellationToken cancellationToken);

        Task<Result<ProductDTO>> UpdateAsync(Guid id, UpdateProductDTO request,
                                            CancellationToken cancellationToken);

        Task<Result<ProductDTO>> ChangeImagesAsync(Guid id, ChangeImagesProductDTO request,
                                                CancellationToken cancellationToken);

        Task<Result> DeleteAsync(Guid id, 
                                CancellationToken cancellationToken);

        Task<Result> RestoreAsync(Guid id,
                                CancellationToken cancellationToken);

    }
}
