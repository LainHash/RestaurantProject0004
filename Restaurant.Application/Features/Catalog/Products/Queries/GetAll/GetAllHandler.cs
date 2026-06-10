using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Products.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Application.Features.Catalog.Products.Queries.GetAll
{
    public class GetAllHandler : IRequestHandler<GetAllQuery, Result<List<ProductDTO>>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result<List<ProductDTO>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var result = await _productRepository.GetAllAsync(cancellationToken);
            return result;
        }
    }
}
