using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Products.DTOs;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetAll
{
    public class GetAllProductsQuery : IRequest<PageResult<List<ProductDTO>>>
    {
        public ProductQuery Query { get; set; }
        public GetAllProductsQuery(ProductQuery query)
        {
            Query = query;
        }
    }
}
