using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Products.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetByPublicId
{
    public class GetOneProductHandler : IRequestHandler<GetOneProductQuery, Result<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        public GetOneProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<ProductDTO>> Handle(GetOneProductQuery request, CancellationToken cancellationToken)
        {
            var response = await _productRepository
                .GetOneByPublicIdAsync(request.Id, cancellationToken);
            return response;
        }
    }
}
