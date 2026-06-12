using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Products.DTOs;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Update
{
    public class UpdateProductCommand : IRequest<Result<ProductDTO>>
    {
        public Guid Id { get; set; }
        public UpdateProductDTO UpdateProductDTO { get; set; } = null!;
        public UpdateProductCommand(Guid id, UpdateProductDTO updateProductDTO)
        {
            Id = id;
            UpdateProductDTO = updateProductDTO;
        }
    }
}
