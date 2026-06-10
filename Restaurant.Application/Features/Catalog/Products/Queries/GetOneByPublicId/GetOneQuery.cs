using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Products.DTOs;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetByPublicId
{
    public class GetOneQuery : IRequest<Result<ProductDTO>>
    {
        public Guid Id { get; set; }
        public GetOneQuery(Guid id)
        {
            Id = id;
        }
    }
}
