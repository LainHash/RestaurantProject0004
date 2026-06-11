using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Products.DTOs;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetByPublicId
{
    public class GetOneProductQuery : IRequest<Result<ProductDTO>>
    {
        public Guid Id { get; set; }
        public GetOneProductQuery(Guid id)
        {
            Id = id;
        }
    }
}
