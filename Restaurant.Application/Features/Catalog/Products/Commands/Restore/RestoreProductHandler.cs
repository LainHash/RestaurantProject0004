using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Restore
{
    public class RestoreProductHandler : IRequestHandler<RestoreProductCommand, Result>
    {
        private readonly IProductRepository _productRepository;
        public RestoreProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Result> Handle(RestoreProductCommand request, CancellationToken cancellationToken)
        {
            var response = await _productRepository.RestoreAsync(request.Id, cancellationToken);
            return response;
        }
    }
}
