using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Products.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetAll
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, Result<List<ProductDTO>>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result<List<ProductDTO>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var response = await _productRepository
                .GetAllAsync(cancellationToken);
            return response;
        }
    }
}
