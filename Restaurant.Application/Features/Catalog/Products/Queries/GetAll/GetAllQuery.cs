using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Products.DTOs;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetAll
{
    public class GetAllQuery : IRequest<Result<List<ProductDTO>>>
    {
    }
}
