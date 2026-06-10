using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Categories.DTOs;

namespace Restaurant.Application.Interfaces.Repositories.Catalog
{
    public interface ICategoryRepository
    {
        Task<Result<List<CategoryDTO>>> GetAllAsync(CancellationToken cancellationToken);
    }
}
