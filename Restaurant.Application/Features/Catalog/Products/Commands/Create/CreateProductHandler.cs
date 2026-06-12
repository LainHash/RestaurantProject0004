using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Products.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Create
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Result<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<ProductDTO>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var response = await _productRepository.CreateAsync(request.CreateProductDTO, cancellationToken);
            return response;
        }
    }
}
