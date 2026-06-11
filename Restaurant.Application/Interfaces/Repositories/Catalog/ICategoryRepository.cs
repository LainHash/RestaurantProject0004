using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Categories.DTOs;

namespace Restaurant.Application.Interfaces.Repositories.Catalog
{
    public interface ICategoryRepository
    {
        Task<Result<List<CategoryDTO>>> GetAllAsync(CancellationToken cancellationToken);

        Task<Result<CategoryDTO>> CreateAsync(CreateCategoryDTO request, 
                                            CancellationToken cancellationToken);
        Task<Result<CategoryDTO>> UpdateAsync(Guid id, UpdateCategoryDTO request, 
                                            CancellationToken cancellationToken);

        Task<Result> DeleteAsync(Guid id, 
                                CancellationToken cancellationToken);

        Task<Result> RestoreAsync(Guid id,
                                CancellationToken cancellationToken);
    }
}
