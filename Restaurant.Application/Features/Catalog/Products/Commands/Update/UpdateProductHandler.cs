using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Products.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Update
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Result<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<ProductDTO>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var response = await _productRepository.UpdateAsync(request.Id, request.UpdateProductDTO, cancellationToken);
            return response;
        }
    }
}
