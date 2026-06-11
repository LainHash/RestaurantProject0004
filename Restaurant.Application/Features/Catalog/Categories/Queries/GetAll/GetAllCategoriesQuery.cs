using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Categories.DTOs;

namespace Restaurant.Application.Features.Catalog.Categories.Queries.GetAll
{
    public class GetAllCategoriesQuery : IRequest<Result<List<CategoryDTO>>>
    {
    }
}
