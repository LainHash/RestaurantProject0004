using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Products.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Application.Features.Catalog.Products.Commands.ChangeImages
{
    public class ChangeImagesProductHandler : IRequestHandler<ChangeImagesProductCommand, Result<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;

        public ChangeImagesProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<ProductDTO>> Handle(ChangeImagesProductCommand request, CancellationToken cancellationToken)
        {
            return await _productRepository.ChangeImagesAsync(request.Id, request.ChangeImagesProductDTO, cancellationToken);
        }
    }
}
