using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Products.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetAll
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, PageResult<List<ProductDTO>>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<PageResult<List<ProductDTO>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var response = await _productRepository
                .GetAllAsync(request.Query, cancellationToken);
            return response;
        }
    }
}
