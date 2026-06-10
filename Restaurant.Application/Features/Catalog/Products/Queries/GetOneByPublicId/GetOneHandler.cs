using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Products.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetByPublicId
{
    public class GetOneHandler : IRequestHandler<GetOneQuery, Result<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        public GetOneHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<ProductDTO>> Handle(GetOneQuery request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetOneByPublicIdAsync(request.Id, cancellationToken);
            return result;
        }
    }
}
